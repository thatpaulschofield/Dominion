using System.Collections.Generic;
using System.Linq;
using Dominion.Cards.BasicSet.Treasures;
using Dominion.GameEvents;

namespace Dominion.Cards.BasicSet.Actions
{
    public class Bureaucrat : TypedCard<Bureaucrat>
    {
        public Bureaucrat() : base(cost: 4, isAction: true, name: "Bureaucrat")
        {
        }

        public override void PlayAsAction(ITurnScope turnScope)
        {
            turnScope.GainCardFromSupplyOntoTopOfDeck(Treasure.Silver);
            turnScope.Publish(new BureaucratAttackEffect(turnScope));
        }

        public class BureaucratAttackEffect : GameMessage, IAttackEffect
        {
            public BureaucratAttackEffect(IActionScope scope) : base(scope)
            {
            }

            public void Resolve(IReactionScope scope)
            {
                scope.Publish(new SelectVictoryCardToRevealForBureaucrat(scope));
            }
        }

        public class SelectVictoryCardToRevealForBureaucrat : GameEventReaction<BureaucratAttackEffect>
        {
            public SelectVictoryCardToRevealForBureaucrat(IReactionScope scope) : base(scope)
            {
                Description = "Select a victory card to reveal [Bureaucrat]";

                GetAvailableResponses = GetResponses;
            }

            public IEnumerable<IEventReaction> GetResponses()
            {
                if (ReactionScope.Hand.VictoryCards().Any())
                    foreach (var v in ReactionScope.Hand.VictoryCards())
                    {
                        yield return new RevealVictoryCardForBureaucrat(ReactionScope, v);
                    }
                else
                    yield return new RevealHandWithNoVictoryCardsForBureaucratGameEventReaction(ReactionScope);
            }

            public override void Execute()
            {
                
            }
        }

        public class RevealVictoryCardForBureaucrat : GameEventReaction<SelectVictoryCardToRevealForBureaucrat, Card>
        {
            public RevealVictoryCardForBureaucrat(IReactionScope scope, Card card) : base(scope, card)
            {
            }

            public override void Execute()
            {
                ReactionScope.RevealCard(Item);
                ReactionScope.PutCardFromHandOnTopOfDeck(Item);
            }
        }

        public class RevealHandWithNoVictoryCardsForBureaucratGameEventReaction : GameEventReaction<SelectVictoryCardToRevealForBureaucrat>
        {
            public RevealHandWithNoVictoryCardsForBureaucratGameEventReaction(IReactionScope scope) : base(scope)
            {
            }

            public override void Execute()
            {
                ReactionScope.Hand.ForEach(c => ReactionScope.RevealCard(c));
            }
        }
    }
}