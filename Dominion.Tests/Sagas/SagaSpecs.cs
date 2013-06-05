using Dominion.Cards.BasicSet.Actions.MineAction;
using Dominion.Cards.BasicSet.Actions.Saga;
using Dominion.Cards.Saga;
using Dominion.Tests.GameEvents;
using StructureMap;

namespace Dominion.Tests.Sagas
{
    class SagaSpecs<T> where T : Saga, new()
    {
        private readonly Container _container;
        protected ITurnScope TurnScope = new MockTurnScope();
        public SagaSpecs()
        {
            _container = new Container();
            _container.Configure(cfg =>
                {
                    cfg.For<MockTurnScope>().Singleton().Use<MockTurnScope>();
                    cfg.For<ITurnScope>().Use<MockTurnScope>();
                    cfg.For<IActionScope>().Use<MockTurnScope>();
                    cfg.For<IEventAggregator>().Singleton().Use<MockEventAggregator>();
                });
        }


        public SagaDsl<T> TheSaga { get { return new SagaDsl<T>(_container.GetNestedContainer(), new T());}}
    }
}