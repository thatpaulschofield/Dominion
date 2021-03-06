﻿using System;
using System.Linq;
using Dominion.Cards;

namespace Dominion.GameEvents
{
    public class BuyPhase : GameCommand
    {
        public BuyPhase(ITurnScope turnScope) : base(turnScope)
        {
            Description = String.Format("{0}, select a card to buy", turnScope.Player.Name);
            GetAvailableResponses = () =>
                    (from card in TurnScope.Supply.FindCardsEligibleForPurchase(turnScope).OrderByDescending(c => c.BaseCost)
                     select new BuyCardResponse(turnScope, card)
                         {
                         }).Union(new GameEventResponse[] { new DeclineToPurchaseResponse(turnScope) })
                        ;
        }

        public override IEventResponse GetDefaultResponse()
        {
            if (CardsAvailable().Any())
            {
                var cardToPurchase = CardsAvailable().FirstOrDefault(c => c.Name == "Province")
                                     ?? CardsAvailable().FirstOrDefault(c => c.Name == "Gold")
                                     ?? CardsAvailable().FirstOrDefault(c => c.Name == "Silver");
                if (cardToPurchase != null)
                    return new BuyCardResponse(TurnScope, cardToPurchase);
            }

            return new DeclineToPurchaseResponse(TurnScope);
        }

        private CardSet CardsAvailable()
        {
            return new CardSet(GetAvailableResponses()
                .Where(r => r is BuyCardResponse)
                .Cast<BuyCardResponse>().Select(r => r.Item));
        }

        public override string ToString()
        {
            string purchases = CardsAvailable().Aggregate("", (x, c) => x + " - " + c.Name);
            return String.Format("{0}: Buys ({1}), available cards: [{2}]", TurnScope.Player.Name, TurnScope.Buys, purchases);
        }
    }
}