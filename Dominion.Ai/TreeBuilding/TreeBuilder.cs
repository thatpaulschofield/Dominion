using Dominion.AI;
using Dominion.Ai.Nodes;

namespace Dominion.Ai.TreeBuilding
{
    public class TreeBuilder
    {
        public INode BuildTree<TROOTNODE>(ITreeBuildStrategy strategy) where TROOTNODE : INode, new()
        {
            var root = new TROOTNODE();
            strategy.BuildNextNode(root);
            return root;
        }
    }
}