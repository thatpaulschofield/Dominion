using System;
using System.Linq;
using Dominion.Cards.BasicSet.BasicSet.Actions.Remodel;
using Dominion.GameEvents;

namespace Dominion.Cards.BasicSet.Actions.Remodel
{
    public class PickCardToRemodelCommand : GameMessage, IPlayerScoped
    {
        public PickCardToRemodelCommand(ITurnScope turnScope)
            : base(turnScope)
        {
            Description = "Select a card from your hand to remodel [Remodel].";
            GetAvailableResponses = () =>
                                    turnScope.Player.Hand.Select(c =>
                                        new CardSelectedToRemodelResponse(turnScope, c){Id = Guid.NewGuid(), CorrelationId = this.CorrelationId, OriginalEventId = this.OriginalEventId})
                                    .Append<GameEventResponse>(new DeclinedToRemodelCardResponse(turnScope));

        }
    }
}