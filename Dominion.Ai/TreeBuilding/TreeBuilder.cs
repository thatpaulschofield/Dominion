using Dominion.AI;
using Dominion.Ai.Nodes;
using Dominion.Ai.Nodes.Functions;

namespace Dominion.Ai.TreeBuilding
{
    public class TreeBuilder
    {

        public TreeBuilder()
        {
        }

        public INode BuildTree<TROOTNODE>(ITreeBuildStrategy strategy) where TROOTNODE : INode, new()
        {
            var root = new CombineVotes();
            strategy.BuildNextNode(root);
            return root;
        }
    }
}