namespace Dominion.AI
{
    public class Function<TRETURN> : Node<TRETURN>
    {
    }

    public class Function<TRETURN, TCHILD1> : Function<TRETURN>
    {
        public Node<TCHILD1> Child1 { get; set; }

        public override void Receive(NodeVisitor visitor)
        {
            base.Receive(visitor);
            Child1.Receive(visitor);
        }
    }

    public class Function<TRETURN, TCHILD1, TCHILD2> : Function<TRETURN, TCHILD1>
    {
        public Node<TCHILD2> Child2 { get; set; }

        public override void Receive(NodeVisitor visitor)
        {
            base.Receive(visitor);
            Child2.Receive(visitor);
        }
    }

    public class Function<TRETURN, TCHILD1, TCHILD2, TCHILD3> : Function<TRETURN, TCHILD1, TCHILD2>
    {
        public Node<TCHILD3> Child3 { get; set; }

        public override void Receive(NodeVisitor visitor)
        {
            base.Receive(visitor);
            Child3.Receive(visitor);
        }
    }
}