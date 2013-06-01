using System;
using System.Collections.Generic;
using Dominion.GameEvents;
using System.Linq;
namespace Dominion.Cards.BasicSet.Actions.MineAction
{
    public class CardStateDsl<TITEM, TEVENTRESPONSE> : CardStateDsl where TEVENTRESPONSE : GameEventResponse<TITEM>
    {
        private readonly CardStateDsl _parent;
        private readonly Func<ITurnScope, IEnumerable<TITEM>> _selectFunction;
        private Func<TITEM, string> _descriptionSelector;
        private Action<ITurnScope, TITEM> _onExecuted = (scope, arg2) => { };

        public CardStateDsl(StateMachineGameMessage message) : base(message)
        {

        }

        public CardStateDsl(CardStateDsl parent,
            Func<ITurnScope, IEnumerable<TITEM>> selectFunction, 
            StateMachineGameMessage stateMachineGameMessage)
            : base(stateMachineGameMessage)
        {
            if (selectFunction == null)
                throw new ArgumentNullException("selectFunction");

            _parent = parent ?? this;
            _selectFunction = selectFunction;
        }

        public CardStateDsl<TITEM, TEVENTRESPONSE> AndForEach<T1>(Action<CardStateDsl<TITEM,TEVENTRESPONSE>> action)
        {
            return this;
        }

        public CardStateDsl<TITEM, TEVENTRESPONSE> WithDescription(Func<TITEM, string> func)
        {
            _descriptionSelector = func;
            return this;
        }

        public void WhenExecuted(Action<ITurnScope, TITEM> onExecuted)
        {
            _onExecuted = onExecuted;
        }

        public override Func<ITurnScope, IEnumerable<CardState>> ResponseOptions
        {
            get
            {
                return scope => _selectFunction(scope).Select(item =>
                    {
                        CardState<TITEM> option = Message.BuildCardState<TITEM, TEVENTRESPONSE>(item);
       
                        if (_childActionsDsl != null)
                        {
                            option.WithResponseOptions(_childActionsDsl.ResponseOptions);

                        }
                        return option;
                    }
                );
            }
        }

        public CardStateDsl And()
        {
            return _parent;
        }

    }

    public class CardStateDsl
    {
        protected readonly StateMachineGameMessage Message;
        private string _description;
        readonly List<ActionDsl> _childActionDsls = new List<ActionDsl>();
        protected CardStateDsl _childActionsDsl;

        public CardStateDsl(StateMachineGameMessage message)
        {
            Message = message;
        }

        public CardStateDsl HasDescription(string description)
        {
            _description = description;
            return this;
        }

        public CardStateDsl<TITEM, TEVENTRESPONSE> WithAResponseForEach<TITEM, TEVENTRESPONSE>(
            Func<ITurnScope, IEnumerable<TITEM>> selectFunction,
            Action<CardStateDsl<TITEM, TEVENTRESPONSE>> configAction = null) where TEVENTRESPONSE : GameEventResponse<TITEM>
        {
            configAction = configAction ?? (x => { });
            var child = new CardStateDsl<TITEM, TEVENTRESPONSE>(this, selectFunction, Message);
            configAction(child);
            _childActionsDsl = child;
            return child;
        }

        public virtual Func<ITurnScope, IEnumerable<CardState>> ResponseOptions
        {
            get { return scope => new List<CardState>(); }
        }


        public ActionDsl WithTheResponse(string description)
        {
            var child = new ActionDsl(this, Message);
            _childActionDsls.Add(child);
            return child;
        }

        public virtual CardState Build()
        {
            var cardState = new CardState(Message)
                .HasDescription(_description);
            foreach (var childActionDsl in _childActionDsls)
            {
                cardState.TransitionsTo(childActionDsl.Build());
            }

            cardState.WithResponseOptions(_childActionsDsl.ResponseOptions);

            return cardState;
        }

        protected virtual CardState CreateInstance()
        {
            return new CardState(Message);
        }
    }
}