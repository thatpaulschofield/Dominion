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

        public Card AcquireCard(CardType cardType)
        {
            return this[cardType].Draw();
        }
    }
}