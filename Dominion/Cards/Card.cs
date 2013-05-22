namespace Dominion.Cards
{
    public class Card
    {
        public Card(CardType type, int coins = 0, bool isTreasure = false, int victoryPoints = 0, int cost = 0)
        {
            this.CardType = type;
            Coins = coins;
            IsTreasure = isTreasure;
            VictoryPoints = victoryPoints;
            Cost = cost;
        }

        public CardType CardType { get; protected set; }

        public int VictoryPoints { get; protected set; }

        public int Coins { get; protected set; }

        public bool IsTreasure { get; protected set; }

        public int Cost { get; protected set; }

        public void Into(CardSet hand)
        {
            throw new System.NotImplementedException();
        }

        public override bool Equals(object obj)
        {
            var other = obj as Card;
            if (other == null)
                return false;

            return this.GetType() == obj.GetType()
                   && this.VictoryPoints == other.VictoryPoints
                   && this.Coins == other.Coins
                   && this.Cost == other.Cost
                   && this.IsTreasure == other.IsTreasure;
        }
    }
}