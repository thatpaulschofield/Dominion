using Dominion.Cards;

namespace Dominion.Ai.Nodes
{
    public interface NodeVisitor
    {
        void Visit(INode node);
        void Visit(IWantToViewCardsSupply node);
    }

    public interface IWantToViewCardsSupply
    {
        void ShowSupply(CardSet supply);
    }
}