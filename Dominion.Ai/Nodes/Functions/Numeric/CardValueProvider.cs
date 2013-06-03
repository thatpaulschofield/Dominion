using System;
using Dominion.Ai.TreeBuilding;
using Dominion.Cards;
using Dominion.Cards.BasicSet.Treasures;

namespace Dominion.Ai.Nodes.Functions.Numeric
{
    public class CardValueProvider : InitialValueProvider<CardType>
    {
        public override Func<CardType> ProvideValueInitializer
        {
            get { return () => Treasure.Copper; }
        }
    }
}