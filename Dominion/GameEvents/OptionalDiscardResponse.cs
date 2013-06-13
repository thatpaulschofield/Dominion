using Dominion.GameEvents;

namespace Dominion.Cards.BasicSet.Actions
{
    public class OptionalDiscardResponse : GameEventResponse<OptionallyDiscard>
    {
        private readonly Card _card;
        private readonly OptionallyDiscard _optionallyDiscard;

        public OptionalDiscardResponse(ITurnScope turnScope, Card card, OptionallyDiscard optionallyDiscard) : base(turnScope)
        {
            _card = card;
            _optionallyDiscard = optionallyDiscard;
            Description = "Discard " + card.Name;
        }

        public override void Execute()
        {
            _optionallyDiscard.CardWasDiscarded();
            TurnScope.Player.Discard(_card, TurnScope);
            TurnScope.Player.Handle(_optionallyDiscard, TurnScope);
        }
    }
}