using System;
using Dominion.AI;
using Dominion.GameEvents;

namespace Dominion
{
    public interface IPlayerController
    {
        GameEventResponse HandleGameEvent(GameEvent @event);
    }
    public class Player
    {
        private readonly IPlayerController _strategy;

        public Player(Deck deck, DiscardPile discardPile, IPlayerController strategy)
        {
            _strategy = strategy;
            if (deck == null)
                throw new ArgumentNullException("Must pass non-null instance of Deck");

            Hand = new Hand();
            DiscardPile = discardPile;
            Deck = deck;
        }

        public Hand Hand { get; private set; }

        public Deck Deck { get; private set; }

        public DiscardPile DiscardPile { get; private set; }

        public void DrawNewHand()
        {
            DiscardHand();
            Deck.Draw(5).Into(Hand);
        }

        private void DiscardHand()
        {
            Hand.Discard(DiscardPile);
        }

        public void BeginActionPhase(ActionPhase phase)
        {
             _strategy.HandleGameEvent(phase).Execute();
        }

        public void BeginBuyPhase(BuyPhase buyPhase)
        {
            _strategy.HandleGameEvent(buyPhase).Execute();
        }

        public void BeginCleanupPhase(TurnScope turnScope)
        {
            turnScope.CleanUp();
        }
    }
}