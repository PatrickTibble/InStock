namespace InStock.Frontend.Abstraction.Validations;

public interface IValidatable
{
    bool IsValid { get; }
    bool Validate();
    IList<IValidationRule> ValidationRules { get; set; }
}
