using System;
using System.Collections.Generic;
using Dominion.Ai.Nodes.Terminals;

namespace Dominion.Ai.ConstantValueProviders
{
    public interface IValueProviderRegistry
    {
        IInitialValueProvider GetProviderFor(Constant constant);
        bool HasProviderFor(Type value);
        IEnumerable<IInitialValueProvider> AllProviders { get; }
        string WhatDoIHave();
    }
}