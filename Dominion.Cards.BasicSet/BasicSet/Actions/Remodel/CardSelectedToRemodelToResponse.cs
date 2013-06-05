using Dominion.GameEvents;

namespace Dominion.Cards.BasicSet.Actions.Remodel
{
    public class CardSelectedToRemodelToResponse : GameEventResponseWithItem<Card, PickCardToRemodelToCommand>
    {
        public CardSelectedToRemodelToResponse(Card card, ITurnScope turnScope)
            : base(card, turnScope)
        {
            Description = Item.Name;
        }

        public override void Execute()
        {
            TurnScope.Publish(this);
        }
    }
}