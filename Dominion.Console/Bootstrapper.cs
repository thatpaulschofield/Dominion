using Dominion.Ai;
using Dominion.Cards.BasicSet.Actions.MineAction;
using Dominion.Configuration;
using StructureMap;

namespace Dominion.Console
{
    public class Bootstrapper
    {
        public IContainer BootstrapApplication()
        {
            var container = new Container();
            container.Configure(cfg =>
                {
                    cfg.AddRegistry<DominionRegistry>();
                    cfg.Scan(scan =>
                        {
                            scan.Assembly("Dominion.Cards.BasicSet");
                            scan.Assembly("Dominion.Cards.Intrigue");
                            scan.Assembly("Dominion.Ai");
                            scan.LookForRegistries();
                            scan.AssemblyContainingType<Game>();
                            //scan.WithDefaultConventions();
                            scan.Convention<DefaultCtorParameterConvention>();
                        });
                });
            AutomapperConfig.ConfigureMappings(container);
            return container;
        }
    }
}