using InStock.Frontend.Abstraction.Factories;
using InStock.Frontend.Abstraction.Validations;
using InStock.Frontend.Core.Validations;

namespace InStock.Frontend.Core.Factories;

public class ValidationRuleFactory : IValidationRuleFactory
{
    public IValidationRule[] RequiredTextEntryRules(string? message = default)
    {
        return
        [
            new LengthValidationRule(1, int.MaxValue, message ?? "This field is required")
        ];
    }

    public IValidationRule[] CreatePasswordRules()
    {
        return
        [
            new LengthValidationRule(8, 255, "Password must be at least 8 characters in length"),
            new RequiredCharactersValidationRule(1, Constants.RuleConstants.SpecialChars, "Password must contain at least one special character"),
            new RequiredCharactersValidationRule(1, Constants.RuleConstants.Numbers, "Password must contain at least one number"),
            new RequiredCharactersValidationRule(1, Constants.RuleConstants.UppercaseLetters, "Password must contain at least one uppercase letter"),
            new RequiredCharactersValidationRule(1, Constants.RuleConstants.LowercaseLetters, "Password must contain at least one lowercase letter")
        ];
    }

    public IValidationRule[] CreateUsernameRules()
    {
        return
        [
            new LengthValidationRule(6, 255, "Username must be at least 6 characters in length")
        ];
    }
}
