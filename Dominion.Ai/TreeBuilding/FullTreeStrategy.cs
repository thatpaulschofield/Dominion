using System;
using System.Collections.Generic;
using System.Linq;
using Dominion.AI;
using Dominion.Ai.ConstantValueProviders;
using Dominion.Ai.Nodes;
using Dominion.Ai.Nodes.Terminals;

namespace Dominion.Ai.TreeBuilding
{
    public class FullTreeStrategy : ITreeBuildStrategy
    {
        private readonly NodeRegistry _nodeRegistry;
        private readonly IValueProviderRegistry _valueProviderRegistry;
        private TreeSpec _spec;
        private Random _random = new Random((int)DateTime.Now.Ticks);

        public FullTreeStrategy(NodeRegistry nodeRegistry, IValueProviderRegistry valueProviderRegistry)
        {
            _nodeRegistry = nodeRegistry;
            _valueProviderRegistry = valueProviderRegistry;
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
            PopulateNodeChildren(node as Function, () => GetAllowableSetOfNodesForDepth(maxDepth), maxDepth);
        }

        IEnumerable<INode> GetAllowableSetOfNodesForDepth(int depth)
        {
            if (depth <= 1)
                return _nodeRegistry.TerminalSet;
            return _nodeRegistry.FunctionSet.Union(_nodeRegistry.TerminalSet);
        }

        private void PopulateNodeChildren(Function node, Func<IEnumerable<INode>> availableChildren, int maxDepth)
        {
            while (node.HasUnassignedChildNode)
            {
                int index = node.GetIndexOfNextUnassignedChild();
                var availableChildNodesOfCorrectReturnType = 
                    AvailableChildNodesOfCorrectReturnType(node, availableChildren(), index);
                node[index] = GetRandomNode(availableChildNodesOfCorrectReturnType);

                if (maxDepth >=0)
                    BuildNextNode(node[index], maxDepth - 1);
            }
        }

        private void EnsureInitialValueOfConstantIsPopulated(INode node)
        {
            var constant = node as Constant;
            if (constant == null)
                return;

            _valueProviderRegistry
                .GetProviderFor(constant)
                .ProvideValue(constant);
        }

        private IEnumerable<INode> AvailableChildNodesOfCorrectReturnType(Function node, IEnumerable<INode> availableChildren, int index)
        {
            var result = availableChildren.Where(n => n.ReturnType == node[index].ReturnType).ToList();
            if (!result.Any())
                // use terminal set as a backup if no functions are available that return the desired type
                result = _nodeRegistry.FunctionSet.Where(n => n.ReturnType == node[index].ReturnType).ToList(); 
            return result;
        }

        private INode GetRandomNode(IEnumerable<INode> functions)
        {
            if (!functions.Any())
                return null;

            var fList = functions.ToList();

            int index = _random.Next(0, fList.Count() - 1);
            return fList[index];

        }
    }
}