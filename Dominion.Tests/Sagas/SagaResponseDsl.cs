using Dominion.Cards.BasicSet.Actions.MineAction;
using Dominion.Cards.BasicSet.Actions.Saga;
using Dominion.Cards.Saga;
using Dominion.GameEvents;

namespace Dominion.Tests.Sagas
{
    internal class SagaResponseDsl<TSAGA,TEVENT> where TSAGA:Saga where TEVENT : IGameMessage
    {
        private readonly SagaDsl<TSAGA> _sagaDsl;
        private readonly TSAGA _saga;

        public SagaResponseDsl(SagaDsl<TSAGA> sagaDsl, TSAGA saga)
        {
            _sagaDsl = sagaDsl;
            _saga = saga;
        }

        public SagaDsl<TSAGA> With<TRESPONSE>()
        {
            var expectedResponse = new ExpectedResponseDsl<TSAGA, TEVENT, TRESPONSE>(_saga);
            _sagaDsl.AddExpectedResponse(expectedResponse);
            return _sagaDsl;
        }
    }
}