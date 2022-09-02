using NumbersInWords.Helpers;

namespace NumbersInWordsTests;

public class TranslationChainModularTests
{
    [Test]
    [TestCase("nine hundred eighty seven billion six hundred fifty four million three hundred twenty one thousand nine hundred eighty seven dollars and fifty two cents", "987 654 321 987.52 $")]
    [TestCase("zero dollars and fifty two cents", "0.52 $")]
    [TestCase("ten dollars", "10 $")]
    public void TranslationTest(string input, string expectedOutput)
    {
        var interpreter = TranslatorHelper.BuildWordToNumberTranslator();
        var res = interpreter.Translate(input);
        Assert.That(res, Is.EqualTo(expectedOutput));
        
        interpreter = TranslatorHelper.BuildNumberToWordTranslator();
        res = interpreter.Translate(expectedOutput);
        Assert.That(res, Is.EqualTo(input));
    }
}