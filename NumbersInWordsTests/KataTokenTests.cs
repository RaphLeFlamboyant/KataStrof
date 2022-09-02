using NumbersInWords.Token;

namespace NumbersInWordsTests;

public class KataTokenTests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void DigitKataToken_HaveDefaultType_Digit()
    {
        var digitToken = new DigitKataToken();
        Assert.Equals(digitToken.Type, TokenType.Digit);
    }
}