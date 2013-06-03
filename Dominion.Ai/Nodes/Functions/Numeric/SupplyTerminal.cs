using Dominion.AI;
using Dominion.Cards;

namespace Dominion.Ai.Nodes.Functions.Numeric
{
    public class SupplyTerminal : Terminal<CardSet>, IWantToViewCardsSupply
    {
        private CardSet _supply;

        public override CardSet Evaluate(IAiContext context)
        {
            return new CardSet(_supply ?? new CardSet());
        }

        public void ShowSupply(CardSet supply)
        {
            _supply = supply;
        }

        public override void Receive(NodeVisitor visitor)
        {
            visitor.Visit(this as IWantToViewCardsSupply);
            base.Receive(visitor);
            
        }
    }
}