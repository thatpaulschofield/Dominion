using Dominion.Cards.BasicSet.Actions.Remodel;
using Dominion.Cards.BasicSet.BasicSet.Actions.Remodel;
using Dominion.Cards.BasicSet.Treasures;
using NUnit.Framework;

namespace Dominion.Tests.Sagas
{
    internal class RemodelSagaSpecs : SagaSpecs<RemodelSaga>
    {

        [Test]
        public void Should_start_by_asking_for_a_card_to_remodel()
        {
            TheSaga.ShouldRespondTo<RemodelPlayedMessage>()
                   .With<PickCardToRemodelCommand>()
                   .Test();
        }

        [Test]
        public void Card_remodeled_sequence()
        {
            TheSaga.AfterHandling<RemodelPlayedMessage>()
                .AndHandling(new CardSelectedToRemodelResponse(TurnScope, Treasure.Copper))
                .AndHandling(new CardSelectedToRemodelToResponse(Treasure.Copper, TurnScope))
                .ShouldBeComplete();
        }
    }
}