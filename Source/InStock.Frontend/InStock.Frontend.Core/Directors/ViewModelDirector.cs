using InStock.Frontend.Abstraction.Builders;
using InStock.Frontend.Abstraction.Directors;
using InStock.Frontend.Core.Resources.Localization;
using System.ComponentModel;
using System.Windows.Input;

namespace InStock.Frontend.Core.Directors;

public class ViewModelDirector : IViewModelDirector
{
    private IViewModelBuilder? _builder;

    public void SetBuilder(IViewModelBuilder builder)
    {
        _builder = builder;
    }

    public IList<INotifyPropertyChanged> CreateLoginPage(string usernamePlaceholder, string passwordPlaceholder, ICommand loginCommand, ICommand registerCommand)
    {
        return _builder?
            .AddHeaderLabel("Test 123", null)
            .AddTitleLabel("Tester 456", null)
            .AddTextEntry(usernamePlaceholder)
            .AddPasswordEntry(passwordPlaceholder)
            .AddButton(Strings.ButtonTitle_Login, loginCommand)
            .AddButton(Strings.ButtonTitle_CreateAccount, registerCommand)
            .Build()
            ?? [];
    }
}
