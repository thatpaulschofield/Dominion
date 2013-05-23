using System.Linq;
using Dominion.AI;
using Dominion.Cards;

namespace Dominion.GameEvents
{
    public class BuyPhase : GameEvent
    {
        private readonly TurnScope _turnScope;
        private readonly CardSet _availablePurchases;

        public BuyPhase(TurnScope turnScope)
        {
            _turnScope = turnScope;

            _availablePurchases = _turnScope.Supply.FindCardsEligibleForPurchase(turnScope);
        }

        public GameEventResponse BuyCard(Card card, CardSet treasuresToPlay)
        {
            if (_availablePurchases.Contains(card))
                return new BuyCardResponse(_turnScope, card, treasuresToPlay);

            return new SkipBuyPhaseResponse(_turnScope);
        }

        public override GameEventResponse GetDefaultResponse()
        {
            if (_availablePurchases.Any())
                return new BuyCardResponse(_turnScope, _availablePurchases[0], _turnScope.Player.Hand.Treasures());

            return new SkipBuyPhaseResponse(_turnScope);
        }
    }
}