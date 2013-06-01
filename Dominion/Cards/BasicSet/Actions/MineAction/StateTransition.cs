using Dominion.GameEvents;

namespace Dominion.Cards.BasicSet.Actions.MineAction
{
    public class StateTransition : IEventResponse
    {
        private readonly StateMachineGameMessage _message;
        private readonly CardState _transitioningFrom;
        private readonly CardState _transitionTo;
        
        public StateTransition(StateMachineGameMessage message, CardState transitioningFrom, CardState transitionTo)
        {
            _message = message;
            _transitioningFrom = transitioningFrom;
            _transitionTo = transitionTo;
            Description = transitionTo.Description;
        }

        public string Description { get; private set; }

        public void Execute()
        {
            _message.TransitionToState(_transitioningFrom, _transitionTo);
        }
    }
} 