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
            Supply = new Supply();
            Hand = new Hand();
            TreasuresInHand = new CardSet();
            Player = new MockPlayer();
            ReactionScopes = new List<IReactionScope>();
            CardsInPlay = new CardSet();
            Player = new MockPlayer();
        }
        public IList<Card> PurchasedCards = new List<Card>();
        public Supply Supply { get; set; }
        public IActingPlayer Player { get; set; }
        public int TurnNumber { get; set; }
        public int PotentialCoins { get; set; }
        public int Coins { get; set; }
        public CardSet TreasuresInHand { get; set; }
        public Hand Hand { get; set; }
        public int Actions  { get; private set; }
        public int Buys { get; private set; }
        public IEnumerable<IReactionScope> ReactionScopes { get; private set; }
        public CardSet CardsInPlay { get; private set; }
        public CardSet Deck { get; private set; }
        public CardSet DiscardPile { get; private set; }
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

        public Card RevealCardFromDeck()
        {
            throw new NotImplementedException();
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

        public void TrashCardFromHand(Card card)
        {
            
        }

        public void GainCardFromSupply(CardType card)
        {
            
        }

        public Money GetPrice(Card card)
        {
            return card.BaseCost;
        }

        public T GetInstance<T>()
        {
            throw new NotImplementedException();
        }

        public CardSet FindCardsEligibleForPurchase(ITurnScope turnScope)
        {
            return new CardSet();
        }

        public void PutCardFromHandIntoPlay(Card card)
        {
            throw new NotImplementedException();
        }

        public void GainCardFromSupplyOntoTopOfDeck(Card card)
        {
            throw new NotImplementedException();
        }

        public void RevealCard(Card card)
        {
            throw new NotImplementedException();
        }

        public void PutCardOnTopOfDeck(Card card)
        {
            throw new NotImplementedException();
        }

        public void PutCardInTrash(Card item)
        {
            throw new NotImplementedException();
        }

        public void PutCardFromHandOnTopOfDeck(Card card)
        {
            throw new NotImplementedException();
        }

        public Card DrawCard()
        {
            throw new NotImplementedException();
        }

        public Card RevealCardFromTopOfDeck()
        {
            throw new NotImplementedException();
        }

        public void PutCardsIntoHand(CardSet cards)
        {
            throw new NotImplementedException();
        }

        public void PutCardsIntoDiscardPile(CardSet cardSet)
        {
            throw new NotImplementedException();
        }

        public void DrawCardsIntoHand(int count)
        {
            throw new NotImplementedException();
        }

        public void DrawCardIntoCardset(CardSet cardSet)
        {
            throw new NotImplementedException();
        }

        public CardSet MoveCardsFrom(CardSet currentLocation)
        {
            throw new NotImplementedException();
        }

        public void TrashCardInPlay(CardType cardType)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            
        }
    }
}