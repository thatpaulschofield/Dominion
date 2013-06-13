using Dominion.Cards.BasicSet.Actions;
using Dominion.Cards.BasicSet.Actions.MineAction;
using Dominion.Cards.BasicSet.BasicSet;

namespace Dominion.Cards
{
    public class BigMoney : DeckSet
    {
        public override ISupplyBuilder Build(ISupplyBuilder builder)
        {
            builder.With(10.Of<Adventurer>())
                   .With(10.Of<Bureaucrat>())
                   .With(10.Of<Chancellor>())
                   .With(10.Of<Chapel>())
                   .With(10.Of<Feast>())
                   .With(10.Of<Laboratory>())
                   .With(10.Of<Market>())
                   .With(10.Of<Mine>())
                   .With(10.Of<Moneylender>())
                   .With(10.Of<ThroneRoom>());
            return builder;
        }
    }
}