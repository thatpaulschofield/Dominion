namespace Dominion.Cards
{
    public abstract class DeckSet
    {
        public abstract ISupplyBuilder Build(ISupplyBuilder builder);
    }
}