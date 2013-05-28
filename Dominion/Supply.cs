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

        public Card AcquireCard(CardType cardType, ITurnScope turnScope)
        {
            return this[cardType].Draw(turnScope);
        }

        public CardSet FindCardsEligibleForPurchase(ITurnScope turnScope)
        {
            var eligibleCards =
                this.Select(x => x.Value)
                    .Where(t => 
                        t.Type.Create().Cost <= turnScope.Coins && t.Count > 0)
                    .Select(z => z.Type.Create());
                
            return new CardSet(eligibleCards);
        }
    }
}