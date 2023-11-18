namespace InStock.Frontend.Abstraction.Validations
{
    public interface IValidationRule
    {
        string ValidationMessage { get; set; }
        bool Check(string? userInput);
    }
}