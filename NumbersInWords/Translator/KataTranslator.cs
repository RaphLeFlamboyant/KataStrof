using NumbersInWords.Interpreter;
using NumbersInWords.Lexing;
using NumbersInWords.Parsing;

namespace NumbersInWords.Translator;

public class KataTranslator
{
    private IKataLexer _lexer;
    private IKataParser _parser;
    private IKataInterpreter _interpreter;
    
    public KataTranslator(IKataLexer lexer, IKataParser parser, IKataInterpreter interpreter)
    {
        _lexer = lexer;
        _parser = parser;
        _interpreter = interpreter;
    }

    public string Translate(string input)
    {
        if (!_lexer.Read(input, out var tokens) || !_parser.Parse(tokens))
            return "Error";

        return _interpreter.Translate(tokens);
    }
}