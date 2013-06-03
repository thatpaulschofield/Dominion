using System;
using Dominion.Cards;

namespace Dominion.GameEvents
{
    public class BuyCardResponse : GameEventResponse
    {
        public Card CardToPurchase { get; private set; }

        public BuyCardResponse(ITurnScope turnScope, Card cardToPurchase) : base(turnScope)
        {
            CardToPurchase = cardToPurchase;
        }

        public override void Execute()
        {
            TurnScope.PerformBuy(CardToPurchase.CardType);
        }

        public override string ToString()
        {
            return String.Format("{0} intends to buy card {1}", TurnScope.ActingPlayer.Name, CardToPurchase.Name);
        }
    }
}