using Dominion.Cards;

namespace Dominion.GameEvents
{
    public interface ICardCostModifierEffect
    {
        Money CalculateCostAdjustment(Card card, IActionScope scope);
    }
}