using NumbersInWords.Lexing.impl;

namespace NumbersInWordsTests.Lexing;

public class NumbersKataLexerInvalidInputTests
{
    private static IEnumerable<string> InvalidStringTestCases()
    {
        yield return "seven hundred and fourty five dollars";
        yield return "1536]23€";
    }

    [Test]
    [TestCaseSource(nameof(InvalidStringTestCases))]
    public void NumbersKataLexer_ReadInvalidString(string input)
    {
        var lexer = new NumbersKataLexer();
        var res = lexer.Read(input, out _);
        
        Assert.IsFalse(res);
    }

    [Test]
    public void NumbersKataLexer_InvalidSeparatorsTest()
    {
        var invalidSeparators = ";?:/!§ù%*µ$£¤^¨&\"#{([-|_\\)]}=+";
        var lexer = new NumbersKataLexer();
        
        foreach (var separator in invalidSeparators)
        {
            var res = lexer.Read($"{separator}", out _);
            Assert.IsFalse(res);
        }
    }
}