using Dominion.GameEvents;

namespace Dominion
{
    public interface IHandleEvents<TEVENT> : IHandleEvents
    {
        void Handle(TEVENT @event);
    }

    public interface IHandleEvents
    {
        void Handle(IMessage @event);
        bool CanHandle(IMessage @event);
    }
}