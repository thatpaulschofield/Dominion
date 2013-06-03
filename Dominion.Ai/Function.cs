using System;
using System.Collections.Generic;
using Dominion.Ai.Nodes;
using Dominion.GameEvents;

namespace Dominion.AI
{
    public interface Function
    {
        IEnumerable<INode> Children { get; }
        int Arity { get; }
        INode this[int i] { get; set; }
    }

    public abstract class Function<TRETURN> : Node<TRETURN>, Function
    {
        public abstract INode this[int i] { get; set; }
    }

    public abstract class Function<TRETURN, TCHILD1> : Function<TRETURN>
    {
        public override INode this[int i]
        {
            get
            {
                INode node;
                switch (i)
                {
                    case 1:
                        node = Child1 ?? new NullNode<TCHILD1>();
                        break;
                    default:
                        throw new ArgumentException();
                }
                return node;
            }
            set
            {
                switch (i)
                {
                    case 1:
                        Child1 = value as Node<TCHILD1>;
                        break;
                    default:
                        throw new ArgumentException();
                }
            }
        }
        
        public Node<TCHILD1> Child1 { get; set; }
        public override int Arity { get { return 1; }}
        public override IEnumerable<INode> Children
        {
            get { yield return Child1; }
        }
    }

    public abstract class Function<TRETURN, TCHILD1, TCHILD2> : Function<TRETURN, TCHILD1>
    {
        public override INode this[int i]
        {
            get
            {
                INode node;
                switch (i)
                {
                    case 1:
                        node = Child1 ?? new NullNode<TCHILD1>();
                        break;
                    case 2:
                        node = Child2 ?? new NullNode<TCHILD2>();
                        break;
                    default:
                        throw new ArgumentException();
                }
                return node;
            }
            set
            {
                switch (i)
                {
                    case 1:
                        Child1 = value as Node<TCHILD1>;
                        break;
                    case 2:
                        Child2 = value as Node<TCHILD2>;
                        break;
                    default:
                        throw new ArgumentException();
                }
            }
        }
        
        public Node<TCHILD2> Child2 { get; set; }
        public override int Arity { get { return 2; } }

        public override System.Collections.Generic.IEnumerable<INode> Children
        {
            get { yield return Child1;
                yield return Child2;
            }
        }
    }

    public abstract class Function<TRETURN, TCHILD1, TCHILD2, TCHILD3> : Function<TRETURN, TCHILD1, TCHILD2>
    {
        public override INode this[int i]
        {
            get
            {
                INode node;
                switch (i)
                {
                    case 1:
                        node = Child1 ?? new NullNode<TCHILD1>();
                        break;
                    case 2:
                        node = Child2 ?? new NullNode<TCHILD2>();
                        break;
                    case 3:
                        node = Child3 ?? new NullNode<TCHILD3>();
                        break;
                    default:
                        throw new ArgumentException();
                }
                return node;
            }
            set
            {
                switch (i)
                {
                    case 1:
                        Child1 = value as Node<TCHILD1>;
                        break;
                    case 2:
                        Child2 = value as Node<TCHILD2>;
                        break;
                    case 3:
                        Child3 = value as Node<TCHILD3>;
                        break;
                    default:
                        throw new ArgumentException();
                }
            }
        }
        public Node<TCHILD3> Child3 { get; set; }
        public override int Arity { get { return 3; } }

        public override System.Collections.Generic.IEnumerable<INode> Children
        {
            get
            {
                yield return Child1;
                yield return Child2;
                yield return Child3;
            }
        }   
    
    }

    public class NullNode : Node
    {
        public override INode this[int i]
        {
            get { return new NullNode(); }
            set { }
        }

        public override int Arity
        {
            get { return 0; }
        }

        public override Type ReturnType
        {
            get { return typeof(object); }
        }

        public override void Evaluate()
        {
            
        }

        public override string ToString()
        {
            return "NullNode";
        }
    }
    public class NullNode<T> : Node<T>
    {
        public override T Evaluate(IAiContext context)
        {
            return default(T);
        }

        public override string ToString()
        {
            return "NullNode<" + typeof (T).Name + ">";
        }
    }
}