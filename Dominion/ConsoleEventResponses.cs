using System.Collections;
using System.Collections.Generic;
using Dominion.GameEvents;

namespace Dominion
{
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