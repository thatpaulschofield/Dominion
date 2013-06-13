using Dominion.GameEvents;

namespace Dominion.Cards.BasicSet.Actions
{
    public class CouncilRoom : TypedCard<CouncilRoom>
    {
        public CouncilRoom() : base(cost: 5, isAction:true)
        {}

        public override void PlayAsAction(ITurnScope turnScope)
        {
            turnScope.DrawCardsIntoHand(4);
        }


        public class CouncilRoomEffect : GameMessage, IAttackEffect
        {
            public CouncilRoomEffect(IActionScope scope) : base(scope)
            {

            }

            public void Resolve(IReactionScope scope)
            {
                scope.DrawCardsIntoHand(1);
            }
        }
    }
}