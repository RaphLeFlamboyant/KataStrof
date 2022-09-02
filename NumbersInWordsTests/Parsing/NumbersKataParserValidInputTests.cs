using System.Collections;
using NumbersInWords.Parsing.impl;
using NumbersInWords.Token;

namespace NumbersInWordsTests.Parsing;

public class NumbersKataParserValidInputTests
{
    private static IEnumerable GenerateValidInputTestCases()
    {
        yield return new List<KataToken>();
        yield return new List<KataToken> {
            new DigitKataToken(9),
            new DigitKataToken(8),
            new DigitKataToken(7),
            new DigitKataToken(6),
            new DigitKataToken(5),
            new DigitKataToken(4),
            new DigitKataToken(3),
            new DigitKataToken(2),
            new DigitKataToken(1),
            new DigitKataToken(9),
            new DigitKataToken(8),
            new DigitKataToken(7),
            new KataToken(TokenType.Currency, "$")
        };
        yield return new List<KataToken> {
            new DigitKataToken(9),
            new DigitKataToken(8),
            new DigitKataToken(7),
            new KataToken(TokenType.Separator),
            new DigitKataToken(5),
            new DigitKataToken(4),
            new KataToken(TokenType.Currency, "$")
        };
        yield return new List<KataToken> {
            new DigitKataToken(0),
            new KataToken(TokenType.Separator),
            new DigitKataToken(5),
            new DigitKataToken(4),
            new KataToken(TokenType.Currency, "$")
        };
        yield return new List<KataToken> {
            new DigitKataToken(0),
            new KataToken(TokenType.Separator),
            new DigitKataToken(5),
            new KataToken(TokenType.Currency, "$")
        };
        yield return new List<KataToken> {
            new DigitKataToken(5),
            new DigitKataToken(6),
            new DigitKataToken(1),
            new KataToken(TokenType.Separator),
            new DigitKataToken(4),
            new DigitKataToken(5),
            new DigitKataToken(6),
            new KataToken(TokenType.Separator),
            new DigitKataToken(4),
            new DigitKataToken(5),
            new KataToken(TokenType.Currency, "$")
        };
        yield return new List<KataToken> {
            new DigitKataToken(1),
            new DigitKataToken(4),
            new DigitKataToken(5),
            new DigitKataToken(7),
            new KataToken(TokenType.Currency, "$")
        };
    }

    [Test]
    [TestCaseSource(nameof(GenerateValidInputTestCases))]
    public void NumbersKataParser_ValidInputTest(List<KataToken> validInput)
    {
        var parser = new NumbersKataParser();
        Assert.IsTrue(parser.Parse(validInput));
    }
}