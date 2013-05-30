using Dominion.GameEvents;

namespace Dominion
{
    public class ConsoleEventResponse
    {
        public ConsoleEventResponse(int index, IEventResponse availableResponse)
        {
            this.Index = index;
            this.Response = availableResponse;
        }

        public int Index { get; set; }
        public IEventResponse Response { get; set; }

        public string Description
        {
            get { return Response.Description; }
        }
    }
}