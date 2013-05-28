using System;
using System.Linq;
using Dominion.Cards;

namespace Dominion.GameEvents
{
    public class ActionPhase : GameCommand
    {
        public ActionPhase(ITurnScope turnScope, CardSet availableActions) : base(turnScope)
        {
            Description = String.Format("{0}, select an action to play", turnScope.Player.Name);
            AvailableActions = availableActions;
            availableActions.ToList().ForEach(actionCard => _availableResponses.Add(new PlayActionResponse(turnScope, actionCard)));
            if (_availableResponses.Any())
                _availableResponses.Add(new SkipActionPhaseResponse(turnScope));
        }

        public CardSet AvailableActions { get; private set; }
        public GameEventResponse PlayAction(Card card)
        {
            if(AvailableActions.Contains(card))
                return new PlayActionResponse(TurnScope, card);

            return new SkipActionPhaseResponse(TurnScope);
        }

        public override GameEventResponse GetDefaultResponse()
        {
            if (AvailableActions.Any())
                return new PlayActionResponse(TurnScope, AvailableActions[0]);

            return new SkipActionPhaseResponse(TurnScope);
        }

        public override string ToString()
        {
            string actions = AvailableActions.Aggregate("", (x, c) => x + " - " + c.Name);
            return String.Format("Action phase for player {0}. Available actions: [{1}]", TurnScope.Player.Name, actions);
        }
    }
}