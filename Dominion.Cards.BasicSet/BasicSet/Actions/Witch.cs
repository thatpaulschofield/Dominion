using System;
using Dominion.GameEvents;

namespace Dominion.Cards.BasicSet.Actions
{
    public class Witch : TypedCard<Witch>
    {
        public Witch() : base(cost:5, isAction:true, isAttack:true)
        {
            
        }

        public override void PlayAsAction(ITurnScope turnScope)
        {
            turnScope.Publish(new WitchEffect(turnScope));
        }

        public class WitchEffect : GameMessage, IAttackEffect
        {
            public WitchEffect(IActionScope scope) : base(scope)
            {
            }

            public void Resolve(IReactionScope scope)
            {
                Card curse = null;

                try
                {
                    scope.GainCardFromSupply(BasicCards.Curse);
                }
                catch (Exception)
                {
                }


            }
        }
    }
}