using Dominion.GameEvents;

namespace Dominion.Cards.BasicSet.Actions.Remodel
{
    public class CardSelectedToRemodelResponse : GameEventResponse

    {
        public Card Card { get; set; }

        public CardSelectedToRemodelResponse(Card card, ITurnScope scope) : base(scope)
        {
            Description = card.Name;
            Card = card;
        }

        public override void Execute()
        {
            TurnScope.Publish(this);
        }
    }
}