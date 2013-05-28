using System;
using System.Collections.Generic;

namespace Dominion.GameEvents
{
    public interface IMessage
    {
        GameEventResponse GetDefaultResponse();
        ITurnScope TurnScope { get; }
        Func<IEnumerable<GameEventResponse>> GetAvailableResponses { get; }
        string Description { get; }
    }
}
