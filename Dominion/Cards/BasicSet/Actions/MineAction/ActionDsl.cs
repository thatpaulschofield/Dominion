namespace Dominion.Cards.BasicSet.Actions.MineAction
{
    public class ActionDsl
    {
        private readonly CardStateDsl _cardStateDsl;
        private readonly StateMachineGameMessage _message;

        public ActionDsl(CardStateDsl cardStateDsl, StateMachineGameMessage message)
        {
            _cardStateDsl = cardStateDsl;
            _message = message;
        }

        public CardState Build()
        {
            return new CardState(_message);
        }
    }
}