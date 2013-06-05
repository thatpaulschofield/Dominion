using Dominion.GameEvents;
using StructureMap;

namespace Dominion.Ai
{
    public class AiBootstrapper
    {
        public IContainer BootstrapContainer()
        {
            AutomapperConfig.ConfigureMappings();

            var container = new Container();
            
            container.Configure(cfg =>
                {
                    
                    cfg.Scan(s =>
                        {
                            s.TheCallingAssembly();
                            s.AssemblyContainingType<Game>();
                            s.AddAllTypesOf<GameEventResponse>();
                        });
                    cfg.AddRegistry<AiRegistry>();
                });

            return container;
        }
    }
}