using System.Collections;
using NumbersInWords.Parsing.impl;
using NumbersInWords.Token;

namespace NumbersInWordsTests.Parsing;

public class WordsKataParserInvalidInputTests
{
    private static IEnumerable GenerateInvalidInputTestCases()
    {
        yield return new List<KataToken> { new KataToken(TokenType.Separator) };
        yield return new List<KataToken> { new KataToken(TokenType.Currency) };
        yield return new List<KataToken> {
            new DigitKataToken { Value = 9 },
            new DigitKataToken { Value = 8 },
            new DigitKataToken { Value = 7 },
            new DigitKataToken { Value = 6 },
            new DigitKataToken { Value = 5 },
            new DigitKataToken { Value = 4 },
            new DigitKataToken { Value = 3 },
            new DigitKataToken { Value = 2 },
            new DigitKataToken { Value = 1 },
            new DigitKataToken { Value = 9 },
            new DigitKataToken { Value = 8 },
            new DigitKataToken { Value = 7 }
        };
        yield return new List<KataToken> {
            new DigitKataToken { Value = 7 },
            new KataToken(TokenType.Separator),
            new DigitKataToken { Value = 5 },
            new DigitKataToken { Value = 4 },
            new KataToken(TokenType.Currency)
        };
        yield return new List<KataToken> {
            new KataToken(TokenType.Separator),
            new DigitKataToken { Value = 5 },
            new DigitKataToken { Value = 4 },
            new KataToken(TokenType.Currency)
        };
        yield return new List<KataToken> {
            new KataToken(TokenType.Separator),
            new DigitKataToken { Value = 5 }
        };
        yield return new List<KataToken> {
            new DigitKataToken { Value = 5 },
            new DigitKataToken { Value = 6 },
            new DigitKataToken { Value = 1 },
            new KataToken(TokenType.Separator),
            new DigitKataToken { Value = 4 },
            new DigitKataToken { Value = 5 },
            new DigitKataToken { Value = 6 },
            new KataToken(TokenType.Separator),
            new DigitKataToken { Value = 4 },
            new DigitKataToken { Value = 5 },
            new KataToken(TokenType.Currency)
        };
        yield return new List<KataToken> {
            new DigitKataToken { Value = 1 },
            new DigitKataToken { Value = 4 },
            new DigitKataToken { Value = 5 },
            new DigitKataToken { Value = 7 },
            new KataToken(TokenType.Currency)
        };
        // Thousand key word cumulation
        yield return new List<KataToken> {
            new DigitKataToken { Value = 9 },
            new KataToken(TokenType.HundredWord),
            new DigitKataToken { Value = 8 },
            new KataToken(TokenType.TenWord),
            new DigitKataToken { Value = 7 },
            new KataToken(TokenType.ThousandWord),
            new DigitKataToken { Value = 6 },
            new KataToken(TokenType.HundredWord),
            new DigitKataToken { Value = 5 },
            new KataToken(TokenType.TenWord),
            new DigitKataToken { Value = 4 },
            new KataToken(TokenType.ThousandWord),
            new DigitKataToken { Value = 9 },
            new KataToken(TokenType.HundredWord),
            new DigitKataToken { Value = 8 },
            new KataToken(TokenType.TenWord),
            new DigitKataToken { Value = 7 },
            new KataToken(TokenType.Currency)
        };
        // Hundred key word cumulation
        yield return new List<KataToken> {
            new KataToken(TokenType.HundredWord),
            new DigitKataToken { Value = 8 },
            new KataToken(TokenType.TenWord),
            new DigitKataToken { Value = 7 },
            new KataToken(TokenType.HundredWord),
            new DigitKataToken { Value = 8 },
            new KataToken(TokenType.TenWord),
            new DigitKataToken { Value = 7 },
            new KataToken(TokenType.Currency)
        };
        yield return new List<KataToken> { new DigitKataToken { Value = 5 } };
        yield return new List<KataToken> { new DigitKataToken { Value = 5 } };
    }
    
    [Test]
    [TestCaseSource(nameof(GenerateInvalidInputTestCases))]
    public void WordsKataParser_InvalidInputTest(List<KataToken> invalidInput)
    {
        var parser = new WordsKataParser();
        Assert.IsFalse(parser.Parse(invalidInput));
    }
}