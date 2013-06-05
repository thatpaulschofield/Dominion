using Dominion.GameEvents;

namespace Dominion.Cards.BasicSet.Actions.Remodel
{
    public class RemodelPlayedMessage : GameMessage
    {
        public RemodelPlayedMessage(IActionScope scope)
            : base(scope)
        {
        }
    }
}