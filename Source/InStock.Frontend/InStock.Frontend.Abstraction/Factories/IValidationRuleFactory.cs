using InStock.Frontend.Abstraction.Validations;

namespace InStock.Frontend.Abstraction.Factories;

public interface IValidationRuleFactory
{
    IValidationRule[] RequiredTextEntryRules(string? message = default);
    IValidationRule[] CreatePasswordRules();
    IValidationRule[] CreateUsernameRules();
}
