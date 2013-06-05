using System;
using System.Linq;
using Dominion.GameEvents;
using StructureMap;

namespace Dominion.Ai.ConstantValueProviders
{
    public class AllResponsesProvider : InitialValueProvider<GameEventResponse>
    {
        private readonly IContainer _container;

        public AllResponsesProvider(IContainer container)
        {
            _container = container;

            ProvideValueInitializer = SelectRandomResponse;
        }

        private GameEventResponse SelectRandomResponse()
        {
            var allResponses = _container.GetAllInstances<GameEventResponse>();
            if (allResponses.Any())
            {
                var index = new Random((int)DateTime.Now.Ticks).Next(0, allResponses.Count - 1);
                return allResponses[index];
            }
            return null;
        }
    }
}