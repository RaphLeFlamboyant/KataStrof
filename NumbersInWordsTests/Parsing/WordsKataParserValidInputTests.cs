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
            new DigitKataToken(7),
            new KataToken(TokenType.HundredWord),
            new KataToken(TokenType.Separator),
            new DigitKataToken(4),
            new KataToken(TokenType.TenWord),
            new DigitKataToken(5),
            new KataToken(TokenType.Currency, "$")
        };
        yield return new List<KataToken> {
            new DigitKataToken(0),
            new KataToken(TokenType.Currency, "$"),
            new KataToken(TokenType.Separator),
            new DigitKataToken(5),
            new KataToken(TokenType.TenWord),
            new DigitKataToken(4),
            new KataToken(TokenType.CentsWord)
        };
        yield return new List<KataToken> {
            new DigitKataToken(7),
            new KataToken(TokenType.Currency, "$"),
            new KataToken(TokenType.Separator),
            new DigitKataToken(5),
            new KataToken(TokenType.TenWord),
            new DigitKataToken(4),
            new KataToken(TokenType.CentsWord)
        };
        // Big word
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
        // Number with cents
        yield return new List<KataToken> {
            new DigitKataToken(9),
            new KataToken(TokenType.HundredWord),
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
    [TestCaseSource(nameof(GenerateValidInputTestCases))]
    public void WordsKataParser_ValidInputTest(List<KataToken> validInput)
    {
        var parser = new WordsKataParser();
        Assert.IsTrue(parser.Parse(validInput));
    }
}