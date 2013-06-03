using System;
using Dominion.Cards;
using Dominion.GameEvents;

namespace Dominion.Tests
{
    public class MockPlayer : IPlayer
    {
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

        public void EndGameCleanup(ITurnScope turnScope)
        {
        }

        public int CountScore(ITurnScope turnScope)
        {
            return 0;
        }

        public void Draw(int cards, ITurnScope turnScope)
        {
        }

        public IEventResponse HandleCommand(ICommand command)
        {
            throw new NotImplementedException();
        }

        public void RevealCard(Card card, IReactionScope externalEventScope)
        {
        }

        public void Handle(IGameMessage message, ITurnScope turnScope)
        {
        }

        public void GainCardFromSupply(Card card, TurnScope turnScope)
        {
        }

        public void TrashCardFromHand(Card cardToTrash)
        {
        }

        public void GainCardFromSupply(Card cardToUpgradeTo, ITurnScope turnScope)
        {
        }
    }
}