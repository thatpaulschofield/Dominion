using Dominion.GameEvents;

namespace Dominion
{
    public class ConsoleEventResponse
    {
        public ConsoleEventResponse(int index, GameEventResponse availableResponse)
        {
            this.Index = index;
            this.Response = availableResponse;
        }

        public int Index { get; set; }
        public GameEventResponse Response { get; set; }

        public string Description
        {
            get { return Response.Description; }
        }
    }
}