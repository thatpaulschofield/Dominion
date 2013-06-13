using System;
using System.Linq;
using Dominion.Cards;
using Dominion.Cards.BasicSet.Actions;
using Dominion.GameEvents;

namespace Dominion
{
    public class Deck : CardSet, IHandleExternalEvents
    {
        public Deck(params CardSet[] cardSets)
        {
            cardSets.ToList().ForEach(InnerList.AddRange);
        }

        public bool CanDraw
        {
            get { return this.ToList().Any(); }
        }

        public Card Draw(IActionScope turnScope)
        {
            if (!InnerList.Any())
            {
                turnScope.Player.Handle(new DeckDepletedEvent(turnScope), turnScope);
            }
            if (InnerList.Any())
            {
                var drawn = InnerList[0];
                InnerList.RemoveAt(0);
                return drawn;
            }
            return null;
        }

        public Deck Shuffle()
        {
            if (!InnerList.Any())
                return this;

            var shuffled = this;
            for (int i = 0; i < 100; i++)
            {
                int posA = new Random((int)DateTime.Now.Ticks).Next(shuffled.InnerList.Count);
                int posB = new Random((int)DateTime.Now.Ticks).Next(shuffled.InnerList.Count);
                Card temp = shuffled.InnerList[posA];
                shuffled.InnerList[posA] = shuffled.InnerList[posB];
                shuffled.InnerList[posB] = temp;
            }
            return shuffled;
        }

        public CardSet Draw(int cardCount, IActionScope turnScope)
        {
            var cardSet = new CardSet();
            cardCount.Times(() => cardSet.Add(Draw(turnScope), turnScope));
            return cardSet;
        }

        public void Handle(IGameMessage @event, IReactionScope scope)
        {
            this.OfType<IHandleExternalEvents>().ForEach(c => c.Handle(@event, scope));
        }

        public void PlaceCardsOnTop(CardSet cards)
        {
            InnerList.InsertRange(0, cards);
        }
    }
}