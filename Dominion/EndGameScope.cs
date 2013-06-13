using System.Collections.Generic;
using Dominion.Cards;
using Dominion.GameEvents;

namespace Dominion
{
    internal class EndGameScope : ITurnScope
    {
        public Supply Supply { get; private set; }
        public IActingPlayer Player { get; private set; }
        public int TurnNumber { get; private set; }
        public int PotentialCoins { get; private set; }
        public int Coins { get; private set; }
        public CardSet TreasuresInHand { get; private set; }
        public Hand Hand { get; private set; }
        public int Actions { get; private set; }
        public int Buys { get; private set; }
        public IEnumerable<Player> PassivePlayers { get; private set; }
        public IEnumerable<IReactionScope> ReactionScopes { get; private set; }
        public CardSet CardsInPlay { get; private set; }
        public CardSet Deck { get; private set; }
        public CardSet DiscardPile { get; private set; }

        public EndGameScope()
        {
        }

        public void Discard(CardSet cardsToDiscard)
        {
            
        }

        public void PerformBuy(CardType cardToPurchase)
        {
        }

        public void PlayTreasures(CardSet treasuresToPlay)
        {
        }

        public void Publish(IGameMessage @event)
        {
        }

        public ITurnScope GetTurnScope { get; private set; }

        public Card RevealCardFromDeck()
        {
            throw new System.NotImplementedException();
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
            throw new System.NotImplementedException();
        }

        public T GetInstance<T>()
        {
            throw new System.NotImplementedException();
        }

        public CardSet FindCardsEligibleForPurchase(ITurnScope turnScope)
        {
            return new CardSet();
        }

        public void PutCardFromHandIntoPlay(Card card)
        {
            throw new System.NotImplementedException();
        }

        public void GainCardFromSupplyOntoTopOfDeck(Card card)
        {
            throw new System.NotImplementedException();
        }

        public void RevealCard(Card card)
        {
            throw new System.NotImplementedException();
        }

        public void PutCardOnTopOfDeck(Card card)
        {
            throw new System.NotImplementedException();
        }

        public void PutCardInTrash(Card item)
        {
            throw new System.NotImplementedException();
        }

        public void PutCardFromHandOnTopOfDeck(Card card)
        {
            throw new System.NotImplementedException();
        }

        public Card DrawCard()
        {
            throw new System.NotImplementedException();
        }

        public Card RevealCardFromTopOfDeck()
        {
            throw new System.NotImplementedException();
        }

        public void PutCardsIntoHand(CardSet cards)
        {
            throw new System.NotImplementedException();
        }

        public void PutCardsIntoDiscardPile(CardSet cardSet)
        {
            throw new System.NotImplementedException();
        }

        public void DrawCardsIntoHand(int count)
        {
            throw new System.NotImplementedException();
        }

        public void DrawCardIntoCardset(CardSet cardSet)
        {
            throw new System.NotImplementedException();
        }

        public CardSet MoveCardsFrom(CardSet currentLocation)
        {
            throw new System.NotImplementedException();
        }

        public void TrashCardInPlay(CardType cardType)
        {
            throw new System.NotImplementedException();
        }

        public void Dispose()
        {
            
        }
    }
}