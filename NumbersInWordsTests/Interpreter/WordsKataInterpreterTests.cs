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
            new DigitKataToken { Value = 5 },
            "five dollars"
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
            "one thousand four hundred fifty six dollars and forty five cents"
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
            "nine hundred eighty seven billion six hundred fifty four million three hundred twenty one thousand nine hundred eighty seven dollars"
        };
        yield return new object[]
        {
            new List<KataToken>
            {
                new KataToken(TokenType.Separator),
                new DigitKataToken { Value = 5 }
            },
            "fifty cents"
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
            "nine hundred eighty seven billion six hundred fifty four million three hundred twenty one thousand nine hundred eighty seven dollars"
        };
        yield return new object[]
        {
            new List<KataToken>
            {
                new DigitKataToken { Value = 9 },
                new DigitKataToken { Value = 8 },
                new DigitKataToken { Value = 7 },
                new KataToken(TokenType.Separator),
                new DigitKataToken { Value = 5 },
                new DigitKataToken { Value = 4 },
                new KataToken(TokenType.Currency)
            },
            "nine hundred eighty seven dollars and fifty four cents"
        };
        yield return new object[]
        {
            new List<KataToken>
            {
                new KataToken(TokenType.Separator),
                new DigitKataToken { Value = 5 },
                new DigitKataToken { Value = 4 },
                new KataToken(TokenType.Currency)
            },
            "fifty four cents"
        };
        yield return new object[]
        {
            new List<KataToken>
            {
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
            },
            "five hundred sixty one thousand four hundred fifty six dollars and forty five cents"
        };
        yield return new object[]
        {
            new List<KataToken>
            {
                new DigitKataToken { Value = 1 },
                new DigitKataToken { Value = 4 },
                new DigitKataToken { Value = 5 },
                new DigitKataToken { Value = 7 },
                new KataToken(TokenType.Currency)
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