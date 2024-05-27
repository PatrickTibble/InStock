using InStock.Frontend.Abstraction.Validations;

namespace InStock.Frontend.Core.Validations;

public class RequiredCharactersValidationRule : IValidationRule
{
    public RequiredCharactersValidationRule(int min, char[] search, string message)
    {
        Minimum = min;
        Search = search;
        ValidationMessage = message;
    }

    public int Minimum { get; set; }
    public char[] Search { get; set; }
    public string ValidationMessage { get; set; }

    public bool Check(string? userInput)
    {
        if (userInput == null)
        {
            return false;
        }

        var count = 0;

        foreach (var c in userInput)
        {
            if (Search.Contains(c))
            {
                count++;
            }

            if (count >= Minimum)
            {
                return true;
            }
        }

        return false;
    }
}
