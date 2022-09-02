using NumbersInWords.Token;

namespace NumbersInWords.Interpreter;

public interface IKataInterpreter
{
    string Translate(IList<KataToken> tokens);
}