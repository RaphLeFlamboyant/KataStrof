using System.Collections;
using NumbersInWords.Parsing.impl;
using NumbersInWords.Token;

namespace NumbersInWordsTests.Parsing;

public class NumbersKataParserValidInputTests
{
    private static IEnumerable GenerateValidInputTestCases()
    {
        yield return new List<KataToken>();
        yield return new List<KataToken> { new DigitKataToken { Value = 5 } };
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
            new DigitKataToken { Value = 9 },
            new DigitKataToken { Value = 8 },
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
    }

    [Test]
    [TestCaseSource(nameof(GenerateValidInputTestCases))]
    public void NumbersKataParser_ValidInputTest(List<KataToken> validInput)
    {
        var parser = new NumbersKataParser();
        Assert.IsTrue(parser.Parse(validInput));
    }
}