using Dominion.Cards.BasicSet.Actions;
using Dominion.Cards.BasicSet.Actions.MineAction;

namespace Dominion.Cards.Intrigue.DeckSets
{
    public class Underlings : DeckSet
    {
        public override ISupplyBuilder Build(ISupplyBuilder builder)
        {
            return builder.With(10.Of<Baron>())
                          .With(10.Of<Cellar>())
                          .With(10.Of<Festival>())
                          .With(10.Of<Library>())
                          .With(10.Of<Masquerade>())
                          .With(10.Of<Minion>())
                          .With(10.Of<Nobles>())
                          .With(10.Of<Pawn>())
                          .With(10.Of<Steward>())
                          .With(10.Of<Witch>());
        }
    }

    public class HandMadness : DeckSet
    {
        public override ISupplyBuilder Build(ISupplyBuilder builder)
        {
            return builder.With(10.Of<Bureaucrat>())
                          .With(10.Of<Chancellor>())
                          .With(10.Of<CouncilRoom>())
                          .With(10.Of<Courtyard>())
                          .With(10.Of<Mine>())
                          .With(10.Of<Militia>())
                          .With(10.Of<Minion>())
                          .With(10.Of<Nobles>())
                          .With(10.Of<Steward>())
                          .With(10.Of<Torturer>());
        }
    }
}