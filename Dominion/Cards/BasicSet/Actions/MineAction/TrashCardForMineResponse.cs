using System;
using Dominion.GameEvents;

namespace Dominion.Cards.BasicSet.Actions.MineAction
{
    public class TrashCardForMineResponse : GameEventResponse<Card>
    {
        public TrashCardForMineResponse(ITurnScope turnScope) : base(turnScope)
        {
        }

        protected override void OnItemSet()
        {
            Description = "Trash " + Item.Name;
        }

        public override void Execute()
        {
            TurnScope.Publish(new CardSelectedToTrashForMineEvent(Item, TurnScope){ Id = Guid.NewGuid(), CorrelationId = this.OriginalEventId, OriginalEventId = this.OriginalEventId});
        }
    }
}