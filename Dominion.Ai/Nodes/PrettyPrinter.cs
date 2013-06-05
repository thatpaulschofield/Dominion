using System;
using System.Text;
using Dominion.AI;

namespace Dominion.Ai.Nodes
{
    public class PrettyPrinter
    {
        StringBuilder builder = new StringBuilder();
        public String Print(INode node)
        {
            Print(node, 0);
            return builder.ToString();
        }

        public void Print(INode node, int indent)
        {
            if (node == null)
                return;

            try
            {
                indent.Times(() => builder.Append("  "));
                builder.AppendLine(node.ToString());
            }
            catch (Exception)
            {
                
                throw;
            }
            var function = node as Function;
            if (function != null)
            {
                indent.Times(() => builder.Append("  "));
                builder.AppendLine("{");
                function.Children.ForEach(c => Print(c, indent + 1));
                indent.Times(() => builder.Append("  "));
                builder.AppendLine("}");
            }
        }
    }
}