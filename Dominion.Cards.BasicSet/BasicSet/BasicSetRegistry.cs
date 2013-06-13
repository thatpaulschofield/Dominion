using Dominion.Cards.BasicSet.Actions;
using Dominion.Cards.BasicSet.Actions.MineAction;
using Dominion.Cards.BasicSet.Actions.Remodel;
using Dominion.Cards.BasicSet.Actions.Saga;
using Dominion.Cards.BasicSet.BasicSet.VictoryCards;
using Dominion.Cards.BasicSet.Treasures;
using Dominion.Cards.BasicSet.VictoryCards;
using StructureMap.Configuration.DSL;

namespace Dominion.Cards.BasicSet.BasicSet
{
    public class BasicSetRegistry : Registry    
    {
        public BasicSetRegistry()
        {
            For<CardType>().Use(Treasure.Copper);
            For<CardType>().Use(Treasure.Silver);
            For<CardType>().Use(Treasure.Gold);
            For<CardType>().Use(Victory.Estate);
            For<CardType>().Use(Victory.Duchy);
            For<CardType>().Use(Victory.Province);

            For<CardType>().Use<CardType<Adventurer>>();
            For<CardType>().Use<CardType<Bureaucrat>>();
            For<CardType>().Use<CardType<Cellar>>();
            For<CardType>().Use<CardType<Chancellor>>();
            For<CardType>().Use<CardType<Chapel>>();
            For<CardType>().Use<CardType<CouncilRoom>>();
            For<CardType>().Use<CardType<Feast>>();
            For<CardType>().Use<CardType<Festival>>();
            For<CardType>().Use<CardType<Gardens>>();
            For<CardType>().Use<CardType<Laboratory>>();
            For<CardType>().Use<CardType<Library>>();
            For<CardType>().Use<CardType<Market>>();
            For<CardType>().Use<CardType<Militia>>();
            For<CardType>().Use<CardType<Mine>>();
            For<CardType>().Use<CardType<Moat>>();
            For<CardType>().Use<CardType<Moneylender>>();
            For<CardType>().Use<CardType<Remodel>>();
            For<CardType>().Use<CardType<Smithy>>();
            For<CardType>().Use<CardType<Spy>>();
            For<CardType>().Use<CardType<ThroneRoom>>();
            For<CardType>().Use<CardType<Thief>>();
            For<CardType>().Use<CardType<Village>>();
            For<CardType>().Use<CardType<Woodcutter>>();
            For<CardType>().Use<CardType<Workshop>>();

            For<IStartedBy<Mine.MinePlayedMessage>>().Use<MineSaga>();
            For<IStartedBy<RemodelPlayedMessage>>().Use<RemodelSaga>();

            For<EndGameCondition>().Use<ProvinceStackDepletedEndGameCondition>();

            For<DeckBuilder>().Singleton().EnrichAllWith(x => x.WithSets(7.Coppers(), 3.Estates()));
            For<ISupplyBuilder>().Use<SupplyBuilder>();

        }
    }
}
