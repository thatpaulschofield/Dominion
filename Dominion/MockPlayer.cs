using System;
using Dominion.Cards;
using Dominion.GameEvents;

namespace Dominion.Tests
{
    public class MockPlayer : IPlayer
    {
        public MockPlayer()
        {
            Hand = new Hand();
            Deck = new Deck();
            DiscardPile = new DiscardPile();
        }
        public Hand Hand { get; private set; }
        public Deck Deck { get; private set; }
        public DiscardPile DiscardPile { get; private set; }
        public string Name { get; private set; }
        public void DrawNewHand(ITurnScope turnScope)
        {
            
        }

        public void DiscardHand(ITurnScope turnScope)
        {
        }

        public void BeginActionPhase(ActionPhase phase)
        {
        }

        public void BeginBuyPhase(BuyPhase buyPhase)
        {
        }

        public void BeginCleanupPhase(ITurnScope turnScope)
        {
        }

        public void PlayTreasures(CardSet treasuresToPlay, TurnScope turnScope)
        {
        }

        public Turn BeginTurn(Game game)
        {
            throw new NotImplementedException();
        }

        public Turn BeginTurn(ITurnScope scope)
        {
            throw new NotImplementedException();
        }

        public void Discard(CardSet cardsToDiscard, IActionScope turnScope)
        {
        }

        public void ShuffleDiscardPileIntoDeck(IActionScope turnScope)
        {
        }

        public void Handle(DeckDepletedEvent @event)
        {
        }

        public void Handle(IGameMessage @event, IReactionScope scope)
        {
        }

        public bool CanHandle(IGameMessage @event)
        {
            return true;
        }

        public void EndGameCleanup(Game game)
        {
        }

        public int CountScore(Game game)
        {
            return 0;
        }

        public CardSet DrawIntoHand(int cards, ITurnScope turnScope)
        {
            throw new NotImplementedException();
        }

        public void PlaceCardOnTopOfDeck(Card card)
        {
            throw new NotImplementedException();
        }

        public Card RevealCardFromTopOfDeck(IActionScope turnScope)
        {
            throw new NotImplementedException();
        }

        public void PlaceCardsIntoHand(CardSet cards)
        {
            throw new NotImplementedException();
        }

        public void PlaceCardsInDiscardPile(CardSet cards)
        {
            throw new NotImplementedException();
        }

        public CardSet TakeCardsFromTopOfDeck(int count, IActionScope scope)
        {
            throw new NotImplementedException();
        }

        public void PutCardInTrash(Card item, IActionScope turnScope)
        {
            throw new NotImplementedException();
        }

        public CardSet TakeCardsFromTopOfDeck(int count)
        {
            throw new NotImplementedException();
        }

        public IEventResponse HandleCommand(ICommand command)
        {
            throw new NotImplementedException();
        }

        public void RevealCard(Card card, IReactionScope externalEventScope)
        {
        }

        public void Handle(IGameMessage message, IActionScope turnScope)
        {
        }

        public void GainCardFromSupply(Card card, IActionScope turnScope)
        {
        }

        public void TrashCardFromHand(Card cardToTrash, IActionScope scope)
        {
        }

        public void GainCardFromSupply(Card cardToUpgradeTo, ITurnScope turnScope)
        {
        }
    }
}