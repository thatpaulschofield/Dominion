using Dominion.Cards.BasicSet.Actions;

namespace Dominion.Cards
{
    public class Interaction : DeckSet
    {
        public override ISupplyBuilder Build(ISupplyBuilder builder)
        {
            builder.With(10.Of<Bureaucrat>())
                   .With(10.Of<Chancellor>())
                   .With(10.Of<CouncilRoom>())
                   .With(10.Of<Festival>())
                   .With(10.Of<Library>())
                   .With(10.Of<Militia>())
                   .With(10.Of<Moat>())
                   .With(10.Of<Spy>())
                   .With(10.Of<Thief>())
                   .With(10.Of<Village>());
            return builder;
        }
    }
}