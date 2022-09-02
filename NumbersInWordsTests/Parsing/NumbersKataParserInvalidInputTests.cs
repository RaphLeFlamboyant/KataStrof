using System.Collections;
using NumbersInWords.Parsing.impl;
using NumbersInWords.Token;

namespace NumbersInWordsTests.Parsing;

public class NumbersKataParserInvalidInputTests
{
    private static IEnumerable GenerateInvalidInputTestCases()
    {
        yield return new List<KataToken> { new KataToken(TokenType.Separator) };
        yield return new List<KataToken> { new KataToken(TokenType.Currency) };
        yield return new List<KataToken> { 
            new KataToken(TokenType.Separator),
            new KataToken(TokenType.Separator),
            new DigitKataToken { Value = 5 },
            new KataToken(TokenType.Currency),
            new DigitKataToken { Value = 6 },
            new DigitKataToken { Value = 8 },
            new KataToken(TokenType.Currency)
        };
        yield return new List<KataToken> { 
            new DigitKataToken { Value = 9 },
            new KataToken(TokenType.HundredWord),
            new DigitKataToken { Value = 8 },
            new KataToken(TokenType.TenWord),
            new DigitKataToken { Value = 7 },
            new KataToken(TokenType.BillionWord),
            new DigitKataToken { Value = 6 },
            new KataToken(TokenType.HundredWord),
            new DigitKataToken { Value = 5 },
            new KataToken(TokenType.TenWord),
            new DigitKataToken { Value = 4 },
            new KataToken(TokenType.MillionWord),
            new DigitKataToken { Value = 3 },
            new KataToken(TokenType.HundredWord),
            new DigitKataToken { Value = 2 },
            new KataToken(TokenType.TenWord),
            new DigitKataToken { Value = 1 },
            new KataToken(TokenType.ThousandWord),
            new KataToken(TokenType.Separator),
            new DigitKataToken { Value = 9 },
            new KataToken(TokenType.HundredWord),
            new DigitKataToken { Value = 8 },
            new KataToken(TokenType.TenWord),
            new DigitKataToken { Value = 7 },
            new KataToken(TokenType.Currency)
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