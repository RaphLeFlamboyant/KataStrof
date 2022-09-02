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
                new DigitKataToken { Value = 1 },
                new KataToken(TokenType.Separator),
                new DigitKataToken { Value = 4 },
                new DigitKataToken { Value = 5 },
                new DigitKataToken { Value = 6 },
                new KataToken(TokenType.Separator),
                new DigitKataToken { Value = 4 },
                new DigitKataToken { Value = 5 },
                new KataToken(TokenType.Currency),
            },
            "1 456.45 $"
        };
        yield return new object[]
        {
            new List<KataToken>
            {
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
            },
            "987 654 321 987 $"
        };
        yield return new object[]
        {
            new List<KataToken>
            {
                new KataToken(TokenType.Separator),
                new DigitKataToken { Value = 5 }
            },
            "0.5 $"
        };
        yield return new object[]
        {
            new List<KataToken>
            {
                new DigitKataToken { Value = 7 },
                new KataToken(TokenType.HundredWord),
                new KataToken(TokenType.Separator),
                new DigitKataToken { Value = 4 },
                new KataToken(TokenType.TenWord),
                new DigitKataToken { Value = 5 },
                new KataToken(TokenType.Currency)
            },
            "700.45 $"
        };
        yield return new object[]
        {
            new List<KataToken>
            {
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
            },
            "987 654 321 987 $"
        };
        yield return new object[]
        {
            new List<KataToken>
            {
                new DigitKataToken { Value = 9 },
                new DigitKataToken { Value = 8 },
                new KataToken(TokenType.TenWord),
                new DigitKataToken { Value = 7 },
                new KataToken(TokenType.Currency),
                new KataToken(TokenType.Separator),
                new DigitKataToken { Value = 5 },
                new DigitKataToken { Value = 4 },
                new KataToken(TokenType.CentsWord)
            },
            "987.54 $"
        };
        yield return new object[]
        {
            new List<KataToken>
            {
                new DigitKataToken { Value = 7 },
                new KataToken(TokenType.Separator),
                new DigitKataToken { Value = 5 },
                new DigitKataToken { Value = 4 },
                new KataToken(TokenType.CentsWord)
            },
            "7.54 $"
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