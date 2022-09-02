using System.Collections;
using NumbersInWords.Parsing.impl;
using NumbersInWords.Token;

namespace NumbersInWordsTests.Parsing;

public class NumbersKataParserInvalidInputTests
{
    private static IEnumerable GenerateInvalidInputTestCases()
    {
        yield return new List<KataToken> { new KataToken(TokenType.Separator) };
        yield return new List<KataToken> { new DigitKataToken(5) };
        yield return new List<KataToken> { new KataToken(TokenType.Currency, "$") };
        yield return new List<KataToken> { 
            new KataToken(TokenType.Separator),
            new KataToken(TokenType.Separator),
            new DigitKataToken(5),
            new KataToken(TokenType.Currency, "$"),
            new DigitKataToken(6),
            new DigitKataToken(8),
            new KataToken(TokenType.Currency, "$")
        };
        yield return new List<KataToken> { 
            new KataToken(TokenType.Separator),
            new DigitKataToken(5),
            new DigitKataToken(5),
            new DigitKataToken(5),
            new KataToken(TokenType.Currency, "$"),
            new DigitKataToken(6),
            new DigitKataToken(8),
            new KataToken(TokenType.Currency, "$")
        };
        yield return new List<KataToken> {
            new KataToken(TokenType.Separator),
            new DigitKataToken(5),
            new DigitKataToken(4),
            new KataToken(TokenType.Currency, "$")
        };
        yield return new List<KataToken> {
            new KataToken(TokenType.Separator),
            new DigitKataToken(5)
        };
        yield return new List<KataToken> { 
            new DigitKataToken(9),
            new KataToken(TokenType.HundredWord),
            new DigitKataToken(8),
            new KataToken(TokenType.TenWord),
            new DigitKataToken(7),
            new KataToken(TokenType.BillionWord),
            new DigitKataToken(6),
            new KataToken(TokenType.HundredWord),
            new DigitKataToken(5),
            new KataToken(TokenType.TenWord),
            new DigitKataToken(4),
            new KataToken(TokenType.MillionWord),
            new DigitKataToken(3),
            new KataToken(TokenType.HundredWord),
            new DigitKataToken(2),
            new KataToken(TokenType.TenWord),
            new DigitKataToken(1),
            new KataToken(TokenType.ThousandWord),
            new KataToken(TokenType.Separator),
            new DigitKataToken(9),
            new KataToken(TokenType.HundredWord),
            new DigitKataToken(8),
            new KataToken(TokenType.TenWord),
            new DigitKataToken(7),
            new KataToken(TokenType.Currency, "$")
        };
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
            new DigitKataToken(7)
        };
    }

    [Test]
    [TestCaseSource(nameof(GenerateInvalidInputTestCases))]
    public void NumbersKataParser_InvalidInputTest(List<KataToken> invalidInput)
    {
        var parser = new NumbersKataParser();
        Assert.IsFalse(parser.Parse(invalidInput));
    }
}