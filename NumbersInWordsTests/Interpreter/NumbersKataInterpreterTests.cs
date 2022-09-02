using System.Collections;
using NumbersInWords.Interpreter.impl;
using NumbersInWords.Token;

namespace NumbersInWordsTests.Interpreter;

public class NumbersKataInterpreterTests
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
            "1 456.45 $"
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
                new KataToken(TokenType.Currency, "$"),
                new KataToken(TokenType.Separator),
                new DigitKataToken(4),
                new DigitKataToken(5)
            },
            "1 456.45 $"
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
            "987 654 321 987 $"
        };
        yield return new object[]
        {
            new List<KataToken>
            {
                new DigitKataToken(0),
                new KataToken(TokenType.Currency, "$"),
                new KataToken(TokenType.Separator),
                new DigitKataToken(5),
                new KataToken(TokenType.TenWord)
            },
            "0.50 $"
        };
        yield return new object[]
        {
            new List<KataToken>
            {
                new DigitKataToken(0),
                new KataToken(TokenType.Separator),
                new DigitKataToken(5),
                new KataToken(TokenType.Currency, "$")
            },
            "0.05 $"
        };
        yield return new object[]
        {
            new List<KataToken>
            {
                new DigitKataToken(7),
                new KataToken(TokenType.HundredWord),
                new KataToken(TokenType.Separator),
                new DigitKataToken(4),
                new KataToken(TokenType.TenWord),
                new DigitKataToken(5),
                new KataToken(TokenType.Currency, "$")
            },
            "700.45 $"
        };
        yield return new object[]
        {
            new List<KataToken>
            {
                new DigitKataToken(7),
                new KataToken(TokenType.HundredWord),
                new KataToken(TokenType.MillionWord),
                new KataToken(TokenType.Separator),
                new DigitKataToken(4),
                new KataToken(TokenType.TenWord),
                new DigitKataToken(5),
                new KataToken(TokenType.Currency, "$")
            },
            "700 000 000.45 $"
        };
        yield return new object[]
        {
            new List<KataToken>
            {
                new DigitKataToken(7),
                new KataToken(TokenType.HundredWord),
                new KataToken(TokenType.MillionWord),
                new DigitKataToken(7),
                new KataToken(TokenType.HundredWord),
                new KataToken(TokenType.BillionWord),
                new KataToken(TokenType.Separator),
                new DigitKataToken(4),
                new KataToken(TokenType.TenWord),
                new DigitKataToken(5),
                new KataToken(TokenType.Currency, "$")
            },
            "700 000 700 000 000 000.45 $"
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
            "987 654 321 987 $"
        };
        yield return new object[]
        {
            new List<KataToken>
            {
                new DigitKataToken(9),
                new DigitKataToken(8),
                new KataToken(TokenType.TenWord),
                new DigitKataToken(7),
                new KataToken(TokenType.Currency, "$"),
                new KataToken(TokenType.Separator),
                new DigitKataToken(5),
                new DigitKataToken(4),
                new KataToken(TokenType.CentsWord)
            },
            "987.54 $"
        };
        yield return new object[]
        {
            new List<KataToken>
            {
                new DigitKataToken(7),
                new KataToken(TokenType.Currency, "$"),
                new KataToken(TokenType.Separator),
                new DigitKataToken(5),
                new DigitKataToken(4),
                new KataToken(TokenType.CentsWord)
            },
            "7.54 $"
        };
        yield return new object[]
        {
            new List<KataToken>
            {
                new DigitKataToken(7),
                new KataToken(TokenType.Currency, "$"),
                new KataToken(TokenType.Separator),
                new DigitKataToken(4),
                new KataToken(TokenType.CentsWord)
            },
            "7.04 $"
        };
    }

    [Test]
    [TestCaseSource(nameof(GenerateInputTestCases))]
    public void NumbersKataInterpreter_ValidOutputTest(List<KataToken> input, string expectedOutput)
    {
        var interpreter = new NumbersKataInterpreter();
        var translation = interpreter.Translate(input);
        
        Assert.That(translation, Is.EqualTo(expectedOutput));
    }
}