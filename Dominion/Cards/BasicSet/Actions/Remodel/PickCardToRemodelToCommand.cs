using System;
using System.Linq;
using Dominion.GameEvents;

namespace Dominion.Cards.BasicSet.Actions.Remodel
{
    public class PickCardToRemodelToCommand : GameMessage, IPlayerScoped
    {
        public PickCardToRemodelToCommand(Money cost, ITurnScope turnScope) : base(turnScope)
        {
            Description = String.Format("Select a card that costs up to {0} coins to remodel to.", cost);
            GetAvailableResponses = 
                () =>
                    turnScope.Supply.FindCardsCostingUpTo(cost).Select
                        (c => new CardSelectedToRemodelToResponse(c, TurnScope));
        }

        
    }
}