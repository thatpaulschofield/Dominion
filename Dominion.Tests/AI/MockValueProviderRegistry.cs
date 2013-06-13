using System;
using System.Collections.Generic;
using Dominion.Ai.ConstantValueProviders;
using Dominion.Ai.Nodes.Terminals;
using Dominion.Ai.TreeBuilding;

namespace Dominion.Tests.AI
{
    public class MockValueProviderRegistry : IValueProviderRegistry
    {
        public IInitialValueProvider GetProviderFor(Constant constant)
        {
            return null;
        }

        public bool HasProviderFor(Type value)
        {
            return true;
        }

        public IEnumerable<IInitialValueProvider> AllProviders { get; private set; }
        public string WhatDoIHave()
        {
            throw new NotImplementedException();
        }
    }
}