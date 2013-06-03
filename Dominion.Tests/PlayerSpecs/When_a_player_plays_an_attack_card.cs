using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominion.Cards.BasicSet;
using Dominion.Cards.BasicSet.Actions;
using NUnit.Framework;

namespace Dominion.Tests.PlayerSpecs
{
    [TestFixture]
    public class When_a_player_plays_an_attack_card : AbstractPlayerSpec
    {
        protected override void When()
        {
            var scope = new TurnScope(SUT, Container.GetInstance<Supply>(), Container.GetInstance<IEventAggregator>());
            SUT.Handle(new AttackCardPlayed(BasicCards.Actions.Militia, scope), scope);

        }
    }
}
