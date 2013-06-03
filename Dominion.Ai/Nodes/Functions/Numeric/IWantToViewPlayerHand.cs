using Dominion.Cards;

namespace Dominion.Ai.Nodes.Functions.Numeric
{
    public interface IWantToViewPlayerHand
    {
        void ShowPlayerHand(CardSet hand);
    }
}