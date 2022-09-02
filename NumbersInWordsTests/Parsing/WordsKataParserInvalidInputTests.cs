using System.Collections;
using NumbersInWords.Parsing.impl;
using NumbersInWords.Token;

namespace NumbersInWordsTests.Parsing;

public class WordsKataParserInvalidInputTests
{
    private static IEnumerable GenerateInvalidInputTestCases()
    {
        yield return new List<KataToken> { new KataToken(TokenType.Separator) };
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
        yield return new List<KataToken> {
            new DigitKataToken(7),
            new KataToken(TokenType.Currency, "$"),
            new KataToken(TokenType.Currency, "$"),
            new KataToken(TokenType.Separator),
            new DigitKataToken(5),
            new DigitKataToken(4),
            new KataToken(TokenType.CentsWord)
        };
        yield return new List<KataToken> {
            new DigitKataToken(7),
            new KataToken(TokenType.Separator),
            new DigitKataToken(5),
            new DigitKataToken(4),
            new KataToken(TokenType.Currency, "$")
        };
        yield return new List<KataToken> {
            new KataToken(TokenType.Separator),
            new DigitKataToken(5),
            new DigitKataToken(4),
            new KataToken(TokenType.Currency, "$")
        };
        yield return new List<KataToken> {
            new DigitKataToken(5),
            new DigitKataToken(4),
            new KataToken(TokenType.CentsWord)
        };
        yield return new List<KataToken> {
            new DigitKataToken(2),
            new KataToken(TokenType.Currency, "$"),
            new DigitKataToken(3),
            new KataToken(TokenType.HundredWord),
            new DigitKataToken(5),
            new KataToken(TokenType.TenWord),
            new DigitKataToken(4),
            new KataToken(TokenType.CentsWord)
        };
        yield return new List<KataToken> {
            new DigitKataToken(2),
            new KataToken(TokenType.ThousandWord),
            new DigitKataToken(3),
            new KataToken(TokenType.HundredWord),
            new DigitKataToken(5),
            new KataToken(TokenType.TenWord),
            new DigitKataToken(4),
            new KataToken(TokenType.CentsWord)
        };
        yield return new List<KataToken> {
            new KataToken(TokenType.Separator),
            new DigitKataToken(5)
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
        // Thousand key word cumulation
        yield return new List<KataToken> {
            new DigitKataToken(9),
            new KataToken(TokenType.HundredWord),
            new DigitKataToken(8),
            new KataToken(TokenType.TenWord),
            new DigitKataToken(7),
            new KataToken(TokenType.ThousandWord),
            new DigitKataToken(6),
            new KataToken(TokenType.HundredWord),
            new DigitKataToken(5),
            new KataToken(TokenType.TenWord),
            new DigitKataToken(4),
            new KataToken(TokenType.ThousandWord),
            new DigitKataToken(9),
            new KataToken(TokenType.HundredWord),
            new DigitKataToken(8),
            new KataToken(TokenType.TenWord),
            new DigitKataToken(7),
            new KataToken(TokenType.Currency, "$")
        };
        // Hundred key word cumulation
        yield return new List<KataToken> {
            new KataToken(TokenType.HundredWord),
            new DigitKataToken(8),
            new KataToken(TokenType.TenWord),
            new DigitKataToken(7),
            new KataToken(TokenType.HundredWord),
            new DigitKataToken(8),
            new KataToken(TokenType.TenWord),
            new DigitKataToken(7),
            new KataToken(TokenType.Currency, "$")
        };
        yield return new List<KataToken> {
            new DigitKataToken(7),
            new KataToken(TokenType.Currency, "$"),
            new KataToken(TokenType.Separator),
            new DigitKataToken(5),
            new KataToken(TokenType.TenWord),
            new DigitKataToken(4)
        };
        yield return new List<KataToken> {
            new DigitKataToken(5),
            new KataToken(TokenType.TenWord),
            new DigitKataToken(4),
            new KataToken(TokenType.CentsWord)
        };
        yield return new List<KataToken> {
            new DigitKataToken(7),
            new KataToken(TokenType.Separator),
            new DigitKataToken(5),
            new DigitKataToken(4),
            new KataToken(TokenType.CentsWord)
        };
        // implicit hundred word not supported
        yield return new List<KataToken> {
            new DigitKataToken(9),
            new DigitKataToken(8),
            new KataToken(TokenType.TenWord),
            new DigitKataToken(7),
            new KataToken(TokenType.Currency, "$"),
            new KataToken(TokenType.Separator),
            new DigitKataToken(5),
            new KataToken(TokenType.TenWord),
            new DigitKataToken(4),
            new KataToken(TokenType.CentsWord)
        };
    }
    
    [Test]
    [TestCaseSource(nameof(GenerateInvalidInputTestCases))]
    public void WordsKataParser_InvalidInputTest(List<KataToken> invalidInput)
    {
        var parser = new WordsKataParser();
        Assert.IsFalse(parser.Parse(invalidInput));
    }
}