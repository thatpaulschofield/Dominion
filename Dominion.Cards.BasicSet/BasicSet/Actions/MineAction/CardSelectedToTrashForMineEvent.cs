using System;
using Dominion.GameEvents;

namespace Dominion.Cards.BasicSet.Actions.MineAction
{
    public class CardSelectedToTrashForMineEvent : GameMessage
    {
        private readonly Card _card;

        public CardSelectedToTrashForMineEvent(Card card, ITurnScope turnScope) : base(turnScope)
        {
            if (card == null)
            {
                throw new ArgumentNullException("card");
            }
            _card = card;

            Description = string.Format("{0} trashed {1}", turnScope.Player.Name, card.Name);
        }

        public Card CardToTrash
        {
            get { return _card; }
        }
    }
}