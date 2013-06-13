using System;
using Dominion.Cards;

namespace Dominion.GameEvents
{
    public class BuyCardResponse : GameEventResponseWithItem<Card, BuyPhase>
    {
        public BuyCardResponse(ITurnScope turnScope, Card cardToPurchase) : base(turnScope)
        {
            Item = cardToPurchase;
            Description = String.Format("Buy a {0} {1} [{2} remaining]", Item.Name, TurnScope.GetPrice(Item),
                TurnScope.Supply == null || !TurnScope.Supply.ContainsKey(Item) ? 0 : TurnScope.Supply[Item].Count);
        }

        public override void Execute()
        {
            TurnScope.PerformBuy(Item.CardType);

        }

        public override string ToString()
        {
            return String.Format("{0}: bought {1}", TurnScope.Player.Name, Item.Name);
        }
    }
}