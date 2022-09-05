using NumbersInWords.Resources;
using NumbersInWords.Token;

namespace NumbersInWords.Interpreter.impl;

public class NumbersKataInterpreter : IKataInterpreter
{
    private TokenType[] _magnitudes =
    {
        TokenType.TenWord,
        TokenType.HundredWord,
        TokenType.ThousandWord,
        TokenType.TenWord,
        TokenType.HundredWord,
        TokenType.MillionWord,
        TokenType.TenWord,
        TokenType.HundredWord,
        TokenType.BillionWord
    };

    public string Translate(IList<KataToken> tokens)
    {
        var res = "";
        var digitCount = 0;

        for (int i = tokens.Count - 1; i >= 0; i--)
        {
            var token = tokens[i];
            switch (token.Type)
            {
                case TokenType.Digit:
                    if (digitCount > 0 && digitCount % 3 == 0)
                        res = " " + res;
                    res = token.AdditionalData + res;
                    digitCount++;
                    break;
                case TokenType.Currency:
                    digitCount = 0;
                    res = res + " " + token.AdditionalData;
                    break;
                case TokenType.Separator:
                    if (digitCount < 3)
                    {
                        if (digitCount == 1)
                            res = "0" + res;
                        res = "." + res;
                        digitCount = 0;
                    }

                    break;
                case TokenType.CentsWord:
                    break;
                default:
                    var (zeros, zeroCount) = GetMissingMagnitudeZeros(token.Type, digitCount);
                    res = zeros + res;
                    digitCount += zeroCount;
                    break;
            }
        }

        return res;
    }

    private (string zeros, int zeroCount) GetMissingMagnitudeZeros(TokenType tType, int digitCount)
    {
        var res = "";
        var initialCount = digitCount;

        if (digitCount == 0)
        {
            res = "0";
            digitCount++;
        }

        while (_magnitudes[(digitCount - 1) % _magnitudes.Length] != tType)
        {
            if (digitCount % 3 == 0)
                res = " " + res;
            res = "0" + res;
            digitCount++;
        }

        return (res, digitCount - initialCount);
    }
}