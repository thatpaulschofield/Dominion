using System.Collections.Generic;
using Dominion.GameEvents;

namespace Dominion
{
    public abstract class AbstractActionScope : IHandleEvents
    {
        protected readonly List<ICardCostModifierEffect> _costModifiers = new List<ICardCostModifierEffect>();
        protected Player _player;
        protected TrashPile _trashPile;
        protected IEventAggregator _eventAggregator;

        protected AbstractActionScope(Supply supply, IEventAggregator eventAggregator, TrashPile trashPile, int turnNumber)
        {
            Supply = supply ?? new Supply();
            _eventAggregator = eventAggregator ?? new EventAggregator();
            eventAggregator.Register(this);
            _trashPile = trashPile ?? new TrashPile();
            TurnNumber = turnNumber;
        }
        public Supply Supply { get; private set; }

        public virtual IActingPlayer Player { get { return _player; } }

        public int TurnNumber { get; private set; }

        public virtual void Handle(IGameMessage @event)
        {
            if (@event is ICardCostModifierEffect)
            {
                _costModifiers.Add(@event as ICardCostModifierEffect);
            }
        }

        public bool CanHandle(IGameMessage @event)
        {
            return @event is ICardCostModifierEffect;
        }

        public void Publish(IGameMessage @event)
        {
            _eventAggregator.Publish(@event);
        }
    }
}