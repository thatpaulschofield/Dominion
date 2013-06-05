using Dominion.Cards.BasicSet.Actions.Saga;
using Dominion.Cards.Saga;
using Dominion.GameEvents;
using Dominion.Tests.GameEvents;
using StructureMap;

namespace Dominion.Tests.Sagas
{
    internal class ExpectedResponseDsl<TSAGA, TEVENT, TRESPONSE> : ExpectedResponseDsl where TSAGA : Saga where TEVENT : IGameMessage
    {
        private readonly Saga _saga;

        public ExpectedResponseDsl(Saga saga)
        {
            _saga = saga;
        }

        public override void Assert(IContainer container)
        {
            container.Configure(cfg =>
                {
                    cfg.For<TEVENT>().Use<TEVENT>();
                    cfg.For<TRESPONSE>().Use<TRESPONSE>();
                });
            _saga.Handle(container.GetInstance<TEVENT>());
            var eventAggregator = container.GetInstance<IEventAggregator>() as MockEventAggregator;
            eventAggregator.AssertMessageWasSent<TRESPONSE>();
        }
    }

    public class ExpectedResponseDsl
    {
        public virtual void Assert(IContainer container)
        {
            
        }
       
    }
}