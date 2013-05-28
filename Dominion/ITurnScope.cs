using Dominion.Cards;
using Dominion.GameEvents;

namespace Dominion
{
    public interface ITurnScope
    {
        Supply Supply { get; }
        Player Player { get; }
        Hand Hand { get; }
        CardSet TreasuresInHand { get; }
        int TurnNumber { get; }
        int Coins { get; }
        int Actions { get; }
        int Buys { get; }
        void Discard(CardSet cardsToDiscard);
        void PerformBuy(CardType cardToPurchase);
        void PlayTreasures(CardSet treasuresToPlay);
        void Publish(GameMessage @event);
        void PlayAction(Card actionCard);
        void PlayTreasure(Card treasureCard);
        void ChangeState(TurnState delta);
        void ChangeState(params TurnState[] deltas);
        void CleanUp();
        string ToString();
    }
}