using System;
using Dominion.AI;
using Dominion.Ai.Nodes.Terminals;

namespace Dominion.Ai.TreeBuilding
{
    public interface IInitialValueProvider
    {
        void ProvideValue(Constant constant);
        Type ValueType { get; }
    }

    public interface IInitialValueProvider<out T> : IInitialValueProvider
    {
        Func<T> ProvideValueInitializer { get; }
    }

    public abstract class InitialValueProvider<T> : IInitialValueProvider<T>
    {
        public abstract Func<T> ProvideValueInitializer { get; }

        public void ProvideValue(Constant constant)
        {
            constant.SetValueAccessor(() => ProvideValueInitializer());
        }

        public Type ValueType { get { return typeof (T); } }
    }

    public class IntValueProvider : InitialValueProvider<int>
    {
        public override Func<int> ProvideValueInitializer
        {
            get { return () => new Random((int) DateTime.Now.Ticks).Next(0, 10); }
        }
    }

    public class VoteValueProvider : InitialValueProvider<ResponseVotes>
    {
        public override Func<ResponseVotes> ProvideValueInitializer
        {
            get{
            var votes = new ResponseVotes();
            return () => votes;
            }
        }
    }
}