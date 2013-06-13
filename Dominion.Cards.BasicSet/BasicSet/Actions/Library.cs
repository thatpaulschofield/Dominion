using System.Collections.Generic;
using System.Linq;
using Dominion.GameEvents;

namespace Dominion.Cards.BasicSet.Actions
{
    public class Library : TypedCard<Library>
    {
        public Library() : base(cost: 5, isAction: true)
        {
        }

        public override void PlayAsAction(ITurnScope turnScope)
        {
            while (turnScope.Player.Hand.Count() < 7)
            {
                var state = new LibraryState();
                turnScope.DrawCardIntoCardset(state.DrawnCards);
                var card = state.DrawnCards.First();
                if (card.IsAction)
                    turnScope.Publish(new ActionDrawnForLibrary(card, state, turnScope));
                else
                    state.DrawnCards.Add(card, turnScope);

                turnScope.PutCardsIntoHand(state.DrawnCards);
                turnScope.Discard(state.SetAsideCards);
            }
        }

        public class ActionDrawnForLibrary : GameMessage
        {
            private readonly Card _action;

            public ActionDrawnForLibrary(Card action, LibraryState state, ITurnScope scope) : base(scope)
            {
                Description = "Take " + action.Name + " into hand, or set it aside?";
                _action = action;
                GetAvailableResponses = () => new List<IEventResponse>
                    {
                        new SetAsideActionForLibrary(scope, state, action),
                        new TakeActionCardIntoHandForLibrary(scope, state, action)
                    };
            }
        
        }

        public class SetAsideActionForLibrary : GameEventResponse<ActionDrawnForLibrary, Card>
        {
            private readonly LibraryState _state;

            public SetAsideActionForLibrary(ITurnScope turnScope, LibraryState state, Card action)
                : base(turnScope, action)
            {
                Description = "Set " + action.Name + " aside";
                _state = state;
            }

            public override void Execute()
            {
                _state.SetAsideTheDrawnCards(TurnScope);
            }
        }

        public class TakeActionCardIntoHandForLibrary : GameEventResponse<ActionDrawnForLibrary, Card>
        {
            private readonly LibraryState _state;

            public TakeActionCardIntoHandForLibrary(ITurnScope turnScope, LibraryState state, Card item)
                : base(turnScope, item)
            {
                Description = "Take " + item.Name + " into hand";
                _state = state;
            }

            public override void Execute()
            {
                
            }
        }

        public class LibraryState
        {
            public LibraryState()
            {
                DrawnCards = new CardSet();
                SetAsideCards = new CardSet();
            }
            public CardSet DrawnCards { get; private set; }
            public CardSet SetAsideCards { get; private set; }

            public void SetAsideTheDrawnCards(IActionScope scope)
            {
                var action = DrawnCards.First();
                DrawnCards.Remove(action);
                SetAsideCards.Add(action, scope);
            }
        }
    }
}