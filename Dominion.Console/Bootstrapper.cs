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
                    cfg.Scan(scan =>
                        {
                            scan.WithDefaultConventions();
                            scan.RegisterConcreteTypesAgainstTheFirstInterface();
                        });
                    cfg.AddRegistry<DominionRegistry>();
                });

            return container;
        }
    }
}