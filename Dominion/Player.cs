using System;
using System.Linq;
using Dominion.Cards;
using Dominion.Cards.BasicSet.Actions;
using Dominion.GameEvents;

namespace Dominion
{
    public class Player : IHandleInternalEvents, IHandleExternalEvents
    {
        private readonly IPlayerController _controller;

        public Player(IPlayerController controller) 
            : this(Game.DealStartupDeck(), new DiscardPile(), controller)
        {
        }

        public Player(Deck deck, DiscardPile discardPile, IPlayerController strategy, string name = "Player")
        {
            _controller = strategy;
            Name = name;
            if (deck == null)
                throw new ArgumentNullException("Must pass non-null instance of Deck");

            Hand = new Hand();
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
             var response = _controller.HandleGameEvent(phase, phase.TurnScope);
            response.Execute();
            phase.ActionScope.Publish(response);
        }

        public void BeginBuyPhase(BuyPhase buyPhase)
        {
            var response = _controller.HandleGameEvent(buyPhase, buyPhase.TurnScope);
            response.Execute();
            buyPhase.ActionScope.Publish(response);
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

        public void Discard(CardSet cardsToDiscard, IActionScope turnScope)
        {
            cardsToDiscard.ForEach(card => Hand.Discard(card, DiscardPile, turnScope));
        }

        public void ShuffleDiscardPileIntoDeck(IActionScope turnScope)
        {
            DiscardPile.Into(Deck, turnScope);
            Deck = Deck.Shuffle();
            
            turnScope.Publish(new DeckReplenishedEvent(turnScope));
        }

        #region IHandleEvents
        public void Handle(DeckDepletedEvent @event)
        {
            if (!Object.ReferenceEquals(@event.TurnScope.Player, this))
                return;

            this.ShuffleDiscardPileIntoDeck(@event.TurnScope);
        }

        public void Handle(IGameMessage @event, IReactionScope scope)
        {
            if (@event is DeckDepletedEvent)
            {
                Handle(@event as DeckDepletedEvent);
                return;
            }

            if (@event is IAttackEffect)
                HandleAttack(@event as IAttackEffect, scope);

            Hand.Handle(@event, scope);

            _controller.HandleGameEvent(@event, scope).Execute();
        }

        private void HandleAttack(IAttackEffect attackEffect, IReactionScope scope)
        {
            attackEffect.Resolve(scope);
        }

        public bool CanHandle(IGameMessage @event)
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

        public IEventResponse HandleCommand(ICommand command)
        {
            return _controller.HandleGameEvent(command, command.TurnScope);
        }

        public void RevealCard(Card card, IReactionScope externalEventScope)
        {
            Hand.RevealCard(card, externalEventScope, this);
        }

        public void Handle(IGameMessage message, ITurnScope turnScope)
        {
            _controller.HandleGameEvent(message, turnScope).Execute();
            Hand.Handle(message, turnScope);
        }
    }
}