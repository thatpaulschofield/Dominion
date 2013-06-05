using System;
using Dominion.Ai.Nodes.Terminals;

namespace Dominion.Ai.ConstantValueProviders
{
    public interface IInitialValueProvider
    {
        void ProvideValue(Constant constant);
        Type ValueType { get; }
        object ProduceValue();
    }

    public interface IInitialValueProvider<out T> : IInitialValueProvider
    {
        Func<T> ProvideValueInitializer { get; }
        void ProvideValue(Action<T> setter);
    }

    public class InitialValueProvider<T> : IInitialValueProvider<T>
    {
        public virtual Func<T> ProvideValueInitializer { get; set; }

        public void ProvideValue(Constant constant)
        {
            constant.SetValueAccessor(() => ProvideValueInitializer());
        }

        public void ProvideValue(Action<T> setter)
        {
            setter(ProvideValueInitializer());
        }

        public Type ValueType { get { return typeof (T); } }
        public object ProduceValue()
        {
            return ProvideValueInitializer();
        }
    }
}