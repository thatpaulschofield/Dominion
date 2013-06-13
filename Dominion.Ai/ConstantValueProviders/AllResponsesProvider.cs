using System;
using Dominion.Ai.Nodes;
using StructureMap;

namespace Dominion.Ai.ConstantValueProviders
{
    public class ResponseCriteriaWithItemBuilder : ResponseCriteriaBuilder
    {
        private Type _closedCriteriaType;
        public ResponseCriteriaWithItemBuilder(Type responseType, IContainer container) : base(responseType, container)
        {
            var itemType = responseType.BaseType.GetGenericArguments()[0];
            var inResponseTo = responseType.BaseType.GetGenericArguments()[1];
            var openCriteriaType = typeof(EventResponseWithItemCriteria<,,>);
            _closedCriteriaType = openCriteriaType.GetGenericTypeDefinition().MakeGenericType(inResponseTo, responseType, itemType);
        }

        public override EventResponseCriteria BuildCriteria()
        {
            return _container.GetInstance(_closedCriteriaType) as EventResponseCriteria;
        }

        public override string ToString()
        {
            return "ResponseCriteriaWithItemBuilder: " + _closedCriteriaType.Name;
        }
    }
}