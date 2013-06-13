using Dominion.Cards;
using Dominion.GameEvents;

namespace Dominion
{
    public interface IActionScope
    {
        IActingPlayer Player { get; }
        Hand Hand { get; }
        void PerformBuy(CardType cardType);
        void Publish(IGameMessage message);
        ITurnScope GetTurnScope { get; }
        CardSet Deck { get; }
        Supply Supply { get; }
        Card RevealCardFromDeck();
        void PutCardsIntoDiscardPile(CardSet cards);
        void PutCardOnTopOfDeck(Card card);
        void PutCardInTrash(Card item);
        void GainCardFromSupply(CardType card);
        Money GetPrice(Card card);
    }
}