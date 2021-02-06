using System.Linq;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace CSharpier
{
    public partial class Printer
    {
        private Doc PrintRefValueExpressionSyntax(RefValueExpressionSyntax node)
        {
            return Concat(
                node.Keyword.Text,
                "(",
                this.Print(node.Expression),
                ", ",
                this.Print(node.Type),
                ")"
            );
        }
    }
}