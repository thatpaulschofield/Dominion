using System;
using System.Collections.Generic;
using System.Linq;
using Dominion.GameEvents;
using Stateless;

namespace Dominion.Cards.BasicSet.Actions.MineAction
{
    public class Saga
    {
        public virtual void Handle(IGameMessage @event)
        {
            
        }

        public Guid Id { get; set; }

        public virtual bool IsComplete
        {
            get { return false; }
        }
    }
    public class Saga<TSTATE,TTRIGGER>  : Saga, IDisposable 
    {
        protected readonly IEventAggregator _eventAggregator;
        readonly StateMachine<TSTATE,TTRIGGER> _stateMachine;
        readonly MessageMaps<TSTATE, TTRIGGER> _messageMaps;
        public Saga(IEventAggregator eventAggregator, TSTATE initialState)
        {
            _eventAggregator = eventAggregator;
            _stateMachine = new StateMachine<TSTATE, TTRIGGER>(initialState);
            _messageMaps = new MessageMaps<TSTATE, TTRIGGER>(this, _stateMachine);
        }

        public StateMachine<TSTATE, TTRIGGER>.StateConfiguration Configure(TSTATE state)
        {
            return _stateMachine.Configure(state);
        }

        public TSTATE _state;

        public override void Handle(IGameMessage @event)
        {
                _messageMaps.Fire(@event);
        }

        public override bool IsComplete
        {
            get { return this._stateMachine.IsInState(this.CompletedState); }
        }

        public TSTATE CompletedState { get; set; }

        public void Dispose()
        {
        }

        protected StateMachine<TSTATE,TTRIGGER>.TriggerWithParameters<TPARAMETER> 
            Event<TPARAMETER>(TTRIGGER trigger) where TPARAMETER : class
        {
            return _messageMaps.Message<TPARAMETER>().MapsTo(trigger);
        }

    }

    public class MessageMaps<TSTATE, TTRIGGER>
    {
        private readonly Saga<TSTATE, TTRIGGER> _saga;
        private readonly StateMachine<TSTATE, TTRIGGER> _stateMachine;
        readonly Dictionary<Type, MessageMap> _messageMaps = new Dictionary<Type, MessageMap>();
        public MessageMaps(Saga<TSTATE, TTRIGGER> saga, StateMachine<TSTATE, TTRIGGER> stateMachine)
        {
            _saga = saga;
            _stateMachine = stateMachine;
        }

        public MessageMap<TSTATE, TTRIGGER, TPARAMETER> Message<TPARAMETER>()
        {
            if (_messageMaps.ContainsKey(typeof (TPARAMETER)))
            {
                return (MessageMap<TSTATE, TTRIGGER, TPARAMETER>) _messageMaps[typeof (TPARAMETER)];
            }
            var map = new MessageMap<TSTATE, TTRIGGER, TPARAMETER>(_saga, _stateMachine);
            _messageMaps.Add(typeof (TPARAMETER), map);
            return map;
        }

        public void Fire(IGameMessage @event)
        {
            if (_messageMaps.ContainsKey(@event.GetType()))
                _messageMaps[@event.GetType()].Fire(@event);
        }
    }

    public abstract class MessageMap
    {

        public virtual void Fire(IGameMessage @event)
        {
        }

        public abstract object TriggerEnum { get; }

        public abstract object TriggerObject { get; }
    }
    public class MessageMap<TSTATE, TTRIGGER, TMESSAGE> : MessageMap
    {
        private readonly Saga<TSTATE, TTRIGGER> _saga;
        private readonly StateMachine<TSTATE, TTRIGGER> _stateMachine;
        private TTRIGGER _triggerEnum;
        public StateMachine<TSTATE, TTRIGGER>.TriggerWithParameters<TMESSAGE> Trigger { get; protected set; }
        
        public MessageMap(Saga<TSTATE, TTRIGGER> saga, StateMachine<TSTATE, TTRIGGER> stateMachine)
        {
            _saga = saga;
            _stateMachine = stateMachine;
        }

        public StateMachine<TSTATE, TTRIGGER>.TriggerWithParameters<TMESSAGE> MapsTo(TTRIGGER trigger)
        {
            _triggerEnum = trigger;
            if (Trigger != null)
                return Trigger;

            Trigger = _stateMachine.SetTriggerParameters<TMESSAGE>(_triggerEnum);
            return Trigger;
        }

        public override void Fire(IGameMessage @event)
        {
            _stateMachine.Fire(Trigger, (TMESSAGE)@event);
        }

        public override object TriggerEnum
        {
            get { return _triggerEnum; }
        }

        public override object TriggerObject
        {
            get { return Trigger; }
        }
    }

 
}