using System.Linq;

namespace Dominion.Cards.BasicSet.Actions
{
    public class Adventurer : TypedCard<Adventurer>
    {
        public Adventurer() : base(cost: 6, isAction: true, isAttack: false, isTreasure: false, name: "Adventurer")
        {
        }

        public override void PlayAsAction(ITurnScope turnScope)
        {
            CardSet drawnTreasures = new CardSet();
            CardSet revealedCards = new CardSet();
            do
            {
                var card = turnScope.RevealCardFromTopOfDeck();
                if (card.IsTreasure)
                    drawnTreasures.Add(card, turnScope);
                else
                    revealedCards.Add(card, turnScope);
            } while (drawnTreasures.Count() < 2);

            turnScope.PutCardsIntoHand(drawnTreasures);
            turnScope.PutCardsIntoDiscardPile(revealedCards);
        }
    }
}