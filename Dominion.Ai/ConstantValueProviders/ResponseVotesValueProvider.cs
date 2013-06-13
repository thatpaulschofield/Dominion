using Dominion.AI;

namespace Dominion.Ai.ConstantValueProviders
{
    public class ResponseVotesValueProvider : InitialValueProvider<ResponseVotes>
    {
        public ResponseVotesValueProvider()
        {
            this.ProvideValueInitializer = () => new ResponseVotes();
        }
    }
}