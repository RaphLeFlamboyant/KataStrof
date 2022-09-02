namespace NumbersInWords.Token;

public class DigitKataToken : KataToken
{
    public int Value { get; set; }

    public DigitKataToken() : base(TokenType.Digit)
    {
    }
}