using System;

namespace Dominion.Cards.BasicSet.Actions.MineAction
{
    public class Mine : TypedCard<Mine>
    {
        public Mine()
            : base(isAction: true, cost: 5, name: "Mine")
        {
            
        }

        public override void PlayAsAction(ITurnScope turnScope)
        {
            var id = Guid.NewGuid();
            turnScope.Publish(new MinePlayedMessage(turnScope) { Id = id, CorrelationId = id, OriginalEventId = id});
        }
    }

}