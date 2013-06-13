using System.Collections.Generic;
using System.Linq;
using Dominion.GameEvents;

namespace Dominion.Cards.BasicSet.Actions
{
    public class Thief : TypedCard<Thief>
    {
        public Thief() : base(cost: 4, isAction: true, isAttack: true)
        {
        }

        public override void PlayAsAction(ITurnScope turnScope)
        {
            turnScope.Publish(new ThiefAttackEffect(turnScope));
        }

        public class ThiefAttackEffect : GameMessage, IAttackEffect
        {
            public ThiefAttackEffect(IActionScope scope) : base(scope)
            {
            }

            public void Resolve(IReactionScope scope)
            {
                var revealedTreasures = new CardSet();
                2.Times(() =>
                    {
                        var revealedCard = scope.RevealCardFromDeck();
                        if (revealedCard.IsTreasure && !revealedTreasures.Contains(revealedCard))
                        {
                            revealedTreasures.Add(revealedCard, scope);
                        }
                    });
                if (revealedTreasures.Count() == 1)
                {
                    scope.Publish(new TrashTreasureCardFromThief(TurnScope, scope, revealedTreasures.Single()));
                }
                else if (revealedTreasures.Count() == 2)
                {
                    TurnScope.Publish(new ChooseTreasureCardToTrashFromThief(TurnScope, scope, revealedTreasures));
                }
            }
        }

        public class ChooseTreasureCardToTrashFromThief : GameMessage
        {
            public ChooseTreasureCardToTrashFromThief(IActionScope turnScope, IReactionScope reactionScope, CardSet cards) : base(turnScope)
            {
                Description = "Choose a treasure card for " + reactionScope.ReactingPlayer.Name + " to trash [Thief]";
                GetAvailableResponses =
                    () => cards.Select(card => new TrashTreasureCardFromThief(turnScope, reactionScope, card));
            }
        }

        public class TrashTreasureCardFromThief : GameEventResponse<ChooseTreasureCardToTrashFromThief, Card>
        {
            private readonly IActionScope _reactionScope;

            public TrashTreasureCardFromThief(IActionScope scope, IActionScope reactionScope, Card item) : base(scope, item)
            {
                Description = "Trash " + item.Name + " [Thief]";
                _reactionScope = reactionScope;
                GetAvailableResponses = () => new List<IEventResponse>{ this };
            }

            public override void Execute()
            {
                _reactionScope.PutCardInTrash(Item);
                TurnScope.Publish(new ChooseWhetherToGainTrashedTreasureForThief(TurnScope, Item));
            }
        }


        public class ChooseWhetherToGainTrashedTreasureForThief : GameEventResponse<TrashTreasureCardFromThief, Card>
        {
            public ChooseWhetherToGainTrashedTreasureForThief(IActionScope turnScope, Card item) : base(turnScope, item)
            {
                Description = turnScope.Player.Name + ", would you like to gain the trashed treasure: " + item.Name + " [Thief]";
                GetAvailableResponses = () => new List<IEventResponse>
                    {
                        new GainTrashedTreasureForThief(turnScope, Item),
                        new DoNotGainTrashedTreasureForThief(turnScope, Item)
                    };
            }

            public override void Execute()
            {
            }
        }

        public class GainTrashedTreasureForThief : GameEventResponse<TrashTreasureCardFromThief, Card>
        {
            public GainTrashedTreasureForThief(IActionScope turnScope, Card item) : base(turnScope, item)
            {
                Description = "Yes";
            }

            public override void Execute()
            {
                TurnScope.GainCardFromSupplyOntoTopOfDeck(Item);
            }
        }

        public class DoNotGainTrashedTreasureForThief : GameEventResponse<TrashTreasureCardFromThief, Card>
        {
            public DoNotGainTrashedTreasureForThief(IActionScope turnScope, Card item)
                : base(turnScope, item)
            {
                Description = "No";
            }

            public override void Execute()
            {
            }
        }
    }
}