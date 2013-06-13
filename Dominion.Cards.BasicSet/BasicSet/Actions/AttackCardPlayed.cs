using System;
using Dominion.GameEvents;

namespace Dominion.Cards.BasicSet.Actions
{
    public class AttackCardPlayed : GameMessage
    {
        private readonly Card _attackCard;

        public AttackCardPlayed(Card attackCard, ITurnScope turnScope) : base(turnScope)
        {
            Description = String.Format("{0} played a {1}", turnScope.Player.Name, attackCard.Name);
            _attackCard = attackCard;
        }

        public override string ToString()
        {
            return Description;
        }
    }
}