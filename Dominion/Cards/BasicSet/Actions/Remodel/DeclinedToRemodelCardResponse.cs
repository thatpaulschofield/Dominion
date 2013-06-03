using Dominion.GameEvents;

namespace Dominion.Cards.BasicSet.Actions.Remodel
{
    public class DeclinedToRemodelCardResponse : GameEventResponse
    {
        public DeclinedToRemodelCardResponse(ITurnScope turnScope) : base(turnScope)
        {
            Description = "None";
        }

        public override void Execute()
        {
            
        }
    }
}