using System;
using Dominion.Cards;
using Dominion.Cards.BasicSet.Actions;
using Dominion.GameEvents;

namespace Dominion
{
    public class ReactionScope : IReactionScope, IHandleEvents
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly Player _originatingPlayer;
        private readonly Player _receivingPlayer;
        private readonly ITurnScope _originatingTurnScope;
        private EventFilterPipeline _eventPipeline;

        public ReactionScope(IEventAggregator eventAggregator, Player originatingPlayer, Player receivingPlayer, ITurnScope originatingTurnScope)
        {
            _eventAggregator = eventAggregator;
            _originatingPlayer = originatingPlayer;
            _receivingPlayer = receivingPlayer;
            _originatingTurnScope = originatingTurnScope;
            eventAggregator.Register(this);
            _eventPipeline = new EventFilterPipeline(_receivingPlayer);
        }

        public ITurnScope OriginatingTurnScope { get; private set; }
        public Player OriginatingPlayer { get { return _originatingPlayer; } }
        public Player ReceivingPlayer { get { return _receivingPlayer; } }
        public void Publish(IGameMessage @event)
        {
            _eventAggregator.Publish(@event);
        }

        public void RevealCard(Card card)
        {
            _receivingPlayer.RevealCard(card, this);
        }

        public void Dispose()
        {
            _eventAggregator.Unregister(this);
        }

        public void Handle(IGameMessage @event)
        {
            if (CanHandle(@event))
            {
                _eventPipeline.Handle(@event, this);
                //_receivingPlayer.Handle(@event, this);
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

        public Player Player {
            get { return _receivingPlayer; }
        }

        public Hand Hand { get { return _receivingPlayer.Hand; } }

        public void PerformBuy(CardType cardType)
        {
            throw new NotImplementedException();
        }
    }
}