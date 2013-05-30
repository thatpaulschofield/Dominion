using System.Linq;
using Dominion.GameEvents;

namespace Dominion.Cards.BasicSet.Actions
{
    public class DiscardReactionCommand : GameMessage
    {
        private readonly IReactionScope _receiverScope;

        public DiscardReactionCommand(IReactionScope receiverScope, string description) : base(receiverScope.OriginatingTurnScope)
        {
            Description = description;
            _receiverScope = receiverScope;

            GetAvailableResponses =
                () => 
                _receiverScope.ReceivingPlayer.Hand.Select(c => new DiscardCardReaction(_receiverScope, c));
        }

        public override System.Collections.Generic.IEnumerable<IEventResponse> GetAvailableReactions(IReactionScope scope)
        {
            return scope.ReceivingPlayer.Hand.Select(c => new DiscardCardReaction(_receiverScope, c));
        }

    }
}