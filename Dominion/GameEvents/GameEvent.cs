using System;
using System.Collections.Generic;

namespace Dominion.GameEvents
{
    public interface IMessage
    {
        IEventResponse GetDefaultResponse();
        IActionScope ActionScope { get; }
        Func<IEnumerable<IEventResponse>> GetAvailableResponses { get; }
        IEnumerable<IEventResponse> GetAvailableReactions(IReactionScope scope);
        string Description { get; }
        bool IsExternalToPlayer(Player player);
    }
}
