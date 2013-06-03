using Dominion.Cards;
using Dominion.GameEvents;

namespace Dominion
{
    public interface IActionScope
    {
        IActingPlayer ActingPlayer { get; }
        Hand Hand { get; }
        void PerformBuy(CardType cardType);
        void Publish(IGameMessage message);
        ITurnScope GetTurnScope { get; }
    }
}