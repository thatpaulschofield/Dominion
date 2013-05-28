using System;

namespace Dominion.Cards
{
    public class Card
    {
        public Card(CardType type, int coins = 0, bool isAction = false, bool isAttack = false, bool isTreasure = false, int victoryPoints = 0, bool isVictory = false, int cost = 0, string name=null)
        {
            if (String.IsNullOrEmpty(name))
                throw new ArgumentNullException("name");
            this.CardType = type;
            Coins = coins;
            IsAction = isAction;
            IsAttack = isAttack;
            IsTreasure = isTreasure;
            IsVictory = isVictory;
            VictoryPoints = victoryPoints;
            Cost = cost;
            Name = name;
        }

        public CardType CardType { get; protected set; }
        public int VictoryPoints { get; protected set; }
        public Money Coins { get; protected set; }
        public Money Cost { get; protected set; }
        public bool IsTreasure { get; protected set; }
        public string Name { get; protected set; }
        public bool IsAction { get; protected set; }
        public bool IsAttack { get; protected set; }
        public bool IsVictory { get; protected set; }
        
        public override bool Equals(object obj)
        {
            var other = obj as Card;
            if (other == null)
                return false;

            return this.GetType() == obj.GetType()
                   && this.Name == other.Name;
        }

        public override string ToString()
        {
            return this.Name;
        }

        public virtual int Score(ITurnScope turnScope)
        {
            return this.VictoryPoints;
        }

        protected bool Equals(Card other)
        {
            return string.Equals(Name, other.Name);
        }

        public override int GetHashCode()
        {
            return (Name != null ? Name.GetHashCode() : 0);
        }

        public virtual void PlayAsAction(ITurnScope turnScope)
        {
            
        }

        public static implicit operator CardType(Card card)
        {
            return card.CardType;
        }
    }
}