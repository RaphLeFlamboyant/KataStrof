using NumbersInWords.Resources;
using NumbersInWords.Token;

namespace NumbersInWords.Lexing.impl;

public class NumbersKataLexer : IKataLexer
{
    private readonly IEnumerable<string> _currencySymbols = Words.CurrencySymbols.Split(('|'));
    private readonly IEnumerable<string> _separatorSymbols = Words.SeparatorSymbols.Split(('|'));

    public bool Read(string numberInWords, out IList<KataToken> tokens)
    {
        tokens = new List<KataToken>();
        for (int i = numberInWords.Length - 1; i >= 0; i--)
        {
            var c = numberInWords[i];
            if (char.IsWhiteSpace(c)) continue;
            var token = Transform(c);
            if (token == null)
                return false;
            tokens.Insert(0, token);
        }

        return true;
    }

    private KataToken Transform(char c)
    {
        var cAsString = $"{c}";
        
        if (c >= '0' && c <= '9')
            return new DigitKataToken(int.Parse(cAsString));

        if (_currencySymbols.Contains(cAsString))
            return new KataToken(TokenType.Currency, cAsString);

        if (_separatorSymbols.Contains(cAsString))
            return new KataToken(TokenType.Separator);

        return null;
    }
}