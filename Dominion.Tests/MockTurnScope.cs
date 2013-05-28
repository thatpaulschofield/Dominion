using System.Collections.Generic;
using Dominion.Cards;
using Dominion.GameEvents;

namespace Dominion.Tests
{
    public class MockTurnScope : ITurnScope
    {
        public MockTurnScope()
        {
            TurnNumber = 1;
        }
        public IList<Card> PurchasedCards = new List<Card>();
        public Supply Supply { get; set; }
        public int TurnNumber { get; set; }
        public Player Player { get; set; }
        public int PotentialCoins { get; set; }
        public int Coins { get; set; }
        public CardSet TreasuresInHand { get; set; }
        public Hand Hand { get; set; }
        public int Actions  { get; private set; }
        public int Buys { get; private set; }

        public void Discard(CardSet cardsToDiscard)
        {
            
        }

        public void CleanUp(ITurnScope turnScope)
        {
        }

        public void PerformBuy(CardType cardToPurchase)
        {
            PurchasedCards.Add(cardToPurchase.Create());
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