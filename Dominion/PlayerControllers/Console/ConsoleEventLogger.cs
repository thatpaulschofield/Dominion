using Dominion.GameEvents;

namespace Dominion.PlayerControllers.Console
{
    public class ConsoleEventLogger : IHandleEvents
    {
        public ConsoleEventLogger(IEventAggregator eventAggregator)
        {
            eventAggregator.Register(this);
        }

        public void Handle(IGameMessage @event)
        {
            System.Console.WriteLine("\t[{0}]", @event.ActionScope);
            System.Console.WriteLine("\t[{0}]", @event);

            if (@event is GameEndedEvent)
            {
                System.Console.WriteLine("Press a key to continue...");
                System.Console.ReadKey();
            }
        }

        public bool CanHandle(IGameMessage @event)
        {
            return true;
        }
    }
}