using System;

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
            TurnScope.TrashCard(Item);
            TurnScope.Publish(new CardTrashedForMineEvent(TurnScope, Item){ Id = Guid.NewGuid(), CorrelationId = this.OriginalEventId, OriginalEventId = this.OriginalEventId});
        }
    }
}