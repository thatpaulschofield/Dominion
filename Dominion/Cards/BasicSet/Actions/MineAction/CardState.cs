using System;
using System.Collections.Generic;
using System.Linq;
using Dominion.GameEvents;

namespace Dominion.Cards.BasicSet.Actions.MineAction
{
    public class CardState<TITEM, TGAMERESPONSE> : CardState<TITEM> where TGAMERESPONSE : GameEventResponse<TITEM>
    {
        public CardState(StateMachineGameMessage statefulMessage, TITEM item, TGAMERESPONSE response) : base(statefulMessage, item)
        {
            response.WithItem(item);
            _action = (scope, i) => response.Execute();
            Description = response.Description;
        }
    }

    public class CardState<T> : CardState
    {
        protected Action<ITurnScope, T> _action = (scope, item) => { };
        private Action<IReactionScope, T> _reaction = (scope, item) => { };

        public CardState(StateMachineGameMessage statefulMessage, T item) : base(statefulMessage)
        {
            Item = item;
        }

        public T Item { get; private set; }


        public virtual void PlayAsAction(ITurnScope scope)
        {
            _action(scope, Item);
        }

        public virtual void PlayAsReaction(IReactionScope scope)
        {
            _reaction(scope, Item);
        }

        public virtual CardState WithAction(Action<ITurnScope, T> action)
        {
            _action = action;
            return this;
        }

        public CardState WithReaction(Action<IReactionScope, T> reaction)
        {
            _reaction = reaction;
            return this;
        }

        public override void Execute(ITurnScope scope)
        {
            _action(scope, Item);
        }

        public override void Execute(IReactionScope reactionScope)
        {
            _reaction(reactionScope, Item);
        }

        public override string ToString()
        {
            return Description;
        }
    }

    public class CardState
    {
        private readonly List<CardState> _transitions = new List<CardState>();
        private readonly StateMachineGameMessage _message;
        private Func<ITurnScope, IEnumerable<CardState>> _responseOptions = scope => new List<CardState>();
        private Func<IReactionScope, IEnumerable<CardState>> _reactionOptions = scope => new List<CardState>();

        public CardState(StateMachineGameMessage statefulMessage)
        {
            _message = statefulMessage;
        }

        public CardState TransitionsTo(CardState endState)
        {
            _transitions.Add(endState);
            return this;
        }

        public string Description { get; set; }


        public virtual IEnumerable<IEventResponse> GetAvailableResponses(ITurnScope scope)
        {
            return _responseOptions(scope)
                .Select(t => new StateTransition(_message, this, t));
        }
    
        public virtual IEnumerable<IEventResponse> GetAvailableReactions(IReactionScope scope)
        {
            return _reactionOptions(scope).Select(to => new StateTransition(_message, this, to));

        }

        public CardState HasDescription(string description)
        {
            this.Description = description;
            return this;
        }

        public CardState WithResponseOptions(Func<ITurnScope, IEnumerable<CardState>> actions)
        {
            _responseOptions = actions;
            return this;
        }

        public CardState WithReactionOptions(Func<IReactionScope, IEnumerable<CardState>> reactions)
        {
            _reactionOptions = reactions;
            return this;
        }

        public virtual void Execute(ITurnScope scope)
        {

        }

        public virtual void Execute(IReactionScope reactionScope)
        {
            
        }

        public override string ToString()
        {
            return Description;
        }

        public void Execute(IActionScope actionScope)
        {
            if (actionScope is IReactionScope)
            {
                Execute(actionScope as IReactionScope);
                return;
            }

            if (actionScope is ITurnScope)
            {
                Execute(actionScope as ITurnScope);
            }
        }

        public void Execute(CardState transitioningFrom, IActionScope actionScope)
        {
            actionScope.State.Push(transitioningFrom);
            Execute(actionScope);
            actionScope.Publish(_message);
            actionScope.State.Pop();
        }
    }
}