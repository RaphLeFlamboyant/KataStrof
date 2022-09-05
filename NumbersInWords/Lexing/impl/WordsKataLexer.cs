using NumbersInWords.Resources;
using NumbersInWords.Token;

namespace NumbersInWords.Lexing.impl;

public class WordsKataLexer : IKataLexer
{
    private readonly IDictionary<string, string> _currencyDico = new Dictionary<string, string>
    {
        {"dollars", "$"},
        {"dollar", "$"},
        {"euros", "€"},
        {"euro", "€"},
    };
    
    private readonly Dictionary<string, Func<string, List<KataToken>>> _tokenGenerator = new Dictionary<string, Func<string, List<KataToken>>>
    {
        { Words.ZeroWord, (_) => new List<KataToken> { new DigitKataToken(0) } },
        { Words.OneWord, (_) => new List<KataToken> { new DigitKataToken(1) } },
        { Words.TwoWord, (_) => new List<KataToken> { new DigitKataToken(2) } },
        { Words.ThreeWord, (_) => new List<KataToken> { new DigitKataToken(3) } },
        { Words.FourWord, (_) => new List<KataToken> { new DigitKataToken(4) } },
        { Words.FiveWord, (_) => new List<KataToken> { new DigitKataToken(5) } },
        { Words.SixWord, (_) => new List<KataToken> { new DigitKataToken(6) } },
        { Words.SevenWord, (_) => new List<KataToken> { new DigitKataToken(7) } },
        { Words.EightWord, (_) => new List<KataToken> { new DigitKataToken(8) } },
        { Words.NineWord, (_) => new List<KataToken> { new DigitKataToken(9) } },
        { Words.TenWord, (_) => new List<KataToken> { new DigitKataToken(1), new KataToken(TokenType.TenWord) } },
        { Words.TwentyWord, (_) => new List<KataToken> { new DigitKataToken(2), new KataToken(TokenType.TenWord) } },
        { Words.ThirtyWord, (_) => new List<KataToken> { new DigitKataToken(3), new KataToken(TokenType.TenWord) } },
        { Words.FourtyWord, (_) => new List<KataToken> { new DigitKataToken(4), new KataToken(TokenType.TenWord) } },
        { Words.FiftyWord, (_) => new List<KataToken> { new DigitKataToken(5), new KataToken(TokenType.TenWord) } },
        { Words.SixtyWord, (_) => new List<KataToken> { new DigitKataToken(6), new KataToken(TokenType.TenWord) } },
        { Words.SeventyWord, (_) => new List<KataToken> { new DigitKataToken(7), new KataToken(TokenType.TenWord) } },
        { Words.EightyWord, (_) => new List<KataToken> { new DigitKataToken(8), new KataToken(TokenType.TenWord) } },
        { Words.NinetyWord, (_) => new List<KataToken> { new DigitKataToken(9), new KataToken(TokenType.TenWord) } },
        { Words.ElevenWord, (_) => new List<KataToken> { new DigitKataToken(1), new KataToken(TokenType.TenWord), new DigitKataToken(1) } },
        { Words.TwelveWord, (_) => new List<KataToken> { new DigitKataToken(1), new KataToken(TokenType.TenWord), new DigitKataToken(2) } },
        { Words.ThirteenWord, (_) => new List<KataToken> { new DigitKataToken(1), new KataToken(TokenType.TenWord), new DigitKataToken(3) } },
        { Words.FourteenWord, (_) => new List<KataToken> { new DigitKataToken(1), new KataToken(TokenType.TenWord), new DigitKataToken(4) } },
        { Words.FifteenWord, (_) => new List<KataToken> { new DigitKataToken(1), new KataToken(TokenType.TenWord), new DigitKataToken(5) } },
        { Words.SixteenWord, (_) => new List<KataToken> { new DigitKataToken(1), new KataToken(TokenType.TenWord), new DigitKataToken(6) } },
        { Words.SeventeenWord, (_) => new List<KataToken> { new DigitKataToken(1), new KataToken(TokenType.TenWord), new DigitKataToken(7) } },
        { Words.EighteenWord, (_) => new List<KataToken> { new DigitKataToken(1), new KataToken(TokenType.TenWord), new DigitKataToken(8) } },
        { Words.NineteenWord, (_) => new List<KataToken> { new DigitKataToken(1), new KataToken(TokenType.TenWord), new DigitKataToken(9) } },
        { Words.CentWords, (_) => new List<KataToken> { new KataToken(TokenType.CentsWord) } },
        { Words.CurrencyWords, (s) => new List<KataToken> { new KataToken(TokenType.Currency, s) } },
        { Words.HundredWord, (_) => new List<KataToken> { new KataToken(TokenType.HundredWord) } },
        { Words.ThousandWord, (_) => new List<KataToken> { new KataToken(TokenType.ThousandWord) } },
        { Words.MillionWord, (_) => new List<KataToken> { new KataToken(TokenType.MillionWord) } },
        { Words.BillionWord, (_) => new List<KataToken> { new KataToken(TokenType.BillionWord) } },
        { Words.SeparatorWords, (_) => new List<KataToken> { new KataToken(TokenType.Separator) } }
    };

    public WordsKataLexer()
    {
        var keyList = _tokenGenerator.Keys.ToList();

        foreach (var key in keyList)
        {
            if (key.Contains('|'))
            {
                var words = key.Split('|');
                var generationFunction = _tokenGenerator[key];
                _tokenGenerator.Remove(key);

                foreach (var word in words)
                {
                    _tokenGenerator.Add(word, generationFunction);
                }
            }
        }
    }

    public bool Read(string numberInWords, out IList<KataToken> tokens)
    {
        var wordList = numberInWords.ToLowerInvariant().Split(new[] {' ', '\t', '\n', '\r'}, StringSplitOptions.RemoveEmptyEntries);

        var res = new List<KataToken>();
        tokens = res;
        foreach (var word in wordList)
        {
            if (!_tokenGenerator.ContainsKey(word))
                return false;

            var tokensToAdd = _tokenGenerator[word](word);
            var firstToken = tokensToAdd.First();
            if (tokensToAdd.Count == 1 && firstToken.Type == TokenType.Currency)
            {
                firstToken.AdditionalData = _currencyDico[firstToken.AdditionalData];
            }
            
            res.AddRange(tokensToAdd);
        }

        return true;
    }
}