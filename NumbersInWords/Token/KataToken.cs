namespace NumbersInWords.Token;

public class KataToken
{
    public TokenType Type { get; }

    public KataToken(TokenType type)
    {
        Type = type;
    }
}