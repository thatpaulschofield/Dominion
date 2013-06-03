using AutoMapper;
using Dominion.AI;
using Dominion.Ai.Nodes.Functions.Numeric;
using Dominion.Ai.TreeBuilding;
using Dominion.Cards;
using StructureMap;
using StructureMap.Configuration.DSL;

namespace Dominion.Ai
{
    public static class AutomapperConfig
    {
        public static void ConfigureMappings()
        {
            Mapper.CreateMap<ITurnScope, AiContext>();
        }

    }

    public class AiBootstrapper
    {
        public IContainer BootstrapContainer()
        {
            AutomapperConfig.ConfigureMappings();

            var container = new Container();
            
            container.Configure(cfg =>
                {
                    //cfg.Scan(s =>
                    //    {
                    //        //s.TheCallingAssembly();
                    //        //s.RegisterConcreteTypesAgainstTheFirstInterface();
                    //        //s.AddAllTypesOf(typeof(Function<>));
                    //        //s.ConnectImplementationsToTypesClosing(typeof (Constant<>));
                    //    });
                    cfg.AddRegistry<AiRegistry>();
                });

            return container;
        }
    }
    public class AiRegistry : Registry
    {
        public AiRegistry()
        {
            For<FullTreeStrategy>().Use<FullTreeStrategy>();
            For<NodeRegistry>().Singleton().Use<NodeRegistry>();
            For<IInitialValueProvider>().Use<IntValueProvider>();
            For<IInitialValueProvider<ResponseVotes>>().Use<VoteValueProvider>();
            For<IInitialValueProvider<CardType>>().Use<CardValueProvider>();
            For<IInitialValueProvider<CardSet>>().Use<SupplyValueProvider>();
            For<IInitialValueProvider<int>>().Use<IntValueProvider>();
            //CardValueProvider : InitialValueProvider<CardType>
        }
    }
}
