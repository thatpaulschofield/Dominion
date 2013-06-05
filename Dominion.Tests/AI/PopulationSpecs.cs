using System;
using System.Linq;
using Dominion.AI;
using Dominion.AI.Functions;
using Dominion.Ai;
using Dominion.Ai.Nodes;
using Dominion.Ai.Nodes.Functions;
using Dominion.Ai.TreeBuilding;
using NUnit.Framework;
using Should;
using StructureMap;

namespace Dominion.Tests.AI
{
    public class PopulationSpecs
    {
        private IContainer _container;
        private INode _tree;

        [SetUp]
        public void SetUp()
        {
            _container = new AiBootstrapper().BootstrapContainer();
            var strategy = _container.GetInstance<FullTreeStrategy>();
            var spec = new TreeSpec { MaxDepth = 10 };

            strategy.WithSpec(spec);
            var treeBuilder = _container.GetInstance<TreeBuilder>();
            _tree = treeBuilder.BuildTree<CombineVotes>(strategy);
            var size = new TreeSizeVisitor();
            _tree.Receive(size);
            
        }

        [Test]
        public void Leaf_nodes_should_be_populated_with_Terminals()
        {
            var enumerator = new NodeEnumerator();
            _tree.Receive(enumerator);
            enumerator.Nodes
                      .Any(n => n is NullNode 
                          || n.GetType().IsInstanceOfType(typeof(NullNode<>))
                          || typeof(NullNode<>).IsInstanceOfType(n)
                          )
                      .ShouldBeFalse();

        }

        [Test]
        public void FullTreeStrategy_can_build_a_tree()
        {
            Console.WriteLine(new PrettyPrinter().Print(_tree));
        }


    }
}