using System;
using Dominion.AI;

namespace Dominion.Ai.Nodes.Terminals
{
    public interface Constant
    {
        void SetValueAccessor(Func<object> accessor);
        Type ValueType { get; }
    }
    
    public class Constant<T> : Terminal<T>, Constant
    {
        public Constant()
        {
            ValueAccessor = () => default(T);
        } 

        public Constant(T value)
        {
            ValueAccessor = () => value;
        }

        public override T Evaluate(IAiContext context)
        {
            return Value;
        }

        public T Value { get
        {
            try
            {
                return ValueAccessor();

            }
            catch (Exception)
            {
                
                throw;
            }
        } }

        public Func<T> ValueAccessor { get; set; }

        public override string ToString()
        {
            if (Value == null)
                return typeof(T).Name + ": [Unassigned]";

            return typeof(T).Name + ": " + Value.ToString();
        }

        public void SetValueAccessor(Func<object> accessor)
        {
            ValueAccessor = (() => (T) accessor());
        }

        public Type ValueType { get { return typeof (T); } }
    }
}
