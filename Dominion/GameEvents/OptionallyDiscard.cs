using System.Linq;
using Dominion.GameEvents;

namespace Dominion.Cards.BasicSet.Actions
{
    public class OptionallyDiscard : GameMessage
    {
        private DoneDiscardingResponse _doneDiscarding;
        public OptionallyDiscard(ITurnScope turnScope) : base(turnScope)
        {
            _doneDiscarding = new DoneDiscardingResponse(turnScope);
            GetAvailableResponses = () => turnScope.Hand.Select(c => new OptionalDiscardResponse(turnScope, c, this))
                                                   .Union(new IEventResponse[] { _doneDiscarding })
                ;
        }

        public void CardWasDiscarded()
        {
            _doneDiscarding.IncrementDiscardedCount();
        }
    }
}