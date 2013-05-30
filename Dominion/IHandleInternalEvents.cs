using Dominion.GameEvents;

namespace Dominion
{
    public interface IHandleInternalEvents
    {
        void Handle(IGameMessage message, ITurnScope turnScope);
    }
}