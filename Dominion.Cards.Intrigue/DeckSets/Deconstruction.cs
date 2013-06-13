using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominion.Cards.BasicSet.Actions;
using Dominion.Cards.BasicSet.Actions.Remodel;

namespace Dominion.Cards.Intrigue.DeckSets
{
    public class Deconstruction : DeckSet
    {
        public override ISupplyBuilder Build(ISupplyBuilder builder)
        {
            return builder.With(10.Of<Bridge>())
                          .With(10.Of<MiningVillage>())
                          .With(10.Of<Remodel>())
                          .With(10.Of<Saboteur>())
                          .With(10.Of<SecretChamber>())
                          .With(10.Of<Spy>())
                          .With(10.Of<Swindler>())
                          .With(10.Of<Thief>())
                          .With(10.Of<ThroneRoom>())
                          .With(10.Of<Torturer>());
        }
    }
}
