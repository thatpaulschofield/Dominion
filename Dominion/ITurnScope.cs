using Dominion.Cards;
using Dominion.GameEvents;

namespace Dominion
{
    public interface ITurnScope
    {
        Supply Supply { get; }
        int TurnNumber { get; }
        Player Player { get; }
        int PotentialCoins { get; }
        int Coins { get; }
        CardSet TreasuresInHand { get; }
        Hand Hand { get; }
        int Actions { get; }
        int Buys { get; }
        void Discard(CardSet cardsToDiscard);
        void PerformBuy(CardType cardToPurchase);
        void PlayTreasures(CardSet treasuresToPlay);
        string ToString();
        void Publish(GameMessage @event);
        void PlayAction(Card actionCard);
        void PlayTreasure(Card treasureCard);
        void ChangeState(TurnState delta);
        void ChangeState(params TurnState[] deltas);
        void CleanUp();
    }
}