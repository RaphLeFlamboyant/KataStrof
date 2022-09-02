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
        var digitToken = new DigitKataToken(5);
        Assert.That(TokenType.Digit, Is.EqualTo(digitToken.Type));
        Assert.That(digitToken.AdditionalData, Is.EqualTo("5"));
    }
}