using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominion.Cards.BasicSet.Actions;
using Dominion.Cards.BasicSet.Actions.MineAction;
using Dominion.Cards.BasicSet.Treasures;
using NUnit.Framework;

namespace Dominion.Tests.Sagas
{
    class MineSagaSpecs : SagaSpecs<MineSaga>
    {
        [Test]
        public void Should_start_by_asking_for_a_treasure_to_trash()
        {
            TheSaga
                .ShouldRespondTo<MinePlayedMessage>()
                .With<PickTreasureToTrashForMineCommand>()
                .Test();
        }

        [Test]
        public void Card_trashed_sequence()
        {
            TheSaga
                .AfterHandling<MinePlayedMessage>()
                .AndHandling(new CardSelectedToTrashForMineEvent(Treasure.Copper, TurnScope))
                .AndHandling(new TreasurePickedToUpgradeToWithMine(Treasure.Copper, TurnScope))
                .ShouldBeComplete();
        }

        [Test]
        public void Card_not_trashed_sequence()
        {
            TheSaga
                .AfterHandling<MinePlayedMessage>()
                .AndHandling<DeclinedToTrashCardForMineResponse>()
                .ShouldBeComplete();
        }
    }
}
