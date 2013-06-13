using Dominion.Cards.BasicSet.Actions.Remodel;
using Dominion.GameEvents;

namespace Dominion.Cards.BasicSet.BasicSet.Actions.Remodel
{
    public class CardSelectedToRemodelResponse : GameEventResponseWithItem<Card, PickCardToRemodelCommand>
    {
        public CardSelectedToRemodelResponse(ITurnScope scope, Card card) : base(card, scope)
        {
            Description = card.Name;
            Item = card;
        }

        public override void Execute()
        {
            TurnScope.Publish(this);
        }
    }
}