using Dominion.Ai.Nodes;

namespace Dominion.Tests.AI
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