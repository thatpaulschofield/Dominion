using Dominion.Cards.BasicSet.VictoryCards;
using Dominion.GameEvents;

namespace Dominion
{
    internal class ProvinceStackDepletedEndGameCondition : EndGameCondition, IHandleEvents<SupplyPileDepletedEvent>
    {
        private readonly IEventAggregator _eventAggregator;
        private bool _conditionMet;

        public ProvinceStackDepletedEndGameCondition(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _eventAggregator.Register(this);
        }

        protected override bool IsConditionMet()
        {
            return _conditionMet;
        }

        public void Handle(SupplyPileDepletedEvent @event)
        {
            if (@event.Type.Equals(Victory.Province))
            {
                _conditionMet = true;
            }
        }

        public void Handle(IGameMessage @event)
        {
            Handle(@event as SupplyPileDepletedEvent);
        }

        public bool CanHandle(IGameMessage @event)
        {
            return @event is SupplyPileDepletedEvent;
        }
    }
}