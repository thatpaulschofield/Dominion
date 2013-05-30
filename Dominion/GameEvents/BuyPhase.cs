using System;
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
                    (from card in TurnScope.Supply.FindCardsEligibleForPurchase(turnScope).OrderByDescending(c => c.Cost)
                     select new BuyCardResponse(turnScope, card)
                         {
                             Description = String.Format("Buy a {0} ({1})  {2} remaining", card.Name, card.Cost, TurnScope.Supply[card].Count)
                         }).Union(new GameEventResponse[] { new DeclineToPurchaseResponse(turnScope) })
                        ;
        }

        public override IEventResponse GetDefaultResponse()
        {
            if (CardsAvailable().Any())
            {
                var cardToPurchase = CardsAvailable().VictoryCards()
                                                        .OrderByDescending(v => v.VictoryPoints)
                                                        .FirstOrDefault()
                                     ?? CardsAvailable().Treasures().OrderByDescending(t => t.Coins).FirstOrDefault()
                                     ?? CardsAvailable()[0];
                return new BuyCardResponse(TurnScope, cardToPurchase);
            }

            return new SkipBuyPhaseResponse(TurnScope);
        }

        private CardSet CardsAvailable()
        {
            return new CardSet(GetAvailableResponses()
                .Where(r => r is BuyCardResponse)
                .Cast<BuyCardResponse>().Select(r => r.CardToPurchase));
        }

        public override string ToString()
        {
            string purchases = CardsAvailable().Aggregate("", (x, c) => x + " - " + c.Name);
            return String.Format("{0}: Buys ({1}), available cards: [{2}]", TurnScope.Player.Name, TurnScope.Buys, purchases);
        }
    }
}