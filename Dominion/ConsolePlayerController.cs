using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Dominion.GameEvents;

namespace Dominion
{
    public class ConsolePlayerController : IPlayerController
    {
        public GameEventResponse HandleGameEvent(IMessage @event)
        {
            if (!@event.GetAvailableResponses().Any())
                return @event.GetDefaultResponse();

            DisplayTurnInfo(@event.TurnScope);
            DisplayHand(@event.TurnScope.Hand);

            Console.WriteLine(@event.Description);
            var responses = new ConsoleEventResponses(@event.GetAvailableResponses());
            foreach (var response in responses)
            {
                Console.WriteLine("{0} - {1}", response.Index, response.Description);
            }
            ConsoleEventResponse consoleEventResponse = null;
            do
            {
                int index;
                var parsed = Int32.TryParse(Console.ReadLine(), out index);
                if (!parsed)
                    index = 1;
                consoleEventResponse = responses.FirstOrDefault(r => r.Index == index);
            } while (consoleEventResponse == null);
                    return consoleEventResponse.Response;
        }

        private void DisplayTurnInfo(ITurnScope turnScope)
        {
            Console.WriteLine(turnScope);
        }

        private void DisplayHand(Hand hand)
        {
            Console.WriteLine("Player's hand: {0}", hand);
            
        }
    }

    public class ConsoleEventResponses : IEnumerable<ConsoleEventResponse>
    {
        private readonly List<ConsoleEventResponse> _responses = new List<ConsoleEventResponse>();

        public ConsoleEventResponses(IEnumerable<GameEventResponse> availableResponses)
        {
            int i = 0;
            foreach (var availableResponse in availableResponses)
            {
                i++;
                _responses.Add(new ConsoleEventResponse(i, availableResponse));
            }
        }

        public IEnumerator<ConsoleEventResponse> GetEnumerator()
        {
            return _responses.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _responses.GetEnumerator();
        }
    }
}