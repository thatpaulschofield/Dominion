﻿using Dominion.GameEvents;

namespace Dominion
{
    public class NaivePlayerController : IPlayerController
    {
        public IEventResponse HandleGameEvent(IGameMessage @event)
        {
            return @event.GetDefaultResponse();
        }

        public IEventResponse HandleGameEvent(IGameMessage @event, ITurnScope scope)
        {
            return @event.GetDefaultResponse();
        }

        public IEventResponse HandleGameEvent(IGameMessage @event, IReactionScope scope)
        {
            return @event.GetDefaultResponse();
        }
    }
}