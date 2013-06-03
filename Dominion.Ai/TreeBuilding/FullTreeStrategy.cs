using System;
using System.Collections.Generic;
using System.Linq;
using Dominion.AI;
using Dominion.Ai.Nodes;
using Dominion.Ai.Nodes.Terminals;

namespace Dominion.Ai.TreeBuilding
{
    public class FullTreeStrategy : ITreeBuildStrategy
    {
        private readonly NodeRegistry _nodeRegistry;
        private TreeSpec _spec;

        public FullTreeStrategy(NodeRegistry nodeRegistry)
        {
            _nodeRegistry = nodeRegistry;
        }

        public ITreeBuildStrategy WithSpec(TreeSpec spec)
        {
            _spec = spec;
            return this;
        }

        public void BuildNextNode(INode rootNode)
        {
            BuildNextNode(rootNode, _spec.MaxDepth);
        }

        public void BuildNextNode(INode node, int maxDepth)
        {
            EnsureInitialValueOfConstantIsPopulated(node);

            if (!(node is Function))
                return;
            PopulateNodeChildren(node as Function, maxDepth <= 1 ?  _nodeRegistry.TerminalSet : _nodeRegistry.FunctionSet, maxDepth);
        }

        private void EnsureInitialValueOfConstantIsPopulated(INode node)
        {
            var constant = node as Constant;
            if (constant == null)
                return;
        }

        private void PopulateNodeChildren(Function node, IEnumerable<INode> availableChildren, int maxDepth)
        {
            for (int i = 1; i<=node.Arity; i++)
            {
                int index = i;
                var availableChildNodesOfCorrectReturnType = 
                    AvailableChildNodesOfCorrectReturnType(node, availableChildren, index);
                node[index] = GetRandomNode(availableChildNodesOfCorrectReturnType);

                if (maxDepth >=0)
                    BuildNextNode(node[index], maxDepth - 1);
            }
        }

        private static IEnumerable<INode> AvailableChildNodesOfCorrectReturnType(Function node, IEnumerable<INode> availableChildren, int index)
        {
            return availableChildren.Where(n => n.ReturnType == node[index].ReturnType);
        }

        private INode GetRandomNode(IEnumerable<INode> functions)
        {
            if (!functions.Any())
                return new NullNode();

            var fList = functions.ToList();

            int index = new Random((int)DateTime.Now.Ticks).Next(0, fList.Count() - 1);
            return fList[index];

        }
    }
}