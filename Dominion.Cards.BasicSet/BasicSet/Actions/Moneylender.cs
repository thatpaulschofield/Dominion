using System.Linq;
using Dominion.Cards.BasicSet.Treasures;

namespace Dominion.Cards.BasicSet.Actions
{
    public class Moneylender : TypedCard<Moneylender>
    {
        public Moneylender() : base(cost: 4, isAction:true, name: "Moneylender")
        {
        }

        public override void PlayAsAction(ITurnScope turnScope)
        {
            if (turnScope.Hand.Contains<Card>(Treasure.Copper))
            {
                turnScope.TrashCardFromHand(Treasure.Copper);
                turnScope.ChangeState(3.TurnCoins());
            }
        }
    }
}