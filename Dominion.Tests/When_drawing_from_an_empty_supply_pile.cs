using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominion.Cards.BasicSet.Treasures;
using Dominion.Tests.GameEvents;
using NUnit.Framework;
using Should;
using SpecsFor;

namespace Dominion.Tests
{
    public class When_drawing_from_an_empty_supply_pile : SpecsFor<Supply>
    {
        private Exception _caughtException;

        protected override void ConfigureContainer(StructureMap.IContainer container)
        {
            container.Configure(cfg =>
                {
                    cfg.For<Supply>().Use(new Supply(new SupplyPile(1, Treasure.Copper, new MockEventAggregator())));
                });
        }

        protected override void Given()
        {
            SUT.AcquireCard(Treasure.Copper, new MockTurnScope());
        }

        protected override void When()
        {
            try
            {
                SUT.AcquireCard(Treasure.Copper, new MockTurnScope());
            }
            catch (Exception ex)
            {
                _caughtException = ex;
            }
        }

        [Test]
        public void Should_not_allow_acquisition()
        {
            _caughtException.ShouldNotBeNull();
        }
    }
}
