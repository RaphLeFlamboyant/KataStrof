using NumbersInWords.Token;

namespace NumbersInWords.Parsing.impl;

public class WordsKataParser : IKataParser
{
    private List<TokenType> _smallMagnitudes = new() { TokenType.TenWord, TokenType.HundredWord };
    private List<TokenType> _bigMagnitudes = new() { TokenType.ThousandWord, TokenType.MillionWord, TokenType.BillionWord };
    
    private bool _centsWordMet;
    private bool _separatorJustMet;
    private bool _currencyWordMet;

    private TokenType? _lastSmallMagnitude;
    private TokenType? _lastBigMagnitude;
    
    private bool _expectsDigit;
    private bool _expectsMagnitude;

    public bool Parse(IList<KataToken> tokenList)
    {
        if (tokenList.Count == 0)
            return true;

        Init();
        
        for (int i = tokenList.Count - 1; i >= 0; i--)
        {
            var t = tokenList[i];
            if (t.Type == TokenType.CentsWord)
            {
                _centsWordMet = true;
                continue;
            }

            if (!CheckCurrencyValidity(t))
                return false;

            if (!CheckSmallMagnitudeValidity(t))
                return false;

            if (!CheckBigMagnitudeValidity(t))
                return false;

            _separatorJustMet = t.Type == TokenType.Separator;
        }

        return !_separatorJustMet && _currencyWordMet; // separator is the first token (eg: 'dollars and thirty two cents') or currency missing
    }

    private void Init()
    {
        _centsWordMet = false;
        _separatorJustMet = false;
        _currencyWordMet = false;

        _lastSmallMagnitude = null;
        _lastBigMagnitude = null;
        
        _expectsDigit = false;
        _expectsMagnitude = false;
    }

    private bool CheckCurrencyValidity(KataToken token)
    {
        if (token.Type == TokenType.Currency)
        {
            _currencyWordMet = true;
            if ((_lastSmallMagnitude.HasValue || _expectsMagnitude) && !_centsWordMet)
                return false;
            if (_lastSmallMagnitude is TokenType.HundredWord || _lastBigMagnitude.HasValue || (_centsWordMet && !_expectsMagnitude))
                return false;

            _lastSmallMagnitude = null;
            _expectsMagnitude = false;
            _centsWordMet = false;
        }

        return true;
    }

    private bool CheckSmallMagnitudeValidity(KataToken token)
    {
        if (_bigMagnitudes.Contains(token.Type))
        {
            if (_expectsDigit)
                return false;
            _lastSmallMagnitude = null;
            _expectsMagnitude = false;
        }
        else if (_smallMagnitudes.Contains(token.Type))
        {
            if (_expectsDigit || (_separatorJustMet && token.Type == TokenType.TenWord))
                return false;
            if (_lastSmallMagnitude.HasValue && _smallMagnitudes.IndexOf(token.Type) <= _smallMagnitudes.IndexOf(_lastSmallMagnitude.Value))
                return false;

            _lastSmallMagnitude = token.Type;
            _expectsMagnitude = false;
            _expectsDigit = true;
        }
        else if (token.Type == TokenType.Digit)
        {
            if (_expectsMagnitude || _separatorJustMet)
                return false;

            _expectsMagnitude = true;
            _expectsDigit = false;
        }

        return true;
    }

    private bool CheckBigMagnitudeValidity(KataToken token)
    {
        if (!_bigMagnitudes.Contains(token.Type))
            return true;
        
        if (_lastBigMagnitude.HasValue && _bigMagnitudes.IndexOf(token.Type) <= _bigMagnitudes.IndexOf(_lastBigMagnitude.Value))
            return false;

        if (token.Type == TokenType.BillionWord)
        {
            _lastBigMagnitude = null;
        }
        else
        {
            _lastBigMagnitude = token.Type;
        }
        
        return true;
    }
}