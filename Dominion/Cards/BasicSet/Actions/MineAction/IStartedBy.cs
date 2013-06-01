using Dominion.GameEvents;

namespace Dominion.Cards.BasicSet.Actions.MineAction
{
    public interface IStartedBy<T> where T : IMessage
    {
        void Handle(T message);
    }
}