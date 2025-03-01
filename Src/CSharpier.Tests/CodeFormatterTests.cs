using FluentAssertions;
using Microsoft.CodeAnalysis.CSharp;
using NUnit.Framework;

namespace CSharpier.Tests;

[TestFixture]
[Parallelizable(ParallelScope.All)]
internal class CodeFormatterTests
{
    [TestCase(EndOfLine.LF, "\n")]
    [TestCase(EndOfLine.CRLF, "\r\n")]
    public void GetLineEndings_Should_Return_Easy_Cases(EndOfLine endOfLine, string expected)
    {
        var code = "tester\n";
        var result = CodeFormatter.GetLineEnding(
            code,
            new PrinterOptions() { EndOfLine = endOfLine }
        );

        result.Should().Be(expected);
    }

    [TestCase("tester\n", "\n")]
    [TestCase("tester\r\n", "\r\n")]
    [TestCase("\ntester\n", "\n")]
    [TestCase("tester", "\n")]
    public void GetLineEndings_With_Auto_Should_Detect(string code, string expected)
    {
        var result = CodeFormatter.GetLineEnding(code, new PrinterOptions());

        result.Should().Be(expected);
    }

    [Test]
    public void Format_Should_Use_Default_Width()
    {
        var code = "var someVariable = someValue;";
        var result = CodeFormatter.Format(code);

        result.Should().Be("var someVariable = someValue;\n");
    }

    [Test]
    public void Format_Should_Use_Width()
    {
        var code = "var someVariable = someValue;";
        var result = CodeFormatter.Format(code, new CodeFormatterOptions { Width = 10 });

        result.Should().Be("var someVariable =\n    someValue;\n");
    }

    [TestCase("\n")]
    [TestCase("\r\n")]
    public void Format_Should_Get_Line_Endings_With_SyntaxTree(string lineEnding)
    {
        var code = $"public class ClassName {{{lineEnding}}}";
        var syntaxTree = CSharpSyntaxTree.ParseText(code);
        var result = CodeFormatter.Format(syntaxTree);

        result.Should().Be("public class ClassName { }" + lineEnding);
    }
}
