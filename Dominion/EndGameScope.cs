using Dominion.Cards;
using Dominion.GameEvents;

namespace Dominion
{
    internal class EndGameScope : ITurnScope
    {
        public Supply Supply { get; private set; }
        public int TurnNumber { get; private set; }
        public Player Player { get; private set; }
        public int PotentialCoins { get; private set; }
        public int Coins { get; private set; }
        public CardSet TreasuresInHand { get; private set; }
        public Hand Hand { get; private set; }
        public int Actions { get; private set; }
        public int Buys { get; private set; }

        public void Discard(CardSet cardsToDiscard)
        {
            
        }

        public void PerformBuy(CardType cardToPurchase)
        {
        }

        public void PlayTreasures(CardSet treasuresToPlay)
        {
        }

        public void Publish(GameMessage @event)
        {
        }

        public void PlayAction(Card actionCard)
        {
        }

        public void PlayTreasure(Card treasureCard)
        {
        }

        public void ChangeState(TurnState delta)
        {
            
        }

        public void ChangeState(params TurnState[] deltas)
        {
            
        }

        public void CleanUp()
        {
        }
    }
}