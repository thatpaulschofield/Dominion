using System;
using System.Linq;
using Dominion.Cards.BasicSet.Actions;
using Dominion.GameEvents;

namespace Dominion.Cards.Intrigue.DeckSets
{
    public class Saboteur : TypedCard<Saboteur>
    {
        public Saboteur() : base(cost: 5, isAction: true, isAttack:true)
        {
        }

        public override void PlayAsAction(ITurnScope turnScope)
        {
            turnScope.Publish(new SaboteurEffect(turnScope));
        }

        public class SaboteurEffect : GameMessage, IAttackEffect
        {
            public SaboteurEffect(IActionScope scope) : base(scope)
            {
            }

            public void Resolve(IReactionScope scope)
            {
                bool validCardFound;
                bool hasReshuffled = false;
                Card card;
                CardSet revealedCards = new CardSet();
                do
                {
                    card = scope.RevealCardFromDeck();
                    validCardFound = card != null && scope.GetPrice(card) >= 3;
                    if (card == null && !hasReshuffled)
                    {
                        scope.ReactingPlayer.ShuffleDiscardPileIntoDeck(scope);
                        hasReshuffled = true;
                    }
                    if (!validCardFound)
                        revealedCards.Add(card, scope);
                } while (!validCardFound && card != null);
                if (card == null)
                    return;

                scope.PutCardInTrash(card);
                scope.Publish(new SelectReplacementCardForSaboteur(scope, card));
            }
        }

        public class SelectReplacementCardForSaboteur : GameEventResponse<SaboteurEffect>
        {
            public SelectReplacementCardForSaboteur(IActionScope scope, Card trashedCard) : base(scope)
            {
                Description = scope.Player.Name + ", select a card to replace the trashed card (" + trashedCard.Name +
                              ")";
                GetAvailableResponses = () => scope.Supply.FindCardsCostingUpTo(scope.GetPrice(trashedCard) - 2, scope)
                                                    .OrderByDescending(scope.GetPrice)
                                                    .Select(c => new ReplaceCardForSaboteur(scope, c));
            }

            public override void Execute()
            {
                
            }
        }

        public class ReplaceCardForSaboteur : GameEventResponse<SelectReplacementCardForSaboteur, Card>
        {
            public ReplaceCardForSaboteur(IActionScope scope, Card card) : base(scope, card)
            {
                if (card == null)
                    throw new ArgumentNullException("card");
                Description = "Replace trashed card with " + card.Name + ". [Saboteur]";
            }

            public override void Execute()
            {
                ActionScope.GainCardFromSupply(Item);
            }
        }
    }
}