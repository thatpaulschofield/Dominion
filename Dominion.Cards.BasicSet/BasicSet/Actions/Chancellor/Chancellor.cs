using System.Collections.Generic;
using System.Linq;
using Dominion.GameEvents;

namespace Dominion.Cards.BasicSet.Actions
{
    public class Chancellor : TypedCard<Chancellor>
    {
        public Chancellor() : base(cost: 3, isAction:true, name: "Chancellor")
        {
        }

        public override void PlayAsAction(ITurnScope turnScope)
        {
            turnScope.ChangeState(2.TurnCoins());
            turnScope.Publish(new ChooseWhetherToPutDeckInDiscardPileFromChancellor(turnScope));
        }

        public class ChooseWhetherToPutDeckInDiscardPileFromChancellor : GameMessage
        {
            public ChooseWhetherToPutDeckInDiscardPileFromChancellor(IActionScope scope) : base(scope)
            {
                Description = "Place deck in discard pile [Chancellor]?";
                GetAvailableResponses = () => new List<IEventResponse>
                    {
                        new PlaceDeckInDiscardPileForChancellor(scope),
                        new DoNotPlaceDeckInDiscardPileForChancellor(scope)
                    };
            }

            public override IEventResponse GetDefaultResponse()
            {
                return GetAvailableResponses().Last();
            }
        }

        public class PlaceDeckInDiscardPileForChancellor : GameEventResponse<ChooseWhetherToPutDeckInDiscardPileFromChancellor>
        {
            public PlaceDeckInDiscardPileForChancellor(IActionScope scope) : base(scope)
            {
                Description = "Put deck into discard pile [Chancellor].";
            }

            public override void Execute()
            {
                TurnScope.MoveCardsFrom(TurnScope.Deck).Into(TurnScope.DiscardPile, TurnScope);
            }
        }

        public class DoNotPlaceDeckInDiscardPileForChancellor : GameEventResponse<ChooseWhetherToPutDeckInDiscardPileFromChancellor>
        {
            public DoNotPlaceDeckInDiscardPileForChancellor(IActionScope scope) : base(scope)
            {
                Description = "Do not put deck into discard pile [Chancellor].";
            }

            public override void Execute()
            {
                
            }
        }
    }
}