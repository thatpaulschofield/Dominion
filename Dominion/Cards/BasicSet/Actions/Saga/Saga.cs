using System;
using Dominion.Cards.BasicSet.Actions.Remodel;
using Dominion.GameEvents;
using Stateless;

namespace Dominion.Cards.BasicSet.Actions.Saga
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

        public virtual object State { get; private set; }
    }
    public class Saga<TSTATE,TTRIGGER>  : Saga, IDisposable 
    {
        readonly StateMachine<TSTATE,TTRIGGER> _stateMachine;
        readonly MessageMaps<TSTATE, TTRIGGER> _messageMaps;

        public TSTATE CompletedState { get; set; }

        public Saga(TSTATE initialState)
        {
            _stateMachine = new StateMachine<TSTATE, TTRIGGER>(initialState);
            _messageMaps = new MessageMaps<TSTATE, TTRIGGER>(this, _stateMachine);
        }

        protected Saga(TSTATE initialState, TSTATE completedState) : this(initialState)
        {
            CompletedState = completedState;
        }

        public StateMachine<TSTATE, TTRIGGER>.StateConfiguration Configure(TSTATE state)
        {
            return _stateMachine.Configure(state);
        }

        public override void Handle(IGameMessage @event)
        {
            _messageMaps.Fire(@event);
        }

        public override bool IsComplete
        {
            get { return this._stateMachine.IsInState(this.CompletedState); }
        }

        public override object State
        {
            get { return _stateMachine.State; }
        }

         protected StateMachine<TSTATE,TTRIGGER>.TriggerWithParameters<TPARAMETER> 
            Event<TPARAMETER>(TTRIGGER trigger) where TPARAMETER : class
        {
            return _messageMaps.Message<TPARAMETER>().MapsTo(trigger);
        }

        protected StateMachine<TSTATE, TTRIGGER>.TriggerWithParameters<TPARAMETER>
            Event<TPARAMETER>() where TPARAMETER : class
        {
            TTRIGGER trigger = default(TTRIGGER);
            bool matchFound = false;
            Enum.GetNames(typeof(TTRIGGER)).ForEach(name =>
                {
                    if (ParseMessageName(typeof(TPARAMETER)) == name)
                    {
                        trigger = (TTRIGGER)Enum.Parse(typeof(TTRIGGER), name);
                        if(matchFound)
                            throw new ArgumentException(
                                String.Format("There were two values of enum {0} that message type {1} starts with.", 
                                typeof(TTRIGGER).Name, typeof(TPARAMETER).Name)
                        );
                        matchFound = true;
                    }
                }
                );
            if (matchFound)
                return _messageMaps.Message<TPARAMETER>().MapsTo(trigger);

            throw new ArgumentException(String.Format("No value in enum {0} matched event name {1}", typeof(TTRIGGER).Name, typeof(TPARAMETER).Name));
        }

        private string ParseMessageName(Type messageType)
        {
            var name = messageType.Name;
            return RemoveTrailing(name, "Message", "Event", "Command", "Response");
            
        }

        private string RemoveTrailing(string original, params string[] stringsToRemove)
        {
            foreach (var stringToRemove in stringsToRemove)
            {
                if (original.EndsWith(stringToRemove))
                {
                    original = original.Remove(original.LastIndexOf(stringToRemove), stringToRemove.Length);
                }
            }
            return original;
        }

        public void Dispose()
        {
        }
    }
}