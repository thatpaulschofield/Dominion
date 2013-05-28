using Dominion.GameEvents;

namespace Dominion
{
    internal class ThreeSupplyPilesDepletedEndGameCondition : EndGameCondition, IHandleEvents<SupplyPileDepletedEvent>
    {
        private readonly IEventAggregator _eventAggregator;
        private bool _conditionMet;
        private int _depletedStacksCount;

        public ThreeSupplyPilesDepletedEndGameCondition(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _eventAggregator.Register(this);
        }

        protected override bool IsConditionMet()
        {
            return _depletedStacksCount >= 3;
        }

        public void Handle(SupplyPileDepletedEvent @event)
        {
            _depletedStacksCount++;
        }

        public void Handle(IMessage @event)
        {
            Handle(@event as SupplyPileDepletedEvent);
        }

        public bool CanHandle(IMessage @event)
        {
            return @event is SupplyPileDepletedEvent;
        }
    }
}