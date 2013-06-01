using System;
using System.Collections.Generic;
using System.Linq;
using Dominion.Cards;
using Dominion.GameEvents;
using StructureMap;

namespace Dominion
{
    public class TurnScope : ITurnScope, IHandleEvents
    {
        private readonly TrashPile _trash;
        private readonly IContainer _container;
        private readonly Player _player;
        private readonly IEventAggregator _eventAggregator;
        private readonly List<IReactionScope> _reactionScopes = new List<IReactionScope>();

        private readonly CardSet _cardsInPlay = new CardSet();
        private TurnState _turnState;

        public TurnScope(Player player, 
            Supply supply, 
            int turnNumber, 
            IEventAggregator eventAggregator, 
            IEnumerable<Player> reactingPlayers, 
            TrashPile trash, 
            IContainer container) 
            : this(player, supply, turnNumber, eventAggregator)
        {
            _trash = trash;
            container.Configure(cfg => cfg.For<ITurnScope>().Use(this));
            _container = container;
            reactingPlayers.ForEach(p => _reactionScopes.Add(new ReactionScope(eventAggregator, player, p, this)));
        }

        public TurnScope(Player player, Supply supply, IEventAggregator eventAggregator)
            : this(player, supply, 1, eventAggregator)
        {
        }

        public TurnScope(Player player, 
            Supply supply, 
            int turnNumber, 
            IEventAggregator eventAggregator)
        {
            _turnState = new TurnState(1, 1, 0);
            if (supply == null)
                throw new ArgumentNullException("Supply cannot be null.");

            _player = player;
            _eventAggregator = eventAggregator;
            _eventAggregator.Register(this);
            Supply = supply;
            TurnNumber = turnNumber;
            State = new StateStack();
        }

        public Supply Supply { get; private set; }
        public StateStack State { get; private set; }
        public ITurnScope GetTurnScope { get { return this; } }

        public int TurnNumber { get; private set; }

        public IEnumerable<IReactionScope> ReactionScopes { get { return _reactionScopes; } }

        public Player Player
        {
            get { return _player; }
        }

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
            _turnState = _turnState.RegisterBuy(cardToPurchase.Create().Cost);
            _eventAggregator.Publish(new PlayerGainedCardEvent(this));
        }

        public void PlayTreasures(CardSet treasuresToPlay)
        {
            _player.PlayTreasures(treasuresToPlay, this);
        }

        public int Actions { get { return _turnState.Actions; }}

        public int Buys { get { return _turnState.Buys; } }

        public int Coins { get { return _turnState.Coins; } }

        public CardSet TreasuresInHand { get { return Player.Hand.Treasures(); } }

        public void Publish(IGameMessage @event)
        {
            _eventAggregator.Publish(@event);
        }

        public void PlayAction(Card action)
        {
            Hand.Remove(action);
            _cardsInPlay.Add(action, this);
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
            return String.Format("Player {0}: {1} Actions, {2} Buys, ({3}) Coins. H: {4}, P: {5}, Di: {6}, Dk {7}", Player.Name, Actions, Buys, Coins, Hand.Count(), _cardsInPlay.Count(), _player.DiscardPile.Count(), _player.Deck.Count());
        }

        public void TrashCard(Card card)
        {
            Player.Hand.TrashCard(card, _trash, this);
        }

        public void GainCardFromSupply(CardType card)
        {
            Player.GainCardFromSupply(card, this);
        }

        public T GetInstance<T>()
        {
            return _container.GetInstance<T>();
        }

        public void Dispose()
        {
            _eventAggregator.Unregister(this);
            _reactionScopes.ForEach(s => s.Dispose());
        }

        public void Handle(IGameMessage @event)
        {
            Player.Handle(@event, this);
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