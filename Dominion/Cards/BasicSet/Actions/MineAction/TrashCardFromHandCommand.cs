using Dominion.GameEvents;

namespace Dominion.Cards.BasicSet.Actions.MineAction
{
    internal class TrashCardFromHandCommand : GameMessage
    {
        private readonly Card _cardToTrash;

        public TrashCardFromHandCommand(Card cardToTrash, ITurnScope turnScope) : base(turnScope)
        {
            _cardToTrash = cardToTrash;
        }
    }
}