using System;
using System.Collections.Generic;
using Dominion.Cards.BasicSet.Actions.Saga;
using Dominion.GameEvents;
using Stateless;

namespace Dominion.Cards.Saga
{
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
            else
            {
                // This saga does not handle this particular message
            }
        }
    }
}