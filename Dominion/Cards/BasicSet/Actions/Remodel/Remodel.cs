using System;

namespace Dominion.Cards.BasicSet.Actions.Remodel
{
    public class Remodel : TypedCard<Remodel>
    {
        public Remodel()
            : base(isAction: true, cost: 4, name: "Remodel")
        {
        }

        public override void PlayAsAction(ITurnScope turnScope)
        {
            var id = Guid.NewGuid();
            turnScope.Publish(new RemodelPlayedMessage(turnScope){Id = id, OriginalEventId = id, CorrelationId = id});
        }
    }
}