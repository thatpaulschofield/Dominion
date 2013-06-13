using Dominion.Ai.Nodes;

namespace Dominion.Ai.TreeBuilding
{
    public class TreeSizeVisitor : NodeVisitor
    {
        private int _count = 0;
        public void Visit(INode node)
        {
            _count++;
        }
    }
}