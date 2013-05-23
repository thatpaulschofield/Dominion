using Dominion.Cards;

namespace Dominion.GameEvents
{
    public class BuyCardResponse : GameEventResponse
    {
        public Card CardToPurchase { get; private set; }
        public CardSet TreasuresToPlay { get; private set; }

        public BuyCardResponse(TurnScope turnScope, Card cardToPurchase, CardSet treasuresToPlay) : base(turnScope)
        {
            CardToPurchase = cardToPurchase;
            TreasuresToPlay = treasuresToPlay;
        }

        public override void Execute()
        {
            _turnScope.PerformBuy(CardToPurchase.CardType, TreasuresToPlay);
        }
    }
}