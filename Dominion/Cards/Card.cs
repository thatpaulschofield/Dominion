using System;
using Dominion.GameEvents;

namespace Dominion.Cards
{
    public class Card
    {
        public Card(CardType type, int coins = 0, bool isAction = false, bool isAttack = false, bool isTreasure = false, int baseVictoryPoints = 0, bool isVictory = false, int baseCost = 0, string name=null)
        {
            this.CardType = type;
            Coins = coins;
            IsAction = isAction;
            IsAttack = isAttack;
            IsTreasure = isTreasure;
            IsVictory = isVictory;
            BaseVictoryPoints = baseVictoryPoints;
            BaseCost = baseCost;
            Name = String.IsNullOrEmpty(name) ? this.ToString() : name;
        }

        public CardType CardType { get; protected set; }
        public int BaseVictoryPoints { get; protected set; }
        public Money Coins { get; protected set; }
        public Money BaseCost { get; protected set; }
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
            return this.BaseVictoryPoints;
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

        #region IHandleEvents
        public virtual void Handle(IMessage @event)
        {
            
        }

        public virtual bool CanHandle(IMessage @event)
        {
            return false;
        }
        #endregion

        public void Reveal(IReactionScope externalEventScope, Player player)
        {
            externalEventScope.Publish(new PlayerRevealedCardEvent(externalEventScope, this));
            OnRevealed(externalEventScope, player);
        }

        public virtual void OnRevealed(IReactionScope externalEventScope, Player player)
        {
            
        }
    }
}