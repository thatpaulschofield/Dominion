using System;
using System.Collections.Generic;
using Dominion.GameEvents;

namespace Dominion.Tests
{
    public class MockPlayerController : IPlayerController
    {
        Dictionary<Type, GameEventResponse> _responses = new Dictionary<Type, GameEventResponse>();
        
        public void HandlesEventWithResponse<TEVENT>(GameEventResponse response)
        {
            _responses.Add(typeof(TEVENT), response);
        }

        public IEventResponse HandleGameEvent(IGameMessage @event)
        {
            if (_responses.ContainsKey(@event.GetType()))
            {
                return _responses[@event.GetType()];
            }

            return new NullResponse(@event.ActionScope);
        }

        public IEventResponse HandleGameEvent(IGameMessage @event, ITurnScope scope)
        {
            throw new NotImplementedException();
        }

        public IEventResponse HandleGameEvent(IGameMessage @event, IReactionScope scope)
        {
            throw new NotImplementedException();
        }
    }
}