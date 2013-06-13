using System;
using System.Collections.Generic;
using System.Linq;

namespace Dominion.GameEvents
{
    public abstract class GameMessage : IGameMessage
    {
        protected IList<IEventResponse> _availableResponses = new List<IEventResponse>();
        protected GameMessage(IActionScope scope)
        {
            ActionScope = scope;
            GetAvailableResponses = () => _availableResponses;
        }

        public virtual IEventResponse GetDefaultResponse()
        {
            if (GetAvailableResponses().Any())
                return GetAvailableResponses().First();
            else
            {
                return new NullResponse(ActionScope);
            }
        }

        public IActionScope ActionScope { get; private set; }

        public Func<IEnumerable<IEventResponse>> GetAvailableResponses { get; protected set; }

        public string Description { get; protected set; }
        public ITurnScope TurnScope { get { return ActionScope.GetTurnScope; } }

        public bool IsExternalToPlayer(Player player)
        {
            return !ReferenceEquals(player, ActionScope.Player);
        }

        public override string ToString()
        {
            return Description;
        }

        public Guid Id { get; set; }
        public Guid CorrelationId { get; set; }
        public Guid OriginalEventId { get; set; }
        public bool IsResponseAvailable(GameEventResponse response)
        {
            return GetAvailableResponses().Contains(response);
        }
    }
}