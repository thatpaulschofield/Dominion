using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Dominion.AI;
using Dominion.Cards;
using Dominion.GameEvents;
using StructureMap;

namespace Dominion.Ai
{
    public static class AutomapperConfig
    {
        public static void ConfigureMappings(IContainer container)
        {
            Mapper.CreateMap<Hand, CardSet>();
            Mapper.CreateMap<Supply, CardSet>().ConvertUsing(s => new CardSet(s.Values.Select(v => v.Type.Create())));
            Mapper.CreateMap<Card, Card>().ConvertUsing(card => card.CardType.Create());
            Mapper.CreateMap<CardSet, CardSet>().ConvertUsing(cards => new CardSet(cards));
            Mapper.CreateMap<Hand, Hand>().ConvertUsing(h => new Hand(h));
            Mapper.CreateMap<ITurnScope, AiContextGame>().ConvertUsing(scope => new AiContextGame{ NumberOfPlayers = scope.ReactionScopes.Count() + 1});
            Mapper.CreateMap<CardSet, Hand>().ConvertUsing(cards => new Hand(cards));
            Mapper.CreateMap<IEnumerable<Card>, CardSet>().ConvertUsing(cards => new CardSet(cards));
            Mapper.CreateMap<IGameMessage, AiContext>()
                  .ConstructUsing(x => new AiContext(x))
                  .ForMember(x => x.Game, cfg =>
                      {
                          cfg.MapFrom(m => m.TurnScope);
                          cfg.NullSubstitute(new AiContextGame());
                      })
                  .ForMember(ai => ai.Hand, cfg =>
                      {
                          cfg.NullSubstitute(new CardSet());
                          cfg.MapFrom(m => 
                              new CardSet(m.TurnScope.Player.Hand)
                              );
                      })
                  .ForMember(ai => ai.Supply, cfg =>
                      {
                          cfg.MapFrom(m => m.TurnScope.Supply);
                          cfg.NullSubstitute(new Supply());
                      })
                  .ForMember(ai => ai.CardsInPlay, cfg => cfg.MapFrom(m => m.TurnScope.CardsInPlay))
                  .ForMember(ai => ai.Actions, cfg => cfg.MapFrom(m => m.TurnScope.Actions))
                  .ForMember(ai => ai.Buys, cfg => cfg.MapFrom(m => m.TurnScope.Buys))
                  .ForMember(ai => ai.Coins, cfg => cfg.MapFrom(m => m.TurnScope.Coins));
            Mapper.CreateMap<NullResponse, AiContext>().ConvertUsing(Mapper.Map<IGameMessage,AiContext>);
           
            //.ConstructUsing(x => new AiContext(x))
            //  .ForMember(x => x.Game, cfg =>
            //      {
            //          cfg.NullSubstitute(new AiContextGame());
            //          cfg.MapFrom(m => m);
            //      })
            //  .ForMember(ai => ai.Hand, cfg =>
            //      {
            //          cfg.MapFrom(m =>
            //              new CardSet(m.TurnScope.ActingPlayer.Hand)
            //              );
            //          cfg.NullSubstitute(new CardSet());
            //      })
            //  .ForMember(ai => ai.Supply, cfg =>
            //      {
            //          cfg.MapFrom(m => m.TurnScope.Supply);
            //          cfg.NullSubstitute(new Supply());
            //      })
            //  .ForMember(ai => ai.CardsInPlay, cfg => cfg.MapFrom(m => m.TurnScope.CardsInPlay))
            //  .ForMember(ai => ai.Actions, cfg => cfg.MapFrom(m => m.TurnScope.Actions))
            //  .ForMember(ai => ai.Buys, cfg => cfg.MapFrom(m => m.TurnScope.Buys))
            //  .ForMember(ai => ai.Coins, cfg => cfg.MapFrom(m => m.TurnScope.Coins));
        }
    }

}