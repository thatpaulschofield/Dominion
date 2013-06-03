using System;
using System.Collections.Generic;
using System.Linq;
using Dominion.AI;

namespace Dominion.Ai.Nodes
{
    public abstract class Node : INode
    {
        public abstract INode this[int i] { get; set; }

        public INode Parent { get; set; }
        public abstract int Arity { get; }
        public abstract Type ReturnType { get; }

        protected Node()
        {
            Children = new List<Node>();
        }

        protected Node(INode parent)
        {
            Parent = parent;
        }

        public virtual void Receive(NodeVisitor visitor)
        {
            visitor.Visit(this);
            Children.ForEach(c => c.Receive(visitor));
        }

        public IEnumerable<Terminal> TerminalNodes()
        {
            return Children.SelectMany(c => TerminalNodes());
        }

        public abstract void Evaluate();

        public IEnumerable<INode> Children { get; set; }
    }

    public interface INode
    {
        void Receive(NodeVisitor visitor);
        IEnumerable<Terminal> TerminalNodes();
        INode Parent { get; set; }
        int Arity { get; }
        Type ReturnType { get; }
    }

    public abstract class Node<T> : INode<T>
    {
        public INode Parent { get; set; }
        public virtual int Arity { get { return 1; } }
        public Type ReturnType { get { return typeof (T); } }

        public abstract T Evaluate(IAiContext context);
        public virtual void Receive(NodeVisitor visitor)
        {
            visitor.Visit(this);
            Children.ForEach(c =>
                {
                    if (c == null)
                        return;
                    c.Receive(visitor);
                });
            
        }

        public IEnumerable<Terminal> TerminalNodes()
        {
            throw new NotImplementedException();
        }


        public virtual IEnumerable<INode> Children { get { return new INode[]{}; } }
    }

    public interface INode<T> : INode
    {
        T Evaluate(IAiContext context);
    }
}
