using System.Collections;
using NumbersInWords.Interpreter.impl;
using NumbersInWords.Token;

namespace NumbersInWordsTests.Interpreter;

public class WordsKataInterpreterTests
{
    private static IEnumerable GenerateInputTestCases()
    {
        yield return new object[]
        {
            new List<KataToken>(),
            ""
        };
        yield return new object[]
        {
            new List<KataToken>
            {
                new DigitKataToken(5),
                new KataToken(TokenType.Currency, "$")
            },
            "five dollars"
        };
        yield return new object[]
        {
            new List<KataToken>
            {
                new DigitKataToken(1),
                new KataToken(TokenType.Separator),
                new DigitKataToken(4),
                new DigitKataToken(5),
                new DigitKataToken(6),
                new KataToken(TokenType.Separator),
                new DigitKataToken(4),
                new DigitKataToken(5),
                new KataToken(TokenType.Currency, "$"),
            },
            "one thousand four hundred fifty six dollars and fourty five cents"
        };
        yield return new object[]
        {
            new List<KataToken>
            {
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
            },
            "nine hundred eighty seven billion six hundred fifty four million three hundred twenty one thousand nine hundred eighty seven dollars"
        };
        yield return new object[]
        {
            new List<KataToken>
            {
                new KataToken(TokenType.Separator),
                new DigitKataToken(5)
            },
            "fifty cents"
        };
        yield return new object[]
        {
            new List<KataToken>
            {
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
            },
            "nine hundred eighty seven billion six hundred fifty four million three hundred twenty one thousand nine hundred eighty seven dollars"
        };
        yield return new object[]
        {
            new List<KataToken>
            {
                new DigitKataToken(9),
                new DigitKataToken(8),
                new DigitKataToken(7),
                new KataToken(TokenType.Separator),
                new DigitKataToken(5),
                new DigitKataToken(4),
                new KataToken(TokenType.Currency, "$")
            },
            "nine hundred eighty seven dollars and fifty four cents"
        };
        yield return new object[]
        {
            new List<KataToken>
            {
                new DigitKataToken(0),
                new KataToken(TokenType.Separator),
                new DigitKataToken(5),
                new DigitKataToken(4),
                new KataToken(TokenType.Currency, "$")
            },
            "zero dollars and fifty four cents"
        };
        yield return new object[]
        {
            new List<KataToken>
            {
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
            },
            "five hundred sixty one thousand four hundred fifty six dollars and fourty five cents"
        };
        yield return new object[]
        {
            new List<KataToken>
            {
                new DigitKataToken(1),
                new DigitKataToken(4),
                new DigitKataToken(5),
                new DigitKataToken(7),
                new KataToken(TokenType.Currency, "$")
            },
            "one thousand four hundred fifty seven dollars"
        };
    }
    
    [Test]
    [TestCaseSource(nameof(GenerateInputTestCases))]
    public void WordsKataInterpreter_ValidOutputTest(List<KataToken> input, string expectedOutput)
    {
        var interpreter = new WordsKataInterpreter();
        var translation = interpreter.Translate(input);
        
        Assert.That(translation, Is.EqualTo(expectedOutput));
    }
}