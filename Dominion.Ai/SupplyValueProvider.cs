using System;
using Dominion.AI;
using Dominion.Ai.Nodes;
using Dominion.Ai.TreeBuilding;
using Dominion.Cards;

namespace Dominion.Ai
{
    public class SupplyValueProvider : InitialValueProvider<CardSet>, IProvideContextFromActionScope

    {
        private CardSet _supply;

        public override Func<CardSet> ProvideValueInitializer
        {
            get
            {
                return () => new CardSet(_supply);
            }
        }

        public void ViewActionScope(IActionScope scope)
        {
            
        }
    }
}