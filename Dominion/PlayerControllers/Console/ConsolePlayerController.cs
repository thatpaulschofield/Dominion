using System;
using System.Collections.Generic;
using System.Linq;
using Dominion.GameEvents;

namespace Dominion.PlayerControllers.Console
{
    public class ConsolePlayerController : IPlayerController
    {
        public IEventResponse HandleGameEvent(IGameMessage @event, IEnumerable<IEventResponse> availableResponses, IActionScope playerActionScope)
        {
            availableResponses = availableResponses.ToList();
            if (!availableResponses.Any())
                return @event.GetDefaultResponse();

            DisplayHand(playerActionScope.Hand);

            System.Console.WriteLine(@event.Description);
            var responses = new ConsoleEventResponses(availableResponses);
            foreach (var response in responses)
            {
                System.Console.WriteLine("{0} - {1}", response.Index, response.Description);
            }
            System.Console.WriteLine("[Enter = {0}]", @event.GetDefaultResponse());
            ConsoleEventResponse consoleEventResponse = null;
            do
            {
                var input = System.Console.ReadLine();
                if (input == String.Empty)
                {
                    consoleEventResponse = new ConsoleEventResponse(0, @event.GetDefaultResponse());
                }
                else
                {
                    int index;
                    var parsed = Int32.TryParse(input, out index);
                    if (!parsed)
                        index = 1;
                    consoleEventResponse = responses.FirstOrDefault(r => r.Index == index);
                }
            } while (consoleEventResponse == null);
                    return consoleEventResponse.Response;
        }

        public IEventResponse HandleGameEvent(IGameMessage @event, ITurnScope scope)
        {
            DisplayTurnInfo(scope);
            System.Console.WriteLine(scope.ActingPlayer.Name + ", please respond to " + @event);
            return HandleGameEvent(@event, @event.GetAvailableResponses(), scope);
        }

        public IEventResponse HandleGameEvent(IGameMessage @event, IReactionScope scope)
        {
            System.Console.WriteLine(scope.ReceivingPlayer.Name + ", please respond to " + @event + " from " + scope.OriginatingPlayer.Name);
            return HandleGameEvent(@event, @event.GetAvailableReactions(scope), scope);
        }

        private void DisplayTurnInfo(IActionScope actionScope)
        {
            System.Console.WriteLine(actionScope);
        }

        private void DisplayHand(Hand hand)
        {
            System.Console.WriteLine("Player's hand: {0}", hand);
            
        }
    }
}