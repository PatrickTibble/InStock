using InStock.Frontend.Abstraction.Builders;
using InStock.Frontend.Abstraction.Directors;
using InStock.Frontend.Abstraction.Enums;
using InStock.Frontend.Abstraction.Factories;
using InStock.Frontend.Core.Resources.Localization;
using System.ComponentModel;
using System.Windows.Input;

namespace InStock.Frontend.Core.Directors;

public class ViewModelDirector : IViewModelDirector
{
    private IViewModelBuilder? _builder;
    private IValidationRuleFactory? _ruleFactory;

    public void SetBuilder(IViewModelBuilder builder)
    {
        _builder = builder;
    }

    public void SetRuleFactory(IValidationRuleFactory factory)
    {
        _ruleFactory = factory;
    }

    public IList<INotifyPropertyChanged> CreateLoginPage(string usernamePlaceholder, string passwordPlaceholder, ICommand loginCommand, ICommand registerCommand)
    {
        return _builder?
            .AddImageRow(Images.Inventory)
            .AddTitleLabel(Strings.AppTitle)
            .AddHeaderLabel(Strings.Login)
            .AddTextEntry(usernamePlaceholder)
                .WithValidations(_ruleFactory?.RequiredTextEntryRules())
            .AddPasswordEntry(passwordPlaceholder)
                .WithValidations(_ruleFactory?.RequiredTextEntryRules())
            .AddButton(Strings.ButtonTitle_Login, loginCommand)
            .AddButton(Strings.ButtonTitle_CreateAccount, registerCommand)
            .Build()
            ?? [];
    }
}
