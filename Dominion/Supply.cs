using System.Collections.Generic;
using System.Linq;
using Dominion.Cards;

namespace Dominion
{
    public class Supply : Dictionary<CardType, SupplyPile>
    {
        public Supply()
        {
            
        }

        public Supply(params SupplyPile[] piles)
        {
            piles.ToList().ForEach(p => this.Add(p.Type, p));
        }

        public Card AcquireCard(CardType cardType, IActionScope turnScope)
        {
            return this[cardType].Draw(turnScope);
        }

        public CardSet FindCardsEligibleForPurchase(ITurnScope turnScope)
        {
            return FindCardsCostingUpTo(turnScope.Coins, turnScope);
        }

        public CardSet FindCardsCostingUpTo(Money maxCost, IActionScope scope)
        {
            var eligibleCards = this.Select(x => x.Value)
                                    .Where(t => t.Type.Create().BaseCost <= maxCost && t.Count > 0)
                                    .Select(z => z.Type.Create());

            return new CardSet(eligibleCards);
        }

        public CardSet FindCardsCostingExactly(Money cost)
        {
            var eligibleCards = this.Select(x => x.Value)
                                    .Where(t => t.Type.Create().BaseCost == cost && t.Count > 0)
                                    .Select(z => z.Type.Create());

            return new CardSet(eligibleCards);
        }

        public static Supply FromISupplyBuilder(ISupplyBuilder builder)
        {
            return builder.BuildSupply();
        }
    }


}