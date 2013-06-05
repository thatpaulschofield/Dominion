using Dominion.Cards;

namespace Dominion.Ai.Nodes
{
    public interface NodeVisitor
    {
        void Visit(INode node);
    }
}