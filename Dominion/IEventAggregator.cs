using Dominion.GameEvents;

namespace Dominion
{
    public interface IEventAggregator
    {
        void Register(IHandleEvents handler);
        void Publish(GameMessage @event);
    }
}