using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Dominion.GameEvents;

namespace Dominion.PlayerControllers.Console
{
    public class ConsoleEventResponses : IEnumerable<ConsoleEventResponse>
    {
        private readonly List<ConsoleEventResponse> _responses = new List<ConsoleEventResponse>();

        public ConsoleEventResponses(IEnumerable<IEventResponse> availableResponses)
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

        public override string ToString()
        {
            return "[" +_responses.Aggregate("", (accumulate, response) => accumulate + response.Description + " ") + "]";
        }
    }
}