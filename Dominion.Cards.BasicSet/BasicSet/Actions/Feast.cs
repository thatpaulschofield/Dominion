using System;
using System.Linq;
using Dominion.GameEvents;

namespace Dominion.Cards.BasicSet.Actions
{
    public class Feast : TypedCard<Feast>
    {
        public Feast() : base(cost: 4, isAction: true, name: "Feast")
        {
            
        }

        public override void PlayAsAction(ITurnScope turnScope)
        {
            turnScope.Publish(new SelectCardToFeast(turnScope));
        }

        public class SelectCardToFeast : GameMessage
        {
            public SelectCardToFeast(IActionScope scope) : base(scope)
            {
                Description = "Select a card to gain [Feast]";
                GetAvailableResponses = () => scope.GetTurnScope.Supply.FindCardsCostingUpTo(5.Coins(), scope)
                                                   .Select(c => new SelectedCardToFeast(scope.GetTurnScope, c));
            }
        }

        public class SelectedCardToFeast : GameEventResponse<SelectCardToFeast, Card>
        {
            public SelectedCardToFeast(ITurnScope turnScope, Card item) : base(turnScope, item)
            {
                Description = String.Format("Gain a {0} [Feast]", item.Name);
            }

            public override void Execute()
            {
                TurnScope.TrashCardFromHand(new Feast().CardType);
                TurnScope.GainCardFromSupply(Item);
            }
        }
    }
}