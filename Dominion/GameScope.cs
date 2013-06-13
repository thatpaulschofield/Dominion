using System.Collections.Generic;
using Dominion.Infrastructure;
using StructureMap;

namespace Dominion
{
    public class GameScope
    {
        public IEventAggregator EventAggregator { get; set; }
        public ISupplyBuilder SupplyBuilder { get; set; }
        public PlayerBuilder PlayerBuilder { get; set; }
        public IEnumerable<EndGameCondition> EndGameConditions { get; set; }
        public IBus Bus { get; set; }
        public IContainer Container { get; private set; }

        public GameScope(IEventAggregator eventAggregator,
                         ISupplyBuilder supplyBuilder,
                         PlayerBuilder playerBuilder,
                         IEnumerable<EndGameCondition> endGameConditions,
                         IBus bus,
                         IContainer container)
        {
            EventAggregator = eventAggregator;
            SupplyBuilder = supplyBuilder;
            PlayerBuilder = playerBuilder;
            EndGameConditions = endGameConditions;
            this.Bus = bus;
            Container = container;
        }

        public T GetInstance<T>()
        {
            return Container.GetInstance<T>();
        }
    }
}