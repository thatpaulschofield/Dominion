using Dominion.AI;
using Dominion.Cards;

namespace Dominion.GameEvents
{
    public class BuyPhase : GameEvent
    {
        private readonly TurnScope _turnScope;
        private readonly CardSet _availablePurchases;

        public BuyPhase(TurnScope turnScope, CardSet availablePurchases)
        {
            _turnScope = turnScope;
            _availablePurchases = availablePurchases;
        }

        public GameEventResponse BuyCard(Card card)
        {
            if (_availablePurchases.Contains(card))
                return new BuyCardResponse(_turnScope, card);

            return new SkipBuyPhaseResponse(_turnScope);
        }

        public override GameEventResponse GetDefaultResponse()
        {
            return new BuyCardResponse(_turnScope, _availablePurchases[0]);
        }
    }

    public class BuyCardResponse : GameEventResponse
    {
        public Card CardToPurchase { get; private set; }

        public BuyCardResponse(TurnScope turnScope, Card cardToPurchase) : base(turnScope)
        {
            CardToPurchase = cardToPurchase;
        }

        public override void Execute()
        {
            _turnScope.Discard(_turnScope.Supply.AcquireCard(CardToPurchase.CardType));
            
        }
    }
}