using NumbersInWords.Token;

namespace NumbersInWords.Parsing.impl;

public class NumbersKataParser : IKataParser
{
    private int _digitCount;
    private int _separatorCount;
    private bool _currencyMet;
    
    public bool Parse(IList<KataToken> tokenList)
    {
        if (tokenList.Count == 0)
            return true;
        if (tokenList.Count == 1)
            return false;

        Init();
        
        for (int i = tokenList.Count - 1; i >= 0; i--)
        {
            if (!CheckValidation(tokenList[i], i == 0))
                return false;
        }

        return tokenList[0].Type == TokenType.Digit;
    }

    private void Init()
    {
        _currencyMet = false;
        _digitCount = 0;
        _separatorCount = 0;
    }

    private bool CheckValidation(KataToken token, bool isLast)
    {
        switch (token.Type)
        {
            case TokenType.Currency:
                _currencyMet = true;
                return _digitCount == 0 && _separatorCount == 0;
            case TokenType.Digit:
                _digitCount++;
                return _currencyMet;
            case TokenType.Separator:
                if ((((_digitCount != 2 && _digitCount != 1) || _separatorCount != 0) && _digitCount != 3) || (_digitCount == 3 && isLast)) 
                    return false;
                _digitCount = 0;
                _separatorCount++;
                return true;
            default:
                return false;
        }
    }
}