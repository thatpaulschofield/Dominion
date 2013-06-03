using Dominion.GameEvents;

namespace Dominion.Cards.BasicSet.Actions.Saga
{
    public interface IStartedBy<T> where T : IMessage
    {
        void Handle(T message);
    }
}