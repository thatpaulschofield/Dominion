using Dominion.Ai;
using Dominion.Configuration;
using StructureMap;

namespace Dominion.Tests
{
    public class Bootstrapper
    {
        public IContainer BootstrapApplication()
        {
            return BootstrapApplication(new Container());
        }

        public IContainer BootstrapApplication(IContainer container)
        {
            container.Configure(cfg =>
                {
                    cfg.AddRegistry<DominionRegistry>();
                    cfg.Scan(scan =>
                        {
                            scan.AssemblyContainingType<Game>();
                            scan.Assembly("Dominion.Cards.BasicSet");
                            scan.AssemblyContainingType<AiRegistry>();
                            scan.LookForRegistries();
                            scan.AssemblyContainingType<Game>();
                            scan.WithDefaultConventions();
                            scan.Convention<DefaultCtorParameterConvention>();
                        });
                });
            AutomapperConfig.ConfigureMappings(container);
            return container;
        }
    }
}