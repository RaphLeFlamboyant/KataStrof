// See https://aka.ms/new-console-template for more information

using NumbersInWords.Helpers;
using NumbersInWords.Translator;

string againAnswer;

do
{
    Console.WriteLine("Select the mode");
    Console.WriteLine("[1] Number to words check translation");
    Console.WriteLine("[2] Words to number check translation");

    var answer = Console.ReadLine();
    if (answer != null)
    {
        switch (answer)
        {
            case "1":
                Translate(TranslatorHelper.BuildNumberToWordTranslator());
                break;
            case "2":
                Translate(TranslatorHelper.BuildWordToNumberTranslator());
                break;
            default:
                Console.WriteLine("Haha you're so fun");
                break;
        }
    }
    Console.WriteLine("Again ? [y]");
    againAnswer = Console.ReadLine() ?? "";
} while (againAnswer.ToLower() == "y");

void Translate(KataTranslator translator)
{
    Console.WriteLine("Write the number please");
    var answer = Console.ReadLine();
    if (answer != null)
    {
        Console.WriteLine(translator.Translate(answer));
    }
}