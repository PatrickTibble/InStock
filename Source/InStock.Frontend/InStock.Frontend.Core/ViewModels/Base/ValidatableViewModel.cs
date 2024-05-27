using CommunityToolkit.Mvvm.ComponentModel;
using InStock.Frontend.Abstraction.Validations;

namespace InStock.Frontend.Core.ViewModels.Base;

public abstract partial class ValidatableViewModel : BaseViewModel, IValidatable
{
    [ObservableProperty]
    private bool _isValid;

    [ObservableProperty]
    private string? _validationMessage;

    [ObservableProperty]
    private string? _text;

    public IList<IValidationRule>? ValidationRules { get; set; }

    public virtual bool Validate()
    {
        if (ValidationRules == null || ValidationRules.Count == 0)
        {
            IsValid = true;
            ValidationMessage = string.Empty;
            return IsValid;
        }

        var failedRules = ValidationRules
            .Where(rule => !rule.Check(Text))
            .ToList();

        IsValid = !failedRules.Any();
        var messages = failedRules.Select(rule => rule.ValidationMessage);
        ValidationMessage = string.Join(Environment.NewLine, messages);

        return IsValid;
    }
}