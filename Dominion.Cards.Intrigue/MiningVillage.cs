using System.Collections.Generic;
using Dominion.Cards.BasicSet.Actions;
using Dominion.GameEvents;

namespace Dominion.Cards.Intrigue
{
    public class MiningVillage : TypedCard<MiningVillage>
    {
        public MiningVillage() : base(cost:4, isAction:true)
        {
            
        }

        public override void PlayAsAction(ITurnScope turnScope)
        {
            turnScope.ChangeState(2.TurnActions());
            turnScope.DrawCardsIntoHand(2);

        }

        public class DoYouWantToTrashMiningVillage : GameMessage
        {
            public DoYouWantToTrashMiningVillage(IActionScope scope) : base(scope)
            {
                Description = scope.Player.Name + ", trash Mining Village for (+2)?";
                GetAvailableResponses = () => new List<IEventResponse>
                    {
                        new TrashMiningVillage(scope),
                        new DoNotTrashMiningVillage(scope)
                    };
            }
        }

        public class TrashMiningVillage : GameEventResponse<DoYouWantToTrashMiningVillage>
        {
            public TrashMiningVillage(IActionScope scope) : base(scope)
            {
                Description = "Yes";
            }

            public override void Execute()
            {
                TurnScope.ChangeState(2.TurnCoins());
                TurnScope.TrashCardInPlay(CardType.Of<MiningVillage>());
            }
        }

        public class DoNotTrashMiningVillage : GameEventResponse<DoYouWantToTrashMiningVillage>
        {
            public DoNotTrashMiningVillage(IActionScope scope)
                : base(scope)
            {
                Description = "No";
            }

            public override void Execute()
            {

            }
        }

    }
}