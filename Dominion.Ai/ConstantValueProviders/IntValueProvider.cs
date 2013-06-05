using System;

namespace Dominion.Ai.ConstantValueProviders
{
    public class IntValueProvider : InitialValueProvider<int>
    {
        private readonly int _value;

        public IntValueProvider()
        {
            _value = new Random((int) DateTime.Now.Ticks).Next(0, 10);
        }

        public override Func<int> ProvideValueInitializer
        {
            get { return () => _value; }
        }
    }
}