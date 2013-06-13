using Dominion.Cards.BasicSet;
using Dominion.Cards.BasicSet.BasicSet;
using Dominion.Configuration;
using Dominion.GameEvents;
using StructureMap;

namespace Dominion.Ai
{
    public class AiBootstrapper
    {
        public IContainer BootstrapContainer()
        {
            var container = new Container();
            
            container.Configure(cfg =>
                {
                    
                    cfg.Scan(scan =>
                        {
                            scan.TheCallingAssembly();
                            scan.AssemblyContainingType<Game>();
                            scan.AddAllTypesOf<GameEventResponse>();
                            scan.Convention<DefaultCtorParameterConvention>();
                        });
                    cfg.AddRegistry<BasicSetRegistry>();
                    cfg.AddRegistry<AiRegistry>();
                });
            AutomapperConfig.ConfigureMappings(container);
            return container;
        }
    }
}