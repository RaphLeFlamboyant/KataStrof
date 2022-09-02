using Moq;
using NumbersInWords.Interpreter;
using NumbersInWords.Lexing;
using NumbersInWords.Parsing;
using NumbersInWords.Token;
using NumbersInWords.Translator;

namespace NumbersInWordsTests.Translator;

public class KataTranslatorTests
{
    [Test]
    public void KataTranslator_LexerErrorTest()
    {
        var lexerMoq = new Mock<IKataLexer>();
        lexerMoq.Setup(l => l.Read(It.IsAny<string>(), out It.Ref<IList<KataToken>>.IsAny)).Returns(false);
        
        var parserMoq = new Mock<IKataParser>();
        var interpreterMoq = new Mock<IKataInterpreter>();

        var translator = new KataTranslator(lexerMoq.Object, parserMoq.Object, interpreterMoq.Object);
        translator.Translate("fake input");
        
        lexerMoq.Verify(l => l.Read(It.IsAny<string>(), out It.Ref<IList<KataToken>>.IsAny));
        parserMoq.Verify(p => p.Parse(It.IsAny<IList<KataToken>>()), Times.Never);
        interpreterMoq.Verify(i => i.Translate(It.IsAny<IList<KataToken>>()), Times.Never);
    }
    
    [Test]
    public void KataTranslator_ParserErrorTest()
    {
        var lexerMoq = new Mock<IKataLexer>();
        lexerMoq.Setup(l => l.Read(It.IsAny<string>(), out It.Ref<IList<KataToken>>.IsAny)).Returns(true);
        
        var parserMoq = new Mock<IKataParser>();
        parserMoq.Setup(p => p.Parse(It.IsAny<IList<KataToken>>())).Returns(false);
        
        var interpreterMoq = new Mock<IKataInterpreter>();

        var translator = new KataTranslator(lexerMoq.Object, parserMoq.Object, interpreterMoq.Object);
        translator.Translate("fake input");
        
        lexerMoq.Verify(l => l.Read(It.IsAny<string>(), out It.Ref<IList<KataToken>>.IsAny));
        parserMoq.Verify(p => p.Parse(It.IsAny<IList<KataToken>>()));
        interpreterMoq.Verify(i => i.Translate(It.IsAny<IList<KataToken>>()), Times.Never);
    }
    
    [Test]
    public void KataTranslator_CallDependenciesTest()
    {
        var lexerMoq = new Mock<IKataLexer>();
        lexerMoq.Setup(l => l.Read(It.IsAny<string>(), out It.Ref<IList<KataToken>>.IsAny)).Returns(true);
        
        var parserMoq = new Mock<IKataParser>();
        parserMoq.Setup(p => p.Parse(It.IsAny<IList<KataToken>>())).Returns(true);
        
        var interpreterMoq = new Mock<IKataInterpreter>();
        interpreterMoq.Setup(i => i.Translate(It.IsAny<IList<KataToken>>())).Returns(string.Empty);

        var translator = new KataTranslator(lexerMoq.Object, parserMoq.Object, interpreterMoq.Object);
        translator.Translate("fake input");
        
        lexerMoq.Verify(l => l.Read(It.IsAny<string>(), out It.Ref<IList<KataToken>>.IsAny));
        parserMoq.Verify(p => p.Parse(It.IsAny<IList<KataToken>>()));
        interpreterMoq.Verify(i => i.Translate(It.IsAny<IList<KataToken>>()));
    }
}