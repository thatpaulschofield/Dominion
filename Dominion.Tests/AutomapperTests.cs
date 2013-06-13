using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Dominion.Ai;
using NUnit.Framework;

namespace Dominion.Tests
{
    public class AutomapperTests
    {
        [Test]
        public void Automapper_configuration_should_be_valid()
        {
            AutomapperConfig.ConfigureMappings(new AiBootstrapper().BootstrapContainer());
            Mapper.AssertConfigurationIsValid();
        }
    }
}
