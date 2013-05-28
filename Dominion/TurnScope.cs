using System;
using System.Linq;
using Dominion.Cards;
using Dominion.GameEvents;

namespace Dominion
{
    public class TurnScope : ITurnScope
    {
        private readonly CardSet _cardsToDiscard = new CardSet();
        private readonly Player _player;
        private readonly IEventAggregator _eventAggregator;

        private readonly CardSet _cardsInPlay = new CardSet();
        private TurnState _turnState;

        public TurnScope(Player player, Supply supply, IEventAggregator eventAggregator) : this(player, supply, 1, eventAggregator)
        {
        }

        public TurnScope(Player player, Supply supply, int turnNumber, IEventAggregator eventAggregator)
        {
            _turnState = new TurnState(1, 1, 0);
            if (supply == null)
                throw new ArgumentNullException("Supply cannot be null.");

            _player = player;
            _eventAggregator = eventAggregator;
            Supply = supply;
            TurnNumber = turnNumber;
        }

        public Supply Supply { get; private set; }

        public int TurnNumber { get; private set; }

        public Player Player
        {
            get { return _player; }
        }

        public Hand Hand { get { return Player.Hand; } }

        public void Discard(CardSet cardsToDiscard)
        {
            _cardsToDiscard.AddRange(cardsToDiscard);
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
            _player.Discard(_cardsToDiscard, this);
            _player.DrawNewHand(this);
        }

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

        public void Publish(GameMessage @event)
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
            return String.Format("Player {0}: {1} Actions, {2} Buys, ({3}) Coins. H: {4}, P: {5}, D: {6}", Player.Name, Actions, Buys, Coins, Hand.Count(), _cardsInPlay.Count(), _player.Deck.Count());
        }
    }
}