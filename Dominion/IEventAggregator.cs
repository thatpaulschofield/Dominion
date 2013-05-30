using Dominion.GameEvents;

namespace Dominion
{
    public interface IEventAggregator
    {
        void Register(IHandleEvents handler);
        void Unregister(IHandleEvents handler);
        void Publish(IGameMessage @event);
    }
}