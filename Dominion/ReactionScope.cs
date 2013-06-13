using System;
using Dominion.Cards;
using Dominion.Cards.BasicSet.Actions;
using Dominion.GameEvents;
using Dominion.Tests.GameEvents;

namespace Dominion
{
    public class ReactionScope : AbstractActionScope, IReactionScope, IHandleEvents
    {
        private readonly Player _actingPlayer;
        private readonly Player _reactingPlayer;
        private readonly ITurnScope _originatingTurnScope;
        private EventFilterPipeline _eventPipeline;

        public ReactionScope(IEventAggregator eventAggregator, 
            Player actingPlayer, 
            Player reactingPlayer, 
            ITurnScope originatingTurnScope, 
            TrashPile trashPile,
            Supply supply) : base(supply, eventAggregator, trashPile, 1)
        {
            _player = reactingPlayer;
            _actingPlayer = actingPlayer;
            _reactingPlayer = reactingPlayer;
            _originatingTurnScope = originatingTurnScope;
            _trashPile = trashPile;
            _eventPipeline = new EventFilterPipeline(_reactingPlayer);
        }

        public ITurnScope OriginatingTurnScope { get; protected set; }
        public Player OriginatingPlayer { get { return _actingPlayer; } }
        public override IActingPlayer Player { get { return _reactingPlayer; } }
        public Player ReactingPlayer { get { return _reactingPlayer; } }
        public ITurnScope GetTurnScope { get; protected set; }
        public CardSet Deck { get { return new CardSet(_actingPlayer.Deck); } }

        public Card RevealCardFromDeck()
        {
            return ReactingPlayer.RevealCardFromTopOfDeck(this);
        }

        public void PutCardsIntoDiscardPile(CardSet cards)
        {
            ReactingPlayer.PlaceCardsInDiscardPile(cards);
        }

        public void RevealCard(Card card)
        {
            _reactingPlayer.RevealCard(card, this);
        }

        public void PutCardFromHandOnTopOfDeck(Card card)
        {
            Hand.Remove(card);
            ReactingPlayer.PlaceCardOnTopOfDeck(card);
        }

        public void DrawCardsIntoHand(int count)
        {
            ReactingPlayer.DrawIntoHand(1, this);
        }

        public void GainCardFromSupply(CardType card)
        {
            Player.GainCardFromSupply(card, this);
        }

        public Money GetPrice(Card card)
        {
            return _originatingTurnScope.GetPrice(card);
        }

        public void PutCardOnTopOfDeck(Card card)
        {
            ReactingPlayer.PlaceCardOnTopOfDeck(card);
        }

        public void PutCardInTrash(Card card)
        {
            _trashPile.Add(card, this);
            Publish(new PlayerTrashedCardEvent(this, card));
        }

        public override void Handle(IGameMessage @event)
        {
            if (base.CanHandle(@event))
                base.Handle(@event);

            if (CanHandle(@event))
            {
                _eventPipeline.Handle(@event, this);
            }
        }

        public bool CanHandle(IGameMessage @event)
        {
            return true;
        }

        public void RegisterEventFilter(ExternalEventFilter filter)
        {
            _eventPipeline.RegisterEventFilter(filter);
        }

        public Hand Hand { get { return _reactingPlayer.Hand; } }

        public void PerformBuy(CardType cardType)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            _eventAggregator.Unregister(this);
        }
    }
}