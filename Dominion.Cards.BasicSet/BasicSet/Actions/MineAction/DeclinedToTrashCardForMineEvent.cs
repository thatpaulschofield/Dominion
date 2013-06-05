using Dominion.GameEvents;

namespace Dominion.Cards.BasicSet.Actions.MineAction
{
    public class DeclinedToTrashCardForMineResponse : GameEventResponse<PickTreasureToTrashForMineCommand>
    {
        public DeclinedToTrashCardForMineResponse(ITurnScope scope) : base(scope)
        {
            Description = "None";
        }

        public override void Execute()
        {
        }
    }
}