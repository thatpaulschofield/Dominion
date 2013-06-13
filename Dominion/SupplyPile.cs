using Dominion.Cards;

namespace Dominion
{
    public class SupplyPile
    {
        private readonly IEventAggregator _eventAggregator;
        public int Count { get; private set; }

        public SupplyPile(int count, CardType cardType, IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            Count = count;
            this.Type = cardType;
        }

        public Card Draw(IActionScope turnScope)
        {
            if (Count > 0)
            {
                Count--;
                if (Count == 0)
                    _eventAggregator.Publish(new SupplyPileDepletedEvent(this.Type, turnScope));
                return Type.Create();
            }
            throw new SupplyEmptyException();
        }

        public CardType Type { get; private set; }

        public override bool Equals(object obj)
        {
            var other = obj as SupplyPile;
            if (other == null)
                return false;

            return this.Type == other.Type;
        }
    }
}