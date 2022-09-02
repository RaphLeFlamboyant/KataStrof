using NumbersInWords.Token;

namespace NumbersInWords.Lexing;

public interface IKataLexer
{
    bool Read(string word, out IList<KataToken> tokens);
}