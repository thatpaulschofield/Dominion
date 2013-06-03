using System.Collections.Generic;
using Dominion.Cards.BasicSet.Actions.MineAction;
using Dominion.Cards.BasicSet.Actions.Saga;
using Dominion.GameEvents;
using Should;
using StructureMap;

namespace Dominion.Tests.Sagas
{
    internal class SagaDsl<TSAGA> where TSAGA : Saga
    {
        private readonly IContainer _container;
        private readonly TSAGA _saga;
        private readonly List<ExpectedResponseDsl> _expectedResponses = new List<ExpectedResponseDsl>();

        public SagaDsl(IContainer container, TSAGA saga)
        {
            _container = container;
            _saga = saga;
        }

        public SagaResponseDsl<TSAGA,TEVENT> ShouldRespondTo<TEVENT>() where TEVENT : IGameMessage
        {
            return new SagaResponseDsl<TSAGA, TEVENT>(this, _saga);
        }

        public void AddExpectedResponse(ExpectedResponseDsl expectedResponse)
        {
            _expectedResponses.Add(expectedResponse);
        }

        public void Test()
        {
            _expectedResponses.ForEach(r => r.Assert(_container));
        }

        public MessageSequenceDsl<TSAGA> AfterHandling<TEVENT>() where TEVENT : IGameMessage
        {
            return new MessageSequenceDsl<TSAGA>(_saga, _container, _container.GetInstance<TEVENT>());
        }
    }

    internal class MessageSequenceDsl<TSAGA> where TSAGA : Saga
    {
        private readonly TSAGA _saga;
        private readonly IContainer _container;
        public MessageSequenceDsl(TSAGA saga, IContainer container, IGameMessage initialMessage)
        {
            _saga = saga;
            _container = container;
            _saga.Handle(initialMessage);
        }

        public MessageSequenceDsl<TSAGA> AndHandling<TEVENT>() where TEVENT : IGameMessage
        {
            _saga.Handle(_container.GetInstance<TEVENT>());
            return this;
        }

        public void ShouldBeComplete()
        {
            _saga.IsComplete.ShouldBeTrue("Status should have been complete.  Actual status: " + _saga.State);
        }

        public MessageSequenceDsl<TSAGA> AndHandling(IGameMessage gameMessage)
        {
            _saga.Handle(gameMessage);
            return this;
        }
    }
}