using System.Collections;
using NumbersInWords.Parsing.impl;
using NumbersInWords.Token;

namespace NumbersInWordsTests.Parsing;

public class WordsKataParserValidInputTests
{
    private static IEnumerable GenerateValidInputTestCases()
    {
        yield return new List<KataToken>();
        yield return new List<KataToken> {
            new DigitKataToken { Value = 7 },
            new KataToken(TokenType.HundredWord),
            new KataToken(TokenType.Separator),
            new DigitKataToken { Value = 4 },
            new KataToken(TokenType.TenWord),
            new DigitKataToken { Value = 5 },
            new KataToken(TokenType.Currency)
        };
        // Big word
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
        // Hundred words implicit & number with cents
        yield return new List<KataToken> {
            new DigitKataToken { Value = 9 },
            new DigitKataToken { Value = 8 },
            new KataToken(TokenType.TenWord),
            new DigitKataToken { Value = 7 },
            new KataToken(TokenType.Currency),
            new KataToken(TokenType.Separator),
            new DigitKataToken { Value = 5 },
            new DigitKataToken { Value = 4 },
            new KataToken(TokenType.CentsWord)
        };
        yield return new List<KataToken> {
            new DigitKataToken { Value = 7 },
            new KataToken(TokenType.Separator),
            new DigitKataToken { Value = 5 },
            new DigitKataToken { Value = 4 },
            new KataToken(TokenType.CentsWord)
        };
    }
    
    [Test]
    [TestCaseSource(nameof(GenerateValidInputTestCases))]
    public void WordsKataParser_ValidInputTest(List<KataToken> validInput)
    {
        var parser = new WordsKataParser();
        Assert.IsTrue(parser.Parse(validInput));
    }
}