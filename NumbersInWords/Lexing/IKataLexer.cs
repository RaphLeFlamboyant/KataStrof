using NumbersInWords.Token;

namespace NumbersInWords.Lexing;

public interface IKataLexer
{
    bool Read(string numberInWords, out IList<KataToken> tokens);
}