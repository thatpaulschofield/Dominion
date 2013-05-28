using Dominion.Cards;

namespace Dominion
{
    public class SupplyPileBuilder
    {
        private readonly IEventAggregator _eventAggregator;
        private int _count;
        private CardType _type;

        public SupplyPileBuilder(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
        }

        public SupplyPileBuilder OfSize(int count)
        {
            _count = count;
            return this;
        }

        public SupplyPileBuilder OfType(CardType type)
        {
            _type = type;
            return this;
        }

        public static implicit operator SupplyPile(SupplyPileBuilder builder)
        {
            return new SupplyPile(builder._count, builder._type, builder._eventAggregator);
        }
    }
}