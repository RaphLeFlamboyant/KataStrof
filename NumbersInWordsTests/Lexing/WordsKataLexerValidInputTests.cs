using System.Collections;
using NumbersInWords.Lexing.impl;
using NumbersInWords.Token;

namespace NumbersInWordsTests.Lexing;

public class WordsKataLexerValidInputTests
{
    private static IEnumerable VocabularyTestCases()
    {
        yield return new object[] { "", new List<KataToken>() };
        yield return new object[] { "zero", new List<KataToken> { new DigitKataToken(0) } };
        yield return new object[] { "one", new List<KataToken> { new DigitKataToken(1) } };
        yield return new object[] { "two", new List<KataToken> { new DigitKataToken(2) } };
        yield return new object[] { "three", new List<KataToken> { new DigitKataToken(3) } };
        yield return new object[] { "four", new List<KataToken> { new DigitKataToken(4) } };
        yield return new object[] { "five", new List<KataToken> { new DigitKataToken(5) } };
        yield return new object[] { "six", new List<KataToken> { new DigitKataToken(6) } };
        yield return new object[] { "seven", new List<KataToken> { new DigitKataToken(7) } };
        yield return new object[] { "eight", new List<KataToken> { new DigitKataToken(8) } };
        yield return new object[] { "nine", new List<KataToken> { new DigitKataToken(9) } };
        
        yield return new object[] { "ten", new List<KataToken> { new DigitKataToken(1), new KataToken(TokenType.TenWord) } };
        yield return new object[] { "twenty", new List<KataToken> { new DigitKataToken(2), new KataToken(TokenType.TenWord) } };
        yield return new object[] { "thirty", new List<KataToken> { new DigitKataToken(3), new KataToken(TokenType.TenWord) } };
        yield return new object[] { "forty", new List<KataToken> { new DigitKataToken(4), new KataToken(TokenType.TenWord) } };
        yield return new object[] { "fourty", new List<KataToken> { new DigitKataToken(4), new KataToken(TokenType.TenWord) } };
        yield return new object[] { "fifty", new List<KataToken> { new DigitKataToken(5), new KataToken(TokenType.TenWord) } };
        yield return new object[] { "sixty", new List<KataToken> { new DigitKataToken(6), new KataToken(TokenType.TenWord) } };
        yield return new object[] { "seventy", new List<KataToken> { new DigitKataToken(7), new KataToken(TokenType.TenWord) } };
        yield return new object[] { "eighty", new List<KataToken> { new DigitKataToken(8), new KataToken(TokenType.TenWord) } };
        yield return new object[] { "ninety", new List<KataToken> { new DigitKataToken(9), new KataToken(TokenType.TenWord) } };
        
        yield return new object[] { "eleven", new List<KataToken> { new DigitKataToken(1), new KataToken(TokenType.TenWord), new DigitKataToken(1) } };
        yield return new object[] { "twelve", new List<KataToken> { new DigitKataToken(1), new KataToken(TokenType.TenWord), new DigitKataToken(2) } };
        yield return new object[] { "thirteen", new List<KataToken> { new DigitKataToken(1), new KataToken(TokenType.TenWord), new DigitKataToken(3) } };
        yield return new object[] { "forteen", new List<KataToken> { new DigitKataToken(1), new KataToken(TokenType.TenWord), new DigitKataToken(4) } };
        yield return new object[] { "fourteen", new List<KataToken> { new DigitKataToken(1), new KataToken(TokenType.TenWord), new DigitKataToken(4) } };
        yield return new object[] { "fifteen", new List<KataToken> { new DigitKataToken(1), new KataToken(TokenType.TenWord), new DigitKataToken(5) } };
        yield return new object[] { "sixteen", new List<KataToken> { new DigitKataToken(1), new KataToken(TokenType.TenWord), new DigitKataToken(6) } };
        yield return new object[] { "seventeen", new List<KataToken> { new DigitKataToken(1), new KataToken(TokenType.TenWord), new DigitKataToken(7) } };
        yield return new object[] { "eighteen", new List<KataToken> { new DigitKataToken(1), new KataToken(TokenType.TenWord), new DigitKataToken(8) } };
        yield return new object[] { "nineteen", new List<KataToken> { new DigitKataToken(1), new KataToken(TokenType.TenWord), new DigitKataToken(9) } };
        
        yield return new object[] { "hundred", new List<KataToken> { new KataToken(TokenType.HundredWord) } };
        yield return new object[] { "thousand", new List<KataToken> { new KataToken(TokenType.ThousandWord) } };
        yield return new object[] { "million", new List<KataToken> { new KataToken(TokenType.MillionWord) } };
        yield return new object[] { "billion", new List<KataToken> { new KataToken(TokenType.BillionWord) } };
        
        yield return new object[] { "cents", new List<KataToken> { new KataToken(TokenType.CentsWord) } };
        yield return new object[] { "dollars", new List<KataToken> { new KataToken(TokenType.Currency, "$") } };
        yield return new object[] { "dollar", new List<KataToken> { new KataToken(TokenType.Currency, "$") } };
        yield return new object[] { "euros", new List<KataToken> { new KataToken(TokenType.Currency, "€") } };
        yield return new object[] { "euro", new List<KataToken> { new KataToken(TokenType.Currency, "€") } };
        
        yield return new object[] { "and", new List<KataToken> { new KataToken(TokenType.Separator) } };
        yield return new object[] { ",", new List<KataToken> { new KataToken(TokenType.Separator) } };
        yield return new object[] { "&", new List<KataToken> { new KataToken(TokenType.Separator) } };
        yield return new object[] { ";", new List<KataToken> { new KataToken(TokenType.Separator) } };
        yield return new object[] { ".", new List<KataToken> { new KataToken(TokenType.Separator) } };
    }
    
    [Test]
    [TestCaseSource(nameof(VocabularyTestCases))]
    public void WordsKataLexer_VocabularyTest(string input, List<KataToken> validOutput)
    {
        var lexer = new WordsKataLexer();
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
    
    private static IEnumerable ComplexValidOutputTestCases()
    {
        yield return new object[] { "", new List<KataToken>() };
        yield return new object[]
        {
            "seven hundred and fourty five euros",
            new List<KataToken>
            {
                new DigitKataToken(7),
                new KataToken(TokenType.HundredWord),
                new KataToken(TokenType.Separator),
                new DigitKataToken(4),
                new KataToken(TokenType.TenWord),
                new DigitKataToken(5),
                new KataToken(TokenType.Currency, "€")
            }
        };
        yield return new object[]
        {
            "nine hundred eighty seven billion six hundred fifty four million three hundred twenty one thousand and nine hundred eighty seven dollars",
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
            }
        };
        yield return new object[]
        {
            "nine and              hundred    &    dollars  \t   four",
            new List<KataToken>
            {
                new DigitKataToken(9),
                new KataToken(TokenType.Separator),
                new KataToken(TokenType.HundredWord),
                new KataToken(TokenType.Separator),
                new KataToken(TokenType.Currency, "$"),
                new DigitKataToken(4)
            }
        };
    }

    [Test]
    [TestCaseSource(nameof(ComplexValidOutputTestCases))]
    public void WordsKataLexer_ReadValidStringTest(string input, List<KataToken> validOutput)
    {
        var lexer = new WordsKataLexer();
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