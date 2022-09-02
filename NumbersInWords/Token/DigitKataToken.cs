namespace NumbersInWords.Token;

public class DigitKataToken : KataToken
{
    public DigitKataToken(int value) : base(TokenType.Digit)
    {
        AdditionalData = value.ToString();
    }
}