using System;
using System.Collections.Generic;
using System.Linq;
using Dominion.Ai.Nodes.Terminals;
using StructureMap;

namespace Dominion.Ai.ConstantValueProviders
{
    public class ValueProviderRegistry : IValueProviderRegistry
    {
        private readonly Lazy<IList<IInitialValueProvider>> _providers;

        public ValueProviderRegistry(IContainer container)
        {
            _providers = new Lazy<IList<IInitialValueProvider>>(() =>
                new List<IInitialValueProvider>(container.GetAllInstances<IInitialValueProvider>()));
        }
        public IInitialValueProvider GetProviderFor(Constant constant)
        {
            return _providers.Value.FirstOrDefault(p => p.ValueType == constant.ValueType);
        }

        public bool HasProviderFor(Type value)
        {
            return _providers.Value.Any(p => p.ValueType == value);
        }

        public IEnumerable<IInitialValueProvider> AllProviders { get { return _providers.Value; } }
    }
}