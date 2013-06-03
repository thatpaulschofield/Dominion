using System;
using Dominion.AI;
using Dominion.AI.Functions;
using Dominion.Ai;
using Dominion.Ai.Nodes;
using Dominion.Ai.TreeBuilding;
using NUnit.Framework;
using StructureMap;

namespace Dominion.Tests.AI
{
    public class PopulationSpecs
    {
        private IContainer _container;

        [Test]
        public void FullTreeStrategy_can_build_a_tree()
        {
            _container = new AiBootstrapper().BootstrapContainer();
            var strategy = _container.GetInstance<FullTreeStrategy>();
            var spec = new TreeSpec {MaxDepth = 10};

            strategy.WithSpec(spec);
            var treeBuilder = new TreeBuilder();
            var tree = treeBuilder.BuildTree<CombineVotes>(strategy);
            var size = new TreeSizeVisitor();
            tree.Receive(size);
            Console.WriteLine(new PrettyPrinter().Print(tree));
        }
    }
}