using Dominion.Cards.BasicSet.Actions;
using Dominion.Cards.BasicSet.Actions.MineAction;
using Dominion.Cards.BasicSet.Actions.Remodel;

namespace Dominion.Cards
{
    public class FirstGame : DeckSet
    {
        public override SupplyBuilder Build(SupplyBuilder builder)
        {
            builder.With(10.Of<Cellar>())
                   .With(10.Of<Market>())
                   .With(10.Of<Militia>())
                   .With(10.Of<Mine>())
                   .With(10.Of<Moat>())
                   .With(10.Of<Remodel>())
                   .With(10.Of<Smithy>())
                   .With(10.Of<Village>())
                   .With(10.Of<Woodcutter>())
                   .With(10.Of<Workshop>());
            return builder;
        }
    }
}