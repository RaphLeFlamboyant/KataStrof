namespace NumbersInWords.Token;

public class KataToken
{
    public string AdditionalData { get; set; }
    
    public TokenType Type { get; }

    public KataToken(TokenType type)
    {
        Type = type;
    }

    public KataToken(TokenType type, string additionalData)
        : this(type)
    {
        AdditionalData = additionalData;
    }
}