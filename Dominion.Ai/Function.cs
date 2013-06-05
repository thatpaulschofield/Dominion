using System;
using System.Collections.Generic;
using System.Linq;
using Dominion.Ai.Nodes;
using Dominion.Ai.Nodes.Functions;
using Dominion.GameEvents;

namespace Dominion.AI
{
    public interface Function
    {
        IEnumerable<INode> Children { get; }
        int Arity { get; }
        bool HasUnassignedChildNode { get; }
        INode this[int i] { get; set; }
        int GetIndexOfNextUnassignedChild();
    }

    public abstract class Function<TRETURN> : Node<TRETURN>, Function
    {
        public bool HasUnassignedChildNode { get { return _children.Any(n => n != null && n.IsNullNode); } }

        public INode this[int i]
        {
            get { return _children[i]; }
            set
            {
                if (value == null)
                    throw new ArgumentNullException();
                if (_children[0] == null)
                    throw new Exception("Child not was not pre-populated");
                if (_children[i].GetType().BaseType.GenericTypeArgument() != value.GetType().BaseType.GenericTypeArgument())
                    throw new ArgumentException(String.Format("Cannot replace a node of type [{0}] with a node of type [{1}]>", 
                        _children[i], value));
                _children[i] = value;
            }
        }

        public int GetIndexOfNextUnassignedChild()
        {
            return 
                HasUnassignedChildNode
                ? _children.IndexOf(_children.First(n => n != null && n.IsNullNode))
                : -1;
        }

        public IEnumerable<INode> Children
        {
            get { return _children; }
        }


        public override int Arity
        {
            get { return _children.Count; }
        }
    }

    public abstract class Function<TRETURN, TCHILD1> : Function<TRETURN>
    {
        protected Function()
        {
            _children.Add(new NullNode<TCHILD1>());
        }



        public Node<TCHILD1> Child1 { get { return _children[0] as Node<TCHILD1>; } set { _children[0] = value; } }
        public override int Arity { get { return 1; }}
    }

    public abstract class Function<TRETURN, TCHILD1, TCHILD2> : Function<TRETURN, TCHILD1>
    {
        protected Function()
        {
            _children.Add(new NullNode<TCHILD2>());
        }

        public Node<TCHILD2> Child2 { get { return _children[1] as Node<TCHILD2>; } set { _children[1] = value; } }
        public override int Arity { get { return 2; } }

    }

    public abstract class Function<TRETURN, TCHILD1, TCHILD2, TCHILD3> : Function<TRETURN, TCHILD1, TCHILD2>
    {
        protected Function()
        {
            _children.Add(new NullNode<TCHILD3>());
        }

        public Node<TCHILD3> Child3 { get { return _children[2] as Node<TCHILD3>; } set { _children[2] = value; } }
        public override int Arity { get { return 3; } }

    }

    public abstract class NullNode : Node
    {
        public override INode this[int i]
        {
            get { return null; }
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

        public override bool IsNullNode { get { return true; } }

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

        public override bool IsNullNode { get { return true; } }

        public static NullNode<T> CreateFromType(Type t)
        {
            var nullNodeType = typeof (NullNode<>).GetGenericTypeDefinition().MakeGenericType(t);
            return Activator.CreateInstance(nullNodeType) as NullNode<T>;
        }
    }
}