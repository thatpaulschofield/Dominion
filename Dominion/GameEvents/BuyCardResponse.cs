using System;
using Dominion.Cards;

namespace Dominion.GameEvents
{
    public class BuyCardResponse : GameEventResponseWithItem<Card, BuyPhase>
    {
        //public Card Item { get; private set; }

        public BuyCardResponse(ITurnScope turnScope, Card cardToPurchase) : base(turnScope)
        {
            Item = cardToPurchase;
        }

        public override void Execute()
        {
            TurnScope.PerformBuy(Item.CardType);
        }

        public override string ToString()
        {
            return String.Format("{0} intends to buy card {1}", TurnScope.ActingPlayer.Name, Item.Name);
        }
    }
}