using Dominion.Cards;
using Dominion.GameEvents;

namespace Dominion
{
    public interface IActionScope
    {
        Player Player { get; }
        Hand Hand { get; }
        void PerformBuy(CardType cardType);
        void Publish(IGameMessage message);
        StateStack State { get; }
        ITurnScope GetTurnScope { get; }
    }
}