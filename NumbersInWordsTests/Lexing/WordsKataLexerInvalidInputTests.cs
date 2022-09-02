using NumbersInWords.Lexing.impl;

namespace NumbersInWordsTests.Lexing;

public class WordsKataLexerInvalidInputTests
{
    private static IEnumerable<string> InvalidStringTestCases()
    {
        yield return "745.00 $ ";
        yield return "seven hundrrrrr and fourty five dollars";
        yield return "seven hundred [ and fourty five dollars";
        yield return "sevenhundredandfourtyfivedollars";
    }

    [Test]
    [TestCaseSource(nameof(InvalidStringTestCases))]
    public void WordsKataLexer_ReadInvalidString(string input)
    {
        var lexer = new WordsKataLexer();
        var res = lexer.Read(input, out _);
        
        Assert.IsFalse(res);
    }

    [Test]
    public void WordsKataLexer_InvalidCharactersTest()
    {
        var invalidCharacters = "?:/!§ù%*µ$£¤^¨\"#{([-|_\\)]}=+";
        var lexer = new WordsKataLexer();
        
        foreach (var character in invalidCharacters)
        {
            var res = lexer.Read($"{character}", out _);
            Assert.IsFalse(res);
        }
    }
}