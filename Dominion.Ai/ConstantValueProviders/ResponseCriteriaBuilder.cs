using System;
using Dominion.Ai.Nodes;
using StructureMap;

namespace Dominion.Ai.ConstantValueProviders
{
    public class ResponseCriteriaBuilder
    {
        protected readonly Type _responseType;
        protected readonly IContainer _container;
        private Type _closedCriteriaType;

        public ResponseCriteriaBuilder(Type responseType, IContainer container)
        {
            try
            {
                _responseType = responseType;
                _container = container;
                var inResponseTo = responseType.BaseType.GetGenericArguments()[0];
                var openCriteriaType = typeof(EventResponseCriteria<,>);
                _closedCriteriaType = openCriteriaType.GetGenericTypeDefinition().MakeGenericType(inResponseTo, responseType);

            }
            catch (Exception)
            {
                
            }
        }

        public virtual EventResponseCriteria BuildCriteria()
        {
            return _container.GetInstance(_closedCriteriaType) as EventResponseCriteria;
        }

        public override string ToString()
        {
            return "ResponseCriteriaBuilder for " + _closedCriteriaType;
        }
    }
}