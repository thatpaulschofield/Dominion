using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dominion.Ai.Nodes;
using Dominion.GameEvents;
using StructureMap;

namespace Dominion.Ai.ConstantValueProviders
{
    public class EventResponseCriteriaProvider : InitialValueProvider<EventResponseCriteria>
    {
        private readonly IValueProviderRegistry _valueProviderRegistry;
        private readonly IContainer _container;
        private readonly Random _random;
        private readonly Lazy<IEnumerable<ResponseCriteriaBuilder>> _builders;

        public EventResponseCriteriaProvider(IValueProviderRegistry valueProviderRegistry, IContainer container, Random random)
        {
            _valueProviderRegistry = valueProviderRegistry;
            _container = container;
            _random = random;

            _builders = new Lazy<IEnumerable<ResponseCriteriaBuilder>>(ScanForResponseTypes);
            ScanForResponseTypes();

            ProvideValueInitializer = SelectRandomResponse;
        }

        private IEnumerable<ResponseCriteriaBuilder> ScanForResponseTypes()
        {
            var responseTypes =
                typeof (GameEventResponse).Assembly.GetTypes().Where(t => t.IsDescendedFrom(typeof(GameEventResponse)));

            return ScanForTypesWithItem(responseTypes).Union(ScanForTypesWithoutItem(responseTypes));

        }

        private IEnumerable<ResponseCriteriaBuilder> ScanForTypesWithItem(IEnumerable<Type> responseTypes)
        {
            return
                from type in responseTypes
                where
                    type.HasGenericAncestor() && type.ClosestGenericAncestor() == typeof (GameEventResponseWithItem<,>)
                    && _valueProviderRegistry.HasProviderFor(type.BaseType.GetGenericArguments()[0])
                    && _valueProviderRegistry.HasProviderFor(type.BaseType.GetGenericArguments()[0])
                from provider in _valueProviderRegistry.AllProviders
                select new ResponseCriteriaWithItemBuilder(provider.ValueType, _container);
        }

        private IEnumerable<ResponseCriteriaBuilder> ScanForTypesWithoutItem(IEnumerable<Type> responseTypes)
        {
            return responseTypes
                .Where(type => !type.IsGenericType
                    && type.BaseType != null 
                    && type.BaseType.IsGenericType
                    && !type.IsDescendedFrom(typeof(GameEventResponseWithItem<>)))
                .Where(type => 
                    type.BaseType.GetGenericTypeDefinition().Equals(typeof(GameEventResponse<>)))
                         .Select(type => new ResponseCriteriaBuilder(type, _container));
        }

        private EventResponseCriteria SelectRandomResponse()
        {
            var builders = _builders.Value.ToList();
            if (builders.Any())
            {
                var index = _random.Next(0, builders.Count() - 1);
                var criteriaBuilder = builders.ToList()[index];
                return criteriaBuilder.BuildCriteria();
            }
            return null;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine("EventResponseCriteriaProvider:");
            _builders.Value.ForEach(b => sb.AppendLine(b.ToString()));
            return sb.ToString();
        }
    }
}