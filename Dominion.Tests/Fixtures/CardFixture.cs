using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominion.Cards;
using Dominion.Cards.BasicSet;
using Dominion.Cards.BasicSet.Actions;
using Dominion.Cards.BasicSet.Actions.MineAction;
using Dominion.Cards.BasicSet.Actions.Remodel;
using Dominion.Cards.BasicSet.Treasures;
using Dominion.Cards.BasicSet.VictoryCards;
using Should;
using StoryTeller;
using StoryTeller.Engine;

namespace Dominion.Tests.Fixtures
{
    public class GameFixture : Fixture
    {
        
    }

    public class CardFixture : Fixture
    {
        private Card _card;

        [ExposeAsTable("Verifying whether the card is a treasure")]
        [return: AliasAs("IsTreasure")]
        public bool CheckingIsTreasure(string cardName)
        {
            return AllCards().Single(c => c.Name == cardName).IsTreasure;
        }

        [ExposeAsTable("Verifying whether the card is an action")]
        [return: AliasAs("IsAction")]
        public bool CheckingAction(string cardName)
        {
            return AllCards().Single(c => c.Name == cardName).IsAction;
        }

        [ExposeAsTable("Verifying the card's cost")]
        [return: AliasAs("Cost")]
        public int CheckingCardCost(string cardName)
        {
            return AllCards().Single(c => c.Name == cardName).BaseCost;
        }

        public override string Description
        {
            get { return "Card fixture"; }
        }

        public IEnumerable<Card> AllCards()
        {
            yield return Treasure.Copper;
            yield return Treasure.Silver;
            yield return Treasure.Gold;
            yield return Victory.Estate;
            yield return Victory.Duchy;
            yield return Victory.Province;
            yield return BasicCards.Actions.Cellar;
            yield return BasicCards.Actions.Militia;
            yield return BasicCards.Actions.Village;
            yield return BasicCards.Curse;
            yield return new Mine();
            yield return new Smithy();
            yield return new Market();
            yield return new Moat();
            yield return new Remodel();
            yield return new Woodcutter();
            yield return new Workshop();
        }
    }
}
