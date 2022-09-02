using System.Collections;
using NumbersInWords.Lexing.impl;
using NumbersInWords.Token;

namespace NumbersInWordsTests.Lexing;

public class NumbersKataLexerTestsValidInputTests
{
    private static IEnumerable VocabularyTestCases()
    {
        yield return new object[] { "", new List<KataToken>() };
        yield return new object[] { "0", new List<KataToken> { new DigitKataToken(0) } };
        yield return new object[] { "1", new List<KataToken> { new DigitKataToken(1) } };
        yield return new object[] { "2", new List<KataToken> { new DigitKataToken(2) } };
        yield return new object[] { "3", new List<KataToken> { new DigitKataToken(3) } };
        yield return new object[] { "4", new List<KataToken> { new DigitKataToken(4) } };
        yield return new object[] { "5", new List<KataToken> { new DigitKataToken(5) } };
        yield return new object[] { "6", new List<KataToken> { new DigitKataToken(6) } };
        yield return new object[] { "7", new List<KataToken> { new DigitKataToken(7) } };
        yield return new object[] { "8", new List<KataToken> { new DigitKataToken(8) } };
        yield return new object[] { "9", new List<KataToken> { new DigitKataToken(9) } };
        
        yield return new object[] { "$", new List<KataToken> { new KataToken(TokenType.Currency, "$") } };
        yield return new object[] { "€", new List<KataToken> { new KataToken(TokenType.Currency, "€") } };
        
        yield return new object[] { ",", new List<KataToken> { new KataToken(TokenType.Separator) } };
        yield return new object[] { ".", new List<KataToken> { new KataToken(TokenType.Separator) } };
    }
    
    [Test]
    [TestCaseSource(nameof(VocabularyTestCases))]
    public void NumbersKataLexer_VocabularyTest(string input, List<KataToken> validOutput)
    {
        var lexer = new NumbersKataLexer();
        var res = lexer.Read(input, out var tokenList);
        
        Assert.IsTrue(res);
        Assert.That(tokenList.Count, Is.EqualTo(validOutput.Count));

        if (validOutput.Count == 0)
            return;

        Assert.That(tokenList[0].GetType(), Is.EqualTo(validOutput[0].GetType()));
        if (validOutput[0] is DigitKataToken)
        {
            var digitValidOutput = (DigitKataToken) validOutput[0];
            var digitResultOutput = (DigitKataToken) validOutput[0];
            Assert.That(digitResultOutput.AdditionalData, Is.EqualTo(digitValidOutput.AdditionalData));
        }
        
        Assert.That(tokenList[0].Type, Is.EqualTo(validOutput[0].Type));
    }
    
    private static IEnumerable NumbersStringToValidOutputTestCases()
    {
        yield return new object[]
        {
            "",
            new List<KataToken>()
        };
        yield return new object[]
        {
            "5",
            new List<KataToken> { new DigitKataToken(5) }
        };
        yield return new object[]
        {
            "987654321987",
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
            }
        };
        yield return new object[]
        {
            "987,54€",
            new List<KataToken>
            {
                new DigitKataToken(9),
                new DigitKataToken(8),
                new DigitKataToken(7),
                new KataToken(TokenType.Separator),
                new DigitKataToken(5),
                new DigitKataToken(4),
                new KataToken(TokenType.Currency, "€")
            }
        };
        yield return new object[]
        {
            "1563.34$",
            new List<KataToken>
            {
                new DigitKataToken(1),
                new DigitKataToken(5),
                new DigitKataToken(6),
                new DigitKataToken(3),
                new KataToken(TokenType.Separator),
                new DigitKataToken(3),
                new DigitKataToken(4),
                new KataToken(TokenType.Currency, "$"),
            }
        };
        yield return new object[]
        {
            ".34€",
            new List<KataToken>
            {
                new KataToken(TokenType.Separator),
                new DigitKataToken(3),
                new DigitKataToken(4),
                new KataToken(TokenType.Currency, "€"),
            }
        };
        yield return new object[]
        {
            ".3",
            new List<KataToken>
            {
                new KataToken(TokenType.Separator),
                new DigitKataToken(3)
            }
        };
        yield return new object[]
        {
            "1.456,45€",
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
                new KataToken(TokenType.Currency, "€"),
            }
        };
        yield return new object[]
        {
            "1 457€",
            new List<KataToken> 
            {
                new DigitKataToken(1),
                new DigitKataToken(4),
                new DigitKataToken(5),
                new DigitKataToken(7),
                new KataToken(TokenType.Currency, "€"),
            }
        };
        yield return new object[]
        {
            ".,5€68€",
            new List<KataToken> 
            {
                new KataToken(TokenType.Separator),
                new KataToken(TokenType.Separator),
                new DigitKataToken(5),
                new KataToken(TokenType.Currency, "€"),
                new DigitKataToken(6),
                new DigitKataToken(8),
                new KataToken(TokenType.Currency, "€")
            }
        };
        yield return new object[]
        {
            "5          8      \t€",
            new List<KataToken> 
            {
                new DigitKataToken(5),
                new DigitKataToken(8),
                new KataToken(TokenType.Currency, "€")
            }
        };
    }

    [Test]
    [TestCaseSource(nameof(NumbersStringToValidOutputTestCases))]
    public void NumbersKataLexer_ReadValidStringTest(string input, List<KataToken> validOutput)
    {
        var lexer = new NumbersKataLexer();
        var res = lexer.Read(input, out var tokenList);
        
        Assert.IsTrue(res);
        Assert.That(tokenList.Count, Is.EqualTo(validOutput.Count));

        for (int i = 0; i < validOutput.Count; i++)
        {
            Assert.That(tokenList[i].GetType(), Is.EqualTo(validOutput[i].GetType()));
            
            var digitValidOutput = validOutput[i];
            var digitResultOutput = tokenList[i];
            Assert.That(digitResultOutput.AdditionalData, Is.EqualTo(digitValidOutput.AdditionalData));
            Assert.That(tokenList[i].Type, Is.EqualTo(validOutput[i].Type));
        }
    }
}