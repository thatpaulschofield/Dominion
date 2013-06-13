using System.Linq;
using Dominion.GameEvents;

namespace Dominion.Cards.BasicSet.Actions
{
    public class ThroneRoom : TypedCard<ThroneRoom>
    {
        public ThroneRoom() : base(cost: 4, isAction: true, name: "Throne Room")
        {
        }

        public override void PlayAsAction(ITurnScope turnScope)
        {
            base.PlayAsAction(turnScope);
            turnScope.Publish(new SelectActionCardToThroneRoom(turnScope));
        }

        public class SelectActionCardToThroneRoom : GameMessage
        {
            public SelectActionCardToThroneRoom(IActionScope scope) : base(scope)
            {
                Description = "Select an action card [Throne Room]";
                GetAvailableResponses = () => from action in TurnScope.Hand.Actions()
                                              select new SelectedActionCardToThroneRoom(scope.GetTurnScope, action);
            }
        }

        public class SelectedActionCardToThroneRoom : GameEventResponse<SelectActionCardToThroneRoom, Card>{
            public SelectedActionCardToThroneRoom(ITurnScope turnScope, Card card) : base(turnScope, card)
            {
                Description = "Select " + card.Name + "[Throne Room]";
            }

            public override void Execute()
            {
                TurnScope.PutCardFromHandIntoPlay(Item);
                2.Times(() => Item.PlayAsAction(TurnScope));
            }
        }
    }
}