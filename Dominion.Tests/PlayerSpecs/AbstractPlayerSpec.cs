using System;
using NUnit.Framework;
using SpecsFor;
using StructureMap;

namespace Dominion.Tests.PlayerSpecs
{
    internal abstract class AbstractGameSpec : SpecsFor<Game>
    {
        protected IContainer Container;

        protected override void ConfigureContainer(IContainer container)
        {
            try
            {
                this.Container = container;
                base.ConfigureContainer(container);
            }
            catch (Exception)
            {
                
                throw;
            }
        }

    }
    internal abstract class AbstractPlayerSpec : SpecsFor<Player>
    {
        protected IContainer Container;

        protected override void ConfigureContainer(IContainer container)
        {
            try
            {
                this.Container = container;
                base.ConfigureContainer(container);
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}