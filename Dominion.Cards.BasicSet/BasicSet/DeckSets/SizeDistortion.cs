using Dominion.Cards.BasicSet.Actions;
using Dominion.Cards.BasicSet.BasicSet;
using Dominion.Cards.BasicSet.BasicSet.VictoryCards;

namespace Dominion.Cards
{
    public class SizeDistortion : DeckSet
    {
        public override ISupplyBuilder Build(ISupplyBuilder builder)
        {
            builder.With(10.Of<Cellar>())
                   .With(10.Of<Chapel>())
                   .With(10.Of<Feast>())
                   .With(10.Of<Gardens>())
                   .With(10.Of<Laboratory>())
                   .With(10.Of<Thief>())
                   .With(10.Of<Village>())
                   .With(10.Of<Witch>())
                   .With(10.Of<Woodcutter>())
                   .With(10.Of<Workshop>());

            return builder;
        }
    }
}