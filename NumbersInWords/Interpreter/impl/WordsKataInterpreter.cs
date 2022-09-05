using System.Runtime.CompilerServices;
using NumbersInWords.Resources;
using NumbersInWords.Token;

namespace NumbersInWords.Interpreter.impl;

public class WordsKataInterpreter : IKataInterpreter
{
    private readonly IDictionary<string, string> _currencyTranslation = new Dictionary<string, string>
    {
        { "$", Words.DollarsWord },
        { "€", Words.EurosWord },
    };

    private string[] _magnitudes =
    {
        Words.TenWord,
        Words.HundredWord,
        Words.ThousandWord,
        Words.TenWord,
        Words.HundredWord,
        Words.MillionWord,
        Words.TenWord,
        Words.HundredWord,
        Words.BillionWord
    };

    private readonly Dictionary<int, string> _unitWords = new Dictionary<int, string>
    {
        { 0, Words.ZeroWord },
        { 1, Words.OneWord },
        { 2, Words.TwoWord },
        { 3, Words.ThreeWord },
        { 4, Words.FourWord },
        { 5, Words.FiveWord },
        { 6, Words.SixWord },
        { 7, Words.SevenWord },
        { 8, Words.EightWord },
        { 9, Words.NineWord }
    };
    
    private readonly Dictionary<int, string> _teenWords = new Dictionary<int, string>
    {
        { 0, Words.TenWord },
        { 1, Words.ElevenWord },
        { 2, Words.TwelveWord },
        { 3, Words.ThirteenWord },
        { 4, Words.FourteenWord.Split('|').First() },
        { 5, Words.FifteenWord },
        { 6, Words.SixteenWord },
        { 7, Words.SeventeenWord },
        { 8, Words.EighteenWord },
        { 9, Words.NineteenWord }
    };
    
    private readonly Dictionary<int, string> _tenWords = new Dictionary<int, string>
    {
        { 2, Words.TwentyWord },
        { 3, Words.ThirtyWord },
        { 4, Words.FourtyWord.Split('|').First() },
        { 5, Words.FiftyWord },
        { 6, Words.SixtyWord },
        { 7, Words.SeventyWord },
        { 8, Words.EightyWord },
        { 9, Words.NinetyWord }
    };

    private readonly List<string> _wordsChain = new List<string>();
    private readonly List<KataToken> _digitBuffer = new List<KataToken>();
    private readonly List<string> _centsBuffer = new List<string>();
    private string _currency = "";
    private int _digitCount;

    public string Translate(IList<KataToken> tokens)
    {
        Init();
        
        for (int i = tokens.Count - 1; i >= 0; i--)
        {
            var token = tokens[i];

            switch (token.Type)
            {
                case TokenType.Currency:
                    _digitCount = 0;
                    _currency = _currencyTranslation[token.AdditionalData];
                    break;
                case TokenType.Separator:
                    if (_digitCount < 3)
                    {
                        _digitCount = 0;
                        if (_digitBuffer.Count == 1)
                            _digitBuffer.Insert(0, new DigitKataToken(0));
                        _centsBuffer.Add(Words.CentWords.Split('|').First());
                        _centsBuffer.AddRange(TranslateAndClearBuffer());
                    }
                    break;
                case TokenType.CentsWord:
                    break;
                case TokenType.Digit:
                    if (_digitCount % 3 < 2)
                    {
                        _digitBuffer.Add(token);
                    }
                    else
                    {
                        _wordsChain.AddRange(TranslateAndClearBuffer());
                        _wordsChain.Add(_magnitudes[(_digitCount - 1) % _magnitudes.Length]);
                        _wordsChain.Add(_unitWords[int.Parse(token.AdditionalData)]);
                    }
                    
                    _digitCount++;
                    break;
                case TokenType.TenWord:
                    break;
                default:
                    if (_digitBuffer.Any())
                    {
                        _wordsChain.AddRange(TranslateAndClearBuffer());
                    }
                    break;
            }
        }

        _wordsChain.AddRange(TranslateAndClearBuffer());
        var centsChain = GetCurrencyAndCentsChain();
        if (!_wordsChain.Any() && centsChain.Any() && centsChain.Last().Equals(_currency))
            centsChain.Add(_unitWords[0]);
        centsChain.AddRange(_wordsChain);

        centsChain.Reverse();
        return string.Join(" ", centsChain);
    }
    
    private void Init()
    {
        _wordsChain.Clear();
        _digitBuffer.Clear();
        _centsBuffer.Clear();
        _currency = "";
        _digitCount = 0;
    }

    private List<string> GetCurrencyAndCentsChain()
    {
        var res = new List<string>();
        
        if (_centsBuffer.Any())
        {
            res.AddRange(_centsBuffer);
            if (!string.IsNullOrEmpty(_currency))
                res.Add(Words.SeparatorWords.Split('|').First());
        }
        
        if (!string.IsNullOrEmpty(_currency))
            res.Add(_currency);
        
        return res;
    }

    private List<string> TranslateAndClearBuffer()
    {
        var res = new List<string>();
        
        if (_digitBuffer.Count == 0) 
            return res;
        
        var unitTokenValue = int.Parse(_digitBuffer[0].AdditionalData);
        string tenWord = "";
        
        if (_digitCount > 3)
            res.Add(_magnitudes[(_digitCount - 1 - (_digitBuffer.Count)) % _magnitudes.Length]);

        if (_digitBuffer.Count == 2)
        {
            var teenToken = _digitBuffer[1];
            var tokenValue = int.Parse(teenToken.AdditionalData);
            if (tokenValue == 1)
            {
                res.Add(_teenWords[unitTokenValue]);
                _digitBuffer.Clear();
                return res;
            }
            
            if (tokenValue > 1)
            {
                tenWord = _tenWords[tokenValue];
            }
        }

        if (unitTokenValue != 0)
            res.Add(_unitWords[unitTokenValue]);
        if (!string.IsNullOrEmpty(tenWord))
            res.Add(tenWord);
        
        _digitBuffer.Clear();
        return res;
    }
}