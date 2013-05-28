using Dominion.AI;
using Dominion.AI.Functions;
using Dominion.AI.Functions.Boolean;
using Dominion.AI.Terminals;
using Dominion.Tests.GameEvents;
using NUnit.Framework;
using Should;

namespace Dominion.Tests.AI
{

    [TestFixture]
    public class blah
    {
        [Test]
        public void bar()
        {

        }
    }

    [TestFixture]
    public class ExpressionSpecs
    {
        private Player player;
        private MockEventAggregator _eventAggregator;

        [SetUp]
        public void SetUp()
        {
            var discardPile = new DiscardPile();
            var deck = new Deck(7.Coppers(), 3.Estates());
            _eventAggregator = new MockEventAggregator();
            player = new Player(deck, discardPile, new NaivePlayerController(), new MockEventAggregator());
        }

        [Test]
        public void True_should_equal_not_false()
        {
            var expression = new Equals<bool>
                {
                    Child1 = new Constant<bool>(true),
                    Child2 = new Not
                        {
                            Child1 = new Constant<bool>(false)
                        }
                };
            expression.Evaluate(new TurnScope(player, new Supply(), _eventAggregator)).ShouldBeTrue();
        }

        [Test]
        public void True_should_not_equal_false()
        {
            var expression = new Equals<bool>
                {
                    Child1 = new Constant<bool>(true),
                    Child2 = new Constant<bool>(false)
                };
            expression.Evaluate(new TurnScope(player, new Supply(), _eventAggregator)).ShouldBeFalse();
        }

        [Test]
        public void True_should_equal_true()
        {
            var expression = new Equals<bool>
                {
                    Child1 = new Constant<bool>(true),
                    Child2 = new Constant<bool>(true)
                };
            expression.Evaluate(new TurnScope(player, new Supply(), _eventAggregator)).ShouldBeTrue();
        }

        [Test]
        public void Six_should_equal_six()
        {
            var equals = new Equals<int>
                {
                    Child1 = new Constant<int>(6),
                    Child2 = new Constant<int>(6)
                };
            equals.Evaluate(new TurnScope(player, new Supply(), _eventAggregator)).ShouldBeTrue();
        }

        [Test]
        public void Not_five_equals_six_should_be_true()
        {
            var expression = new Not
                {
                    Child1 = new Equals<int>
                        {
                            Child1 = new Constant<int>(5),
                            Child2 = new Constant<int>(6)
                        }
                };
            expression.Evaluate(new TurnScope(player, new Supply(), _eventAggregator)).ShouldBeTrue();
        }

        [Test]
        public void Five_should_not_equal_six()
        {
            var expression = new Equals<int>
                {
                    Child1 = new Constant<int>(5),
                    Child2 = new Constant<int>(6)
                };
            expression.Evaluate(new TurnScope(player, new Supply(), _eventAggregator)).ShouldBeFalse();
        }
    }
}