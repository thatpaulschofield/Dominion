using Dominion.GameEvents;
using System.Linq;

namespace Dominion.Cards.BasicSet.Actions
{
    public class Workshop : TypedCard<Workshop>
    {
        public Workshop()
            : base(isAction: true, cost: 3, name: "Workshop")
        {

        }

        public override void PlayAsAction(ITurnScope turnScope)
        {
            turnScope.Publish(new GainCardCostingUpToMessage(4.Coins(), turnScope));
        }
    }

    public class GainCardCostingUpToMessage : GameMessage
    {
        public GainCardCostingUpToMessage(Money maxCost, ITurnScope turnScope) : base(turnScope)
        {
            Description = "Select a card costing up to " + maxCost + ".";
            GetAvailableResponses = () =>
                                    turnScope.Supply.FindCardsCostingUpTo(maxCost, turnScope).OrderByDescending(c => c.BaseCost)
                                             .Select(c => new CardSelectedToGainResponse(c, turnScope));
        }
        
    }

    public class CardSelectedToGainResponse : GameEventResponseWithItem<Card, GainCardCostingUpToMessage>
    {
        public CardSelectedToGainResponse(Card card, ITurnScope turnScope) : base(card, turnScope)
        {
            Description = card.Name;
        }

        public override void Execute()
        {
            TurnScope.GainCardFromSupply(Item);
        }
    }
}