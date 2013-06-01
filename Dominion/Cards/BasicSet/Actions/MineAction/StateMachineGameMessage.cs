using System;
using System.Collections.Generic;
using Dominion.GameEvents;

namespace Dominion.Cards.BasicSet.Actions.MineAction
{
    public class StateMachineGameMessage : GameMessage
    {
        private Lazy<CardState> _state;
        private readonly CardStateDsl _cardStateDsl;

        public StateMachineGameMessage(ITurnScope scope)
            : base(scope)
        {
            _cardStateDsl = new CardStateDsl(this);
            _state = new Lazy<CardState>(() => _cardStateDsl.Build());
            base.GetAvailableResponses = () => _state.Value.GetAvailableResponses(scope);
        }

        public CardState State
        {
            get { return _state.Value; }
            set { _state = new Lazy<CardState>(() => value); }
        }

        public override IEnumerable<IEventResponse> GetAvailableReactions(IReactionScope scope)
        {
            return State.GetAvailableReactions(scope);
        }

        public void TransitionToState(CardState transitioningFrom, CardState newState)
        {
            State = newState;
            newState.Execute(transitioningFrom, this.ActionScope);
        }

        protected CardStateDsl Initially()
        {
            return _cardStateDsl;
        }

        public CardState<TITEM> BuildCardState<TITEM, TEVENTRESPONSE>(TITEM item) where TEVENTRESPONSE : GameEventResponse<TITEM>
        {
            return new CardState<TITEM, TEVENTRESPONSE>(this, item, TurnScope.GetInstance<TEVENTRESPONSE>());
        }
    }
}