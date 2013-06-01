using System;
using Dominion.GameEvents;

namespace Dominion.Cards.BasicSet.Actions.MineAction
{
    public class CardTrashedForMineEvent : GameMessage
    {
        private readonly Card _card;

        public CardTrashedForMineEvent(ITurnScope turnScope, Card card) : base(turnScope)
        {
            if (card == null)
            {
                throw new ArgumentNullException("card");
            }
            _card = card;

            Description = string.Format("{0} trashed {1}", turnScope.Player.Name, card.Name);
        }

        public Card TrashedCard
        {
            get { return _card; }
        }
    }
}