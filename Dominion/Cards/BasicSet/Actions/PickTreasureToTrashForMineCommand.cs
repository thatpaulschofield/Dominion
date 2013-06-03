using System;
using System.Linq;
using Dominion.Cards.BasicSet.Actions.MineAction;
using Dominion.GameEvents;

namespace Dominion.Cards.BasicSet.Actions
{
    public class PickTreasureToTrashForMineCommand : GameMessage, IPlayerScoped
    {
        public PickTreasureToTrashForMineCommand(ITurnScope scope) : base(scope)
        {
            Description = "Select a treasure to trash [Mine]";
            GetAvailableResponses =
                () => scope.ActingPlayer.Hand.Treasures()
                           .Select(
                               t =>
                               new TrashCardForMineResponse(scope)
                                   {
                                       Id = Guid.NewGuid(),
                                       CorrelationId = this.OriginalEventId,
                                       OriginalEventId = this.OriginalEventId,
                                       Item = t
                                   })
                           .Append<GameEventResponse>(new DeclinedToTrashCardForMineResponse(TurnScope));
        }
    }
}
