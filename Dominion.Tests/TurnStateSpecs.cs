using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Should;

namespace Dominion.Tests
{
    [TestFixture]
    class TurnStateSpecs
    {
        [Test]
        public void One_action_plus_one_action_should_equal_two_actions()
        {
                        (new TurnState(1, 0, 0) 
                       + new TurnState(1, 0, 0))
            .ShouldEqual(new TurnState(2, 0, 0));
        }

        [Test]
        public void One_buy_plus_one_buy_should_equal_two_buys()
        {
                        (new TurnState(1, 0, 0) 
                       + new TurnState(1, 0, 0))
            .ShouldEqual(new TurnState(2, 0, 0));
        }

        [Test]
        public void One_coin_plus_one_coin_should_equal_two_coins()
        {
                        (new TurnState(0, 0, 1)
                       + new TurnState(0, 0, 1))
            .ShouldEqual(new TurnState(0, 0, 2));
        }

        [Test]
        public void One_action_should_equal_one_action()
        {
            new TurnState(1, 0, 0).ShouldEqual(new TurnState(1, 0, 0));
        }

        [Test]
        public void Two_actions_should_not_equal_one_action()
        {
            new TurnState(2, 0, 0).ShouldNotEqual(new TurnState(1, 0, 0));
        }

        [Test]
        public void One_buy_should_equal_one_buy()
        {
            new TurnState(0, 1, 0).ShouldEqual(new TurnState(0, 1, 0));
        }

        [Test]
        public void Two_buys_should_not_equal_one_buy()
        {
            new TurnState(0, 2, 0).ShouldNotEqual(new TurnState(2, 0, 0));
        }


        [Test]
        public void One_coin_should_equal_one_coin()
        {
            new TurnState(0, 0, 1).ShouldEqual(new TurnState(0, 0, 1));
        }

        [Test]
        public void Two_coins_should_not_equal_one_coins()
        {
            new TurnState(0, 0, 2).ShouldNotEqual(new TurnState(0, 0, 1));
        }
    }
}
