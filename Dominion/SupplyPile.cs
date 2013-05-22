using Dominion.Cards;

namespace Dominion
{
    public class SupplyPile
    {
        public int Count { get; private set; }

        public SupplyPile(int count, CardType cardType)
        {
            Count = count;
            this.Type = cardType;
        }

        public Card Draw()
        {
            if (Count > 0)
            {
                Count--;
                return Type.Create();
            }
            throw new SupplyEmptyException();
        }

        public CardType Type { get; private set; }
    }
}