using System.Collections.Generic;
using Dominion.Ai.Nodes;
using Dominion.Ai.Nodes.Functions;
using Dominion.Ai.Nodes.Functions.CardSets;
using Dominion.Ai.Nodes.Terminals;
using NUnit.Framework;
using Should;

namespace Dominion.Tests.AI
{
    public class TypeExtensionSpecs
    {
        [Test]
        public void Getting_the_generic_argument_works_like_I_think()
        {
            typeof(List<Game>).GenericTypeArgument()
                              .ShouldEqual(typeof(Game));
        }

        [Test]
        public void IsAssignableFrom_works_like_I_think()
        {
            (typeof (Constant).IsAssignableFrom(typeof (CardsInHandTerminal)))
                .ShouldBeFalse();
            (typeof(Terminal).IsAssignableFrom(typeof(CardsInHandTerminal)))
                .ShouldBeTrue();
        }
    }
}