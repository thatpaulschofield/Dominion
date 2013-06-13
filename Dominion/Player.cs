using System;
using System.Linq;
using Dominion.Cards;
using Dominion.Cards.BasicSet.Actions;
using Dominion.GameEvents;

namespace Dominion
{

    public class Player : IPlayer, IHandleExternalEvents
    {
        private readonly IPlayerController _controller;

        public Player(Deck deck, DiscardPile discardPile, IPlayerController strategy, PlayerId id = null, string name = "Player")
        {
            _controller = strategy;
            Name = name;

            if (deck == null)
                throw new ArgumentNullException("Must pass non-null instance of Deck");

            Hand = new Hand();
            DiscardPile = discardPile;
            Deck = deck.Shuffle();
        }

        public PlayerId Id { get { return _controller.Id; } }

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

        public void DrawIntoHand(int cards, IActionScope turnScope)
        {
            Deck.Draw(cards, turnScope).Into(Hand, turnScope);
        }

        public CardSet DrawIntoHand(int cards, ITurnScope turnScope)
        {
            var drawnCards = Deck.Draw(cards, turnScope);
            var cardsThatWereDrawn = new CardSet(drawnCards);                
            drawnCards.Into(Hand, turnScope);
            return cardsThatWereDrawn;
        }

        public void BeginActionPhase(ActionPhase phase)
        {
            var response = _controller.HandleGameEvent(phase, phase.TurnScope);
            response.Execute();
        }

        public void BeginBuyPhase(BuyPhase buyPhase)
        {
            var response = _controller.HandleGameEvent(buyPhase, buyPhase.TurnScope);
            response.Execute();
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

            if(@event.GetAvailableResponses().Count() == 1)
                @event.GetDefaultResponse().Execute();
            else
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

        public void EndGameCleanup(Game game)
        {
            var scope = game.StartTurn(this);
            Hand.Discard(DiscardPile, scope);
            DiscardPile.Into(Deck, scope);
        }

        public int CountScore(Game game)
        {
            var scope = game.StartTurn(this);
            int score = 0;
            score = Deck.Aggregate(score, (i, card) => i + card.Score(scope));
            return score;
        }

        public void PlaceCardOnTopOfDeck(Card card)
        {
            Deck.PlaceCardsOnTop(card);
        }

        public Card RevealCardFromTopOfDeck(IActionScope turnScope)
        {
            var card = Deck.Draw(turnScope);
            if (card != null)
                turnScope.Publish(new PlayerRevealedCardEvent(turnScope, card));
            return card;
        }

        public void PlaceCardsIntoHand(CardSet cards)
        {
            this.Hand.AddRange(cards);
        }

        public void PlaceCardsInDiscardPile(CardSet cards)
        {
            DiscardPile.AddRange(cards);
        }

        public CardSet TakeCardsFromTopOfDeck(int count, IActionScope scope)
        {
            return Deck.Draw(count, scope);
        }

        public void PutCardInTrash(Card card, IActionScope turnScope)
        {
            turnScope.PutCardInTrash(card);
        }

        public IEventResponse HandleCommand(ICommand command)
        {
            return _controller.HandleGameEvent(command, command.TurnScope);
        }

        public void RevealCard(Card card, IReactionScope externalEventScope)
        {
            Hand.RevealCard(card, externalEventScope, this);
        }

        public void Handle(IGameMessage message, IActionScope turnScope)
        {
            if (message is IPlayerScoped && turnScope.Player != this)
                return;

            _controller.HandleGameEvent(message, turnScope).Execute();
            Hand.Handle(message, turnScope);
        }

        public void GainCardFromSupply(Card typeToGain, IActionScope turnScope)
        {
            Card gainedCard = turnScope.Supply.AcquireCard(typeToGain, turnScope);
            turnScope.Publish(new PlayerGainedCardEvent(gainedCard, turnScope));
            DiscardPile.Discard(gainedCard, turnScope);
        }

        public void TrashCardFromHand(Card cardToTrash, IActionScope scope)
        {
            Hand.Remove(cardToTrash);
            scope.PutCardInTrash(cardToTrash);
        }

        public class PlayerId : Id<Guid>
        {
            public PlayerId() : base(Guid.NewGuid())
            {
            }

            public PlayerId(Guid id) : base(id)
            {
            }

            public static implicit operator PlayerId(Guid id)
            {
                return new PlayerId(id);
            }

            public static implicit operator Guid(PlayerId id)
            {
                return id._id;
            }
        }
    }

    public interface IPlayer : IActingPlayer, IReactingPlayer
    {
        Hand Hand { get; }
        Deck Deck { get; }
        DiscardPile DiscardPile { get; }
        void BeginActionPhase(ActionPhase phase);
        void BeginBuyPhase(BuyPhase buyPhase);
        void BeginCleanupPhase(ITurnScope turnScope);
        void PlayTreasures(CardSet treasuresToPlay, TurnScope turnScope);
        Turn BeginTurn(Game game);
        Turn BeginTurn(ITurnScope scope);
        void Handle(DeckDepletedEvent @event);
        void Handle(IGameMessage @event, IReactionScope scope);
        bool CanHandle(IGameMessage @event);
        void EndGameCleanup(Game game);
        int CountScore(Game game);
        IEventResponse HandleCommand(ICommand command);
        void Handle(IGameMessage message, IActionScope turnScope);
    }

    public interface IActingPlayer : IHandleInternalEvents
    {
        string Name { get; }
        Hand Hand { get; }
        void GainCardFromSupply(Card card, IActionScope turnScope);
        void Discard(CardSet cardsToDiscard, IActionScope turnScope);
        void ShuffleDiscardPileIntoDeck(IActionScope turnScope);
        void RevealCard(Card card, IReactionScope externalEventScope);
        void DrawNewHand(ITurnScope turnScope);
        void DiscardHand(ITurnScope turnScope);
        CardSet DrawIntoHand(int cards, ITurnScope turnScope);
        void PlaceCardOnTopOfDeck(Card card);
        Card RevealCardFromTopOfDeck(IActionScope turnScope);
        void PlaceCardsIntoHand(CardSet cards);
        void PlaceCardsInDiscardPile(CardSet cards);
        CardSet TakeCardsFromTopOfDeck(int count, IActionScope scope);
        void PutCardInTrash(Card item, IActionScope turnScope);
        void TrashCardFromHand(Card cardToTrash, IActionScope scope);
    }

    public interface IReactingPlayer
    {
        
    }
}