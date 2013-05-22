using System.Linq;
using Dominion.AI;
using Dominion.Cards;

namespace Dominion.GameEvents
{
    public class ActionPhase : GameEvent
    {
        private readonly TurnScope _turnScope;

        public ActionPhase(TurnScope turnScope, CardSet availableActions)
        {
            _turnScope = turnScope;
            AvailableActions = availableActions;
        }

        public CardSet AvailableActions { get; private set; }
        public GameEventResponse PlayAction(Card card)
        {
            if(AvailableActions.Contains(card))
                return new PlayActionResponse(_turnScope, card);

            return new SkipActionPhaseResponse(_turnScope);
        }

        public override GameEventResponse GetDefaultResponse()
        {
            if (AvailableActions.Any())
                return new PlayActionResponse(_turnScope, AvailableActions[0]);

            return new SkipActionPhaseResponse(_turnScope);
        }
    }
}