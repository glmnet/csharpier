using System.Linq;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace CSharpier
{
    public partial class Printer
    {
        private Doc PrintNullableTypeSyntax(NullableTypeSyntax node)
        {
            return Concat(this.Print(node.ElementType), String("?"));
        }
    }
}