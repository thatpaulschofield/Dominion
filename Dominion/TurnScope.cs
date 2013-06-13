using System;
using System.Collections.Generic;
using System.Linq;
using Dominion.Cards;
using Dominion.GameEvents;
using StructureMap;

namespace Dominion
{
    public class TurnScope : AbstractActionScope, ITurnScope, IHandleEvents
    {
        private readonly TrashPile _trash;
        private readonly IContainer _container;
        private readonly List<IReactionScope> _reactionScopes = new List<IReactionScope>();
        private readonly CardSet _cardsInPlay = new CardSet();
        private TurnState _turnState;

        public TurnScope(Player player, 
                         Supply supply, 
                         IEventAggregator eventAggregator = null, 
                         TrashPile trash = null)
            : this(player, supply, 1, eventAggregator, trash)
        {
        }

        public TurnScope(Player player, 
                         Supply supply, 
                         int turnNumber, 
                         IEventAggregator eventAggregator, 
                         TrashPile trashPile) : base(supply, eventAggregator, trashPile, turnNumber)
        {
            _turnState = TurnState.NewTurn();
            if (supply == null)
                throw new ArgumentNullException("Supply cannot be null.");

            _player = player;
            Deck = _player.Deck;
            DiscardPile = _player.DiscardPile;
            _eventAggregator.Register(this);
        }

        public TurnScope(Player player, 
            Supply supply, 
            int turnNumber, 
            IEventAggregator eventAggregator, 
            IEnumerable<Player> reactingPlayers, 
            TrashPile trash, 
            IContainer container) 
            : this(player, supply, turnNumber, eventAggregator, trash)
        {
            container.Configure(cfg => cfg.For<ITurnScope>().Use(this));
            _container = container;
            reactingPlayers.ForEach(p => _reactionScopes.Add(new ReactionScope(eventAggregator, player, p, this, trash, supply)));
        }

        public ITurnScope GetTurnScope { get { return this; } }

        public Card RevealCardFromDeck()
        {
            return Player.RevealCardFromTopOfDeck(this);
        }

        public IEnumerable<IReactionScope> ReactionScopes { get { return _reactionScopes; } }
        
        public CardSet CardsInPlay { get { return new CardSet(_cardsInPlay); } }
        public CardSet Deck { get; private set; }
        public CardSet DiscardPile { get; private set; }

        public Hand Hand { get { return Player.Hand; } }

        public void Discard(CardSet cardsToDiscard)
        {
            cardsToDiscard.Into(_player.DiscardPile, this);
        }

        public void ChangeState(TurnState delta)
        {
            _turnState = _turnState + delta;
        }

        public void ChangeState(params TurnState[] deltas)
        {
            deltas.ForEach(ChangeState);
        }

        public void CleanUp()
        {
            Discard(_cardsInPlay);
            _player.DiscardHand(this);
            _player.DrawNewHand(this);
        }

        public int TotalCardCount { get { return _cardsInPlay.Count() + _player.Hand.Count() + _player.DiscardPile.Count() + _player.Deck.Count(); } }

        public void PerformBuy(CardType cardToPurchase)
        {
            if (Buys <= 0)
                throw new OutOfBuysException();

            Discard(Supply.AcquireCard(cardToPurchase, this));
            _turnState = _turnState.RegisterBuy(cardToPurchase.Create().BaseCost);
            _eventAggregator.Publish(new PlayerGainedCardEvent(cardToPurchase, this));
        }

        public void PlayTreasures(CardSet treasuresToPlay)
        {
            _player.PlayTreasures(treasuresToPlay, this);
        }

        public int Actions { get { return _turnState.Actions; }}

        public int Buys { get { return _turnState.Buys; } }

        public int Coins { get { return _turnState.Coins; } }

        public CardSet TreasuresInHand { get { return Player.Hand.Treasures(); } }

        public void PlayAction(Card action)
        {
            PutCardFromHandIntoPlay(action);
            ChangeState((-1).TurnActions());
            action.PlayAsAction(this);
        }

        public void PlayTreasure(Card treasure)
        {
            _cardsInPlay.Add(treasure, this);
            _turnState += treasure.Coins;
            _eventAggregator.Publish(new TreasurePlayedEvent(treasure, this));
        }

        public override string ToString()
        {
            return String.Format("{0}: {1}/{2}/({3})  H: {4}, P: {5}, Di: {6}, Dk {7}", Player.Name, Actions, Buys, Coins, Hand.Count(), _cardsInPlay.Count(),                                        _player.DiscardPile.Count(), _player.Deck.Count());
        }

        public void TrashCardFromHand(Card card)
        {
            Player.Hand.TrashCard(card, _trash, this);
        }

        public void GainCardFromSupply(CardType card)
        {
            Player.GainCardFromSupply(card, this);
        }

        public Money GetPrice(Card card)
        {
            var aggregatedAdjustment = _costModifiers.Aggregate(0.Coins(), (m, e) => m + e.CalculateCostAdjustment(card, this));
            return card.BaseCost + aggregatedAdjustment;
        }

        public T GetInstance<T>()
        {
            return _container.GetInstance<T>();
        }

        public CardSet FindCardsEligibleForPurchase(ITurnScope turnScope)
        {
            return Supply.FindCardsEligibleForPurchase(turnScope);
        }

        public void PutCardFromHandIntoPlay(Card card)
        {
            if (!Hand.Contains(card))
                throw new ArgumentOutOfRangeException("Hand does not contain the card " + card);

            Hand.Remove(card);
            _cardsInPlay.Add(card, this);
        }

        public void GainCardFromSupplyOntoTopOfDeck(Card card)
        {
            this.Player.PlaceCardOnTopOfDeck(Supply[card].Draw(this));
        }

        public void RevealCard(Card card)
        {
            Publish(new PlayerRevealedCardEvent(this, card));
        }

        public void PutCardOnTopOfDeck(Card card)
        {
            Player.PlaceCardOnTopOfDeck(card);
        }

        public void PutCardInTrash(Card card)
        {
            _trashPile.Add(card, this);
            Publish(new PlayerTrashedCardEvent(this, card));
        }

        public void PutCardFromHandOnTopOfDeck(Card card)
        {
            Hand.Remove(card);
            Player.PlaceCardOnTopOfDeck(card);
        }

        public Card DrawCard()
        {
            return Player.DrawIntoHand(1, this).Single();
        }

        public Card RevealCardFromTopOfDeck()
        {
            return Player.RevealCardFromTopOfDeck(this);
        }

        public void PutCardsIntoHand(CardSet cards)
        {
            Player.PlaceCardsIntoHand(cards);
        }

        public void PutCardsIntoDiscardPile(CardSet cards)
        {
            Player.PlaceCardsInDiscardPile(cards);
        }

        public void DrawCardsIntoHand(int count)
        {
            Player.DrawIntoHand(4, this);
        }

        public void DrawCardIntoCardset(CardSet cardSet)
        {
            var cards = Player.TakeCardsFromTopOfDeck(1, this);
            cards.ForEach(card => cardSet.Add(card, this));
        }

        public CardSet MoveCardsFrom(CardSet currentLocation)
        {
            var cards = new CardSet(currentLocation);
            cards.ForEach(currentLocation.Remove);
            return cards;
        }

        public void TrashCardInPlay(CardType cardType)
        {
            var card = _cardsInPlay.FirstOrDefault(c => c.CardType == cardType);
            if (card == null)
                return;

            _cardsInPlay.Remove(card);
            PutCardInTrash(card);
        }

        public void Dispose()
        {
            _eventAggregator.Unregister(this);
            _reactionScopes.ForEach(s => s.Dispose());
        }

        public bool CanHandle(IGameMessage @event)
        {
            return true;
        }
    }

    public class TrashPile : CardSet
    {
        
    }
}