using System;
using System.Linq;
using Dominion.AI;
using Dominion.GameEvents;

namespace Dominion
{
    public class Player : IHandleEvents<IMessage>
    {
        private readonly IPlayerController _controller;
        private readonly IEventAggregator _eventAggregator;

        public Player(IPlayerController controller, IEventAggregator eventAggregator) 
            : this(Game.DealStartupDeck(), new DiscardPile(), controller, eventAggregator)
        {
        }

        public Player(Deck deck, DiscardPile discardPile, IPlayerController strategy, IEventAggregator eventAggregator, string name = "Player")
        {
            _controller = strategy;
            _eventAggregator = eventAggregator;
            Name = name;
            _eventAggregator.Register(this);
            if (deck == null)
                throw new ArgumentNullException("Must pass non-null instance of Deck");

            Hand = new Hand(eventAggregator);
            DiscardPile = discardPile;
            Deck = deck.Shuffle();
        }

        public Hand Hand { get; private set; }

        public Deck Deck { get; private set; }

        public DiscardPile DiscardPile { get; private set; }

        public string Name { get; private set; }

        public void DrawNewHand(ITurnScope turnScope)
        {
            Deck.Draw(5, turnScope).Into(Hand, turnScope);
        }

        public void DiscardHand(ITurnScope turnScope)
        {
            Hand.Discard(DiscardPile, turnScope);
        }

        public void BeginActionPhase(ActionPhase phase)
        {
             var response = _controller.HandleGameEvent(phase);
            response.Execute();
            _eventAggregator.Publish(response);
        }

        public void BeginBuyPhase(BuyPhase buyPhase)
        {
            _controller.HandleGameEvent(new SelectTreasuresToPlayCommand(buyPhase.TurnScope)).Execute();
            var response = _controller.HandleGameEvent(buyPhase);
            response.Execute();
            _eventAggregator.Publish(response);
        }

        public void BeginCleanupPhase(ITurnScope turnScope)
        {
            turnScope.CleanUp();
        }

        public void PlayTreasures(CardSet treasuresToPlay, TurnScope turnScope)
        {
            treasuresToPlay.ToList().ForEach(t => this.Hand.PlayTreasure(t, turnScope));
        }

        public Turn BeginTurn(Game game)
        {
            var scope = game.StartTurn(this);
            var turn = BeginTurn(scope);
            return turn;
        }

        public Turn BeginTurn(ITurnScope scope)
        {
            var turn = new Turn(this, scope);
            turn.Begin();
            return turn;
        }

        public void Discard(CardSet cardsToDiscard, ITurnScope turnScope)
        {
            cardsToDiscard.DiscardInto(DiscardPile, turnScope);
        }

        public void ShuffleDiscardPileIntoDeck(ITurnScope turnScope)
        {
            DiscardPile.Into(Deck, turnScope);
            Deck = Deck.Shuffle();
            
            _eventAggregator.Publish(new DeckReplenishedEvent(turnScope));
        }

        #region IHandleEvents
        public void Handle(DeckDepletedEvent @event)
        {
            if (!Object.ReferenceEquals(@event.TurnScope.Player, this))
                return;

            this.ShuffleDiscardPileIntoDeck(@event.TurnScope);
        }

        public void Handle(IMessage @event)
        {
            if (@event is DeckDepletedEvent)
            {
                Handle(@event as DeckDepletedEvent);
                return;
            }

            _controller.HandleGameEvent(@event);
        }

        public bool CanHandle(IMessage @event)
        {
            return true;
        }
        #endregion

        public void EndGameCleanup(ITurnScope turnScope)
        {
            Hand.Discard(DiscardPile, turnScope);
            DiscardPile.Into(Deck, turnScope);
        }

        public int CountScore(ITurnScope turnScope)
        {
            int score = 0;
            score = Deck.Aggregate(score, (i, card) => i + card.Score(turnScope));
            return score;
        }

        public void Draw(int cards, ITurnScope turnScope)
        {
            Deck.Draw(cards, turnScope).Into(Hand, turnScope);
        }
    }
}