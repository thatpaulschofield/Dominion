using System;
using System.Collections.Generic;
using System.Linq;

namespace Dominion.GameEvents
{
    public abstract class GameMessage : IMessage
    {
        protected IList<GameEventResponse> _availableResponses = new List<GameEventResponse>();
        protected GameMessage(ITurnScope turnScope)
        {
            TurnScope = turnScope;
            GetAvailableResponses = () => _availableResponses;
        }

        public virtual GameEventResponse GetDefaultResponse()
        {
            if (GetAvailableResponses().Any())
                return GetAvailableResponses().First();
            else
            {
                return new NullResponse(TurnScope);
            }
        }

        public ITurnScope TurnScope { get; private set; }
        public Func<IEnumerable<GameEventResponse>> GetAvailableResponses { get; protected set; }

        public string Description { get; protected set; }
    }
}