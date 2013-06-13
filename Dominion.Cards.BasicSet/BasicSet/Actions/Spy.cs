using System.Collections.Generic;
using Dominion.GameEvents;

namespace Dominion.Cards.BasicSet.Actions
{
    public class Spy : TypedCard<Spy>
    {
        public Spy() : base(cost: 4, isAction:true, isAttack: true)
        {
            
        }
        public override void PlayAsAction(ITurnScope turnScope)
        {
            turnScope.ChangeState(1.TurnActions());
            turnScope.Player.DrawIntoHand(1, turnScope);
            turnScope.Publish(new ChooseToDiscardOrReturnTopCardFromSpy(turnScope, turnScope));
            turnScope.Publish(new SpyAttackEffect(turnScope));
        }

        public class SpyAttackEffect : GameMessage, IAttackEffect
        {

            public SpyAttackEffect(IActionScope scope) : base(scope)
            {
            }

            public void Resolve(IReactionScope scope)
            {
                TurnScope.Publish(new ChooseToDiscardOrReturnTopCardFromSpy(TurnScope, scope));
            }
        }

        public class ChooseToDiscardOrReturnTopCardFromSpy : GameMessage
        {
            public ChooseToDiscardOrReturnTopCardFromSpy(IActionScope scope, IActionScope reactionScope) : base(scope)
            {
                var card = reactionScope.RevealCardFromDeck();
                Description = reactionScope.Player.Name + " revealed " + card.Name + " [Spy]";
                GetAvailableResponses = () => new List<IEventResponse>
                    {
                        new DiscardTopCardFromSpyResponse(scope, reactionScope, card),
                        new ReturnTopCardFromSpyResponse(scope, reactionScope, card)
                    };
            }
        }

        public class DiscardTopCardFromSpyResponse : GameEventResponse<ChooseToDiscardOrReturnTopCardFromSpy, Card>
        {
            private readonly IActionScope _reactionScope;

            public DiscardTopCardFromSpyResponse(IActionScope scope, IActionScope reactionScope, Card card)
                : base(scope, card)
            {
                Description = "Discard " + card.Name + " [Spy]";
                _reactionScope = reactionScope;
            }

            public override void Execute()
            {
                _reactionScope.PutCardsIntoDiscardPile(Item);
            }
        }
        public class ReturnTopCardFromSpyResponse : GameEventResponse<ChooseToDiscardOrReturnTopCardFromSpy, Card>
        {
            private readonly IActionScope _reactionScope;

            public ReturnTopCardFromSpyResponse(IActionScope scope, IActionScope reactionScope, Card card)
                : base(scope, card)
            {
                Description = "Return " + card.Name + " to top of deck [Spy]";
                _reactionScope = reactionScope;
            }

            public override void Execute()
            {
                _reactionScope.PutCardOnTopOfDeck(Item);
            }
        }

    }
}