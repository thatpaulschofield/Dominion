using System.Linq;
using Dominion.Cards.BasicSet.Actions;
using Dominion.GameEvents;

namespace Dominion.Cards.BasicSet.BasicSet
{
    public class Chapel : TypedCard<Chapel>
    {
        public Chapel() : base(cost: 2, isAction:true, name: "Chapel")
        {
        }

        public override void PlayAsAction(ITurnScope turnScope)
        {
            base.PlayAsAction(turnScope);
            turnScope.Publish(new SelectCardsToChapel(turnScope));
        }

        public class SelectCardsToChapel : GameMessage
        {
            public SelectCardsToChapel(IActionScope scope) : base(scope)
            {
                GetAvailableResponses = () => (from card in scope.Hand
                                              select new ChapelCardFromHandResponse(card, scope.GetTurnScope))
                                              .Append<GameEventResponse>(new DeclineToChapelCardsResponse(scope.GetTurnScope));
            }
        }

        public class ChapelCardFromHandResponse : GameEventResponse<SelectCardsToChapel, Card>
        {
            public ChapelCardFromHandResponse(Card card, ITurnScope turnScope) : base(turnScope, card)
            {
                Description = "Trash " + card.Name;
            }

            public override void Execute()
            {
                TurnScope.TrashCardFromHand(Item);
                if (TurnScope.Hand.Any())
                    TurnScope.Publish(new SelectCardsToChapel(TurnScope));
            }
        }

        public class DeclineToChapelCardsResponse : GameEventResponse<SelectCardsToChapel>
        {
            public DeclineToChapelCardsResponse(ITurnScope turnScope) : base(turnScope)
            {
                Description = "None";
            }

            public override void Execute()
            {
                
            }
        }
    }
}
