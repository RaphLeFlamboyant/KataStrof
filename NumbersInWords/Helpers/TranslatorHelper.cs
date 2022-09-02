using NumbersInWords.Interpreter.impl;
using NumbersInWords.Lexing.impl;
using NumbersInWords.Parsing.impl;
using NumbersInWords.Translator;

namespace NumbersInWords.Helpers;

public static class TranslatorHelper
{
    public static KataTranslator BuildWordToNumberTranslator()
    {
        return new KataTranslator(new WordsKataLexer(), new WordsKataParser(), new NumbersKataInterpreter());
    }
    
    public static KataTranslator BuildNumberToWordTranslator()
    {
        return new KataTranslator(new NumbersKataLexer(), new NumbersKataParser(), new WordsKataInterpreter());
    }
}