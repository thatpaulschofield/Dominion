using System;
using Dominion.Cards.BasicSet.Actions;

namespace Dominion.GameEvents
{
    public interface IGameMessage : IMessage
    {
        Guid Id { get; }
        Guid CorrelationId { get; }
        Guid OriginalEventId { get; }
        bool IsResponseAvailable(GameEventResponse response);
    }
}