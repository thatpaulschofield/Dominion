using System;
using Dominion.Cards.BasicSet.Actions.MineAction;
using Dominion.GameEvents;
using Stateless;

namespace Dominion.Cards.BasicSet.Actions.Saga
{
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

    public abstract class MessageMap
    {

        public virtual void Fire(IGameMessage @event)
        {
        }

        public abstract object TriggerEnum { get; }

        public abstract object TriggerObject { get; }

        public override string ToString()
        {
            return String.Format("Message map {0} - {1}", TriggerEnum, TriggerObject);
        }
    }
}