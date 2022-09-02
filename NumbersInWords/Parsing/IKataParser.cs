using NumbersInWords.Token;

namespace NumbersInWords.Parsing;

public interface IKataParser
{
    bool Parse(IList<KataToken> tokenList);
}