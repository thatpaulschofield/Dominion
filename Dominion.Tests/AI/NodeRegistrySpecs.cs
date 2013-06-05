using Dominion.Ai;
using Dominion.Ai.Nodes;
using NUnit.Framework;
using StructureMap;

namespace Dominion.Tests.AI
{
    public class NodeRegistrySpecs
    {
        [Test]
        public void NodeRegistry_can_scan_assembly_without_crashing()
        {
            var registry = new NodeRegistry(new Container(), new MockValueProviderRegistry());
        }
    }
}