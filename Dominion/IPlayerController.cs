using System;
using Dominion.Cards.BasicSet.Actions;
using Dominion.GameEvents;

namespace Dominion
{
    public interface IPlayerController
    {
        Guid Id { get; }
        IEventResponse HandleGameEvent(IGameMessage @event, IActionScope scope);
        IEventResponse HandleGameEvent(IGameMessage @event, IReactionScope scope);
    }
}