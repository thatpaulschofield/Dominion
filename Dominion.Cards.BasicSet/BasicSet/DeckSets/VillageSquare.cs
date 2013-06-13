using Dominion.Cards.BasicSet.Actions;
using Dominion.Cards.BasicSet.Actions.Remodel;

namespace Dominion.Cards
{
    public class VillageSquare : DeckSet
    {
        public override ISupplyBuilder Build(ISupplyBuilder builder)
        {
            return builder.With(10.Of<Bureaucrat>())
                          .With(10.Of<Cellar>())
                          .With(10.Of<Festival>())
                          .With(10.Of<Library>())
                          .With(10.Of<Market>())
                          .With(10.Of<Remodel>())
                          .With(10.Of<Smithy>())
                          .With(10.Of<ThroneRoom>())
                          .With(10.Of<Village>())
                          .With(10.Of<Woodcutter>());
        }
    }
}