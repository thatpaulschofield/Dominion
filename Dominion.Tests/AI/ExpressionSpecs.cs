using Dominion.AI;
using Dominion.AI.Functions;
using Dominion.AI.Functions.Boolean;
using Dominion.AI.Functions.Numeric;
using Dominion.Ai;
using Dominion.Ai.Nodes.Functions;
using Dominion.Ai.Nodes.Functions.Boolean;
using Dominion.Ai.Nodes.Functions.Numeric;
using Dominion.Ai.Nodes.Terminals;
using Dominion.Cards.BasicSet.Treasures;
using Dominion.GameEvents;
using Dominion.Tests.GameEvents;
using NUnit.Framework;
using Should;

namespace Dominion.Tests.AI
{
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
            player = new Player(deck, discardPile, new NaivePlayerController());
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
            expression.Evaluate(new MockAiContext()).ShouldBeTrue();
        }

        [Test]
        public void True_should_not_equal_false()
        {
            var expression = new Equals<bool>
                {
                    Child1 = new Constant<bool>(true),
                    Child2 = new Constant<bool>(false)
                };
            expression.Evaluate(new MockAiContext()).ShouldBeFalse();
        }

        [Test]
        public void True_should_equal_true()
        {
            var expression = new Equals<bool>
                {
                    Child1 = new Constant<bool>(true),
                    Child2 = new Constant<bool>(true)
                };
            expression.Evaluate(new MockAiContext()).ShouldBeTrue();
        }

        [Test]
        public void Six_should_equal_six()
        {
            var equals = new Equals<int>
                {
                    Child1 = new Constant<int>(6),
                    Child2 = new Constant<int>(6)
                };
            equals.Evaluate(new MockAiContext()).ShouldBeTrue();
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
            expression.Evaluate(new MockAiContext()).ShouldBeTrue();
        }

        [Test]
        public void Five_should_not_equal_six()
        {
            var expression = new Equals<int>
                {
                    Child1 = new Constant<int>(5),
                    Child2 = new Constant<int>(6)
                };
            expression.Evaluate(new MockAiContext()).ShouldBeFalse();
        }

        [Test]
        public void One_should_be_less_than_two()
        {
            new LessThan
                {
                    Child1 = new Constant<int>(1),
                    Child2 = new Constant<int>(2)
                }
                .Evaluate(new MockAiContext()).ShouldBeTrue();
        }


        [Test]
        public void Two_should_be_greater_than_one()
        {
            new GreaterThan
            {
                Child1 = new Constant<int>(2),
                Child2 = new Constant<int>(1)
            }
                .Evaluate(new MockAiContext()).ShouldBeTrue();
        }

        [Test]
        public void Four_plus_four_should_be_greater_than_seven()
        {
            new GreaterThan
                {
                    Child1 = new Plus
                        {
                            Child1 = new Constant<int>(4),
                            Child2 = new Constant<int>(4)
                        },
                        Child2 = new Constant<int>(7)
                }.Evaluate(new MockAiContext()).ShouldBeTrue();
        }

        [Test]
        public void combining_two_sets_of_response_votes()
        {
            var response = new DeclineToPurchaseResponse(new MockTurnScope(new MockEventAggregator()));
            var response2 = new BuyCardResponse(new MockTurnScope(new MockEventAggregator()), Treasure.Silver);
            new CombineVotes()
                {
                    Child1 = new Constant<ResponseVotes>()
                        {
                            ValueAccessor = () => new ResponseVotes()
                                .VoteFor(response, 1)
                                .VoteFor(response2, 1)
                        },
                    Child2 = new Constant<ResponseVotes>()
                        {
                            ValueAccessor = () => new ResponseVotes()
                                .VoteFor(response, 2)
                        }
                }.Evaluate(new MockAiContext()).Winner.ShouldEqual(response);

        }

        [Test]
        public void If_true_returns_first_option()
        {
            new If<int>
                {
                    Child1 = new Constant<bool>(true),
                    Child2 = new Constant<int>(1),
                    Child3 = new Constant<int>(2)
                }.Evaluate(new MockAiContext()).ShouldEqual(1);
        }

        [Test]
        public void If_false_returns_second_option()
        {
            new If<int>
            {
                Child1 = new Constant<bool>(false),
                Child2 = new Constant<int>(1),
                Child3 = new Constant<int>(2)
            }.Evaluate(new MockAiContext()).ShouldEqual(2);
        }
    }
}