using System;
using System.Linq;
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
                                    turnScope.ActingPlayer.Hand.Select(c =>
                                        new CardSelectedToRemodelResponse(c, turnScope){Id = Guid.NewGuid(), CorrelationId = this.CorrelationId, OriginalEventId = this.OriginalEventId})
                                    .Append<GameEventResponse>(new DeclinedToRemodelCardResponse(turnScope));

        }
    }
}