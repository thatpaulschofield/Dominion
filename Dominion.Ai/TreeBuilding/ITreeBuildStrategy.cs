using Dominion.AI;
using Dominion.Ai.Nodes;

namespace Dominion.Ai.TreeBuilding
{
    public interface ITreeBuildStrategy
    {
        ITreeBuildStrategy WithSpec(TreeSpec spec);
        void BuildNextNode(INode rootNode, int maxDepth);
        void BuildNextNode(INode rootNode);
    }
}