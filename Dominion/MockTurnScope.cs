using System;
using System.Collections.Generic;
using Dominion.Cards;
using Dominion.GameEvents;
using Dominion.Tests.GameEvents;

namespace Dominion.Tests
{
    public class MockTurnScope : ITurnScope
    {
        public MockTurnScope(IEventAggregator eventAggregator = null)
        {
            TurnNumber = 1;
            EventAggregator = eventAggregator ?? new MockEventAggregator();
            PassivePlayers = new List<Player>();
            ActingPlayer = new MockPlayer();
        }
        public IList<Card> PurchasedCards = new List<Card>();
        public Supply Supply { get; set; }
        public IActingPlayer ActingPlayer { get; set; }
        public int TurnNumber { get; set; }
        public int PotentialCoins { get; set; }
        public int Coins { get; set; }
        public CardSet TreasuresInHand { get; set; }
        public Hand Hand { get; set; }
        public int Actions  { get; private set; }
        public int Buys { get; private set; }
        public IEnumerable<IReactionScope> ReactionScopes { get; private set; }
        public CardSet CardsInPlay { get; private set; }
        public IEnumerable<Player> PassivePlayers { get; private set; }
        public IEventAggregator EventAggregator { get; set; }

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

        public void Publish(IGameMessage @event)
        {
            EventAggregator.Publish(@event);
        }

        public ITurnScope GetTurnScope { get { return this; } }

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

        public void TrashCard(Card card)
        {
            
        }

        public void GainCardFromSupply(CardType card)
        {
            
        }

        public T GetInstance<T>()
        {
            throw new NotImplementedException();
        }

        public CardSet FindCardsEligibleForPurchase(ITurnScope turnScope)
        {
            return new CardSet();
        }

        public void Dispose()
        {
            
        }
    }
}