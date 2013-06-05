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
                            //scan.TheCallingAssembly();
                            scan.Assembly("Dominion.Cards.BasicSet");
                            scan.LookForRegistries();
                            scan.AssemblyContainingType<Game>();
                            scan.WithDefaultConventions();
                            //scan.AddAllTypesOf(typeof (GameEventResponse<>));

                            //scan.AddAllTypesOf<Saga>();
                            //scan.ConnectImplementationsToTypesClosing(typeof (IStartedBy<>));
                        });
                    
                });

            return container;
        }
    }
}