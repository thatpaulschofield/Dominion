using System;
using Dominion.Cards;

namespace Dominion.Ai.ConstantValueProviders
{
    public class SupplyValueProvider : InitialValueProvider<CardSet>
    {
        private CardSet _supply;

        public override Func<CardSet> ProvideValueInitializer
        {
            get
            {
                return () => new CardSet(_supply);
            }
        }
    }
}