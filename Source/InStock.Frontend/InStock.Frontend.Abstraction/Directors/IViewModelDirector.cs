using InStock.Frontend.Abstraction.Builders;
using System.ComponentModel;
using System.Windows.Input;

namespace InStock.Frontend.Abstraction.Directors;

public interface IViewModelDirector
{
    void SetBuilder(IViewModelBuilder builder);
    IList<INotifyPropertyChanged> CreateLoginPage(string usernamePlaceholder, string passwordPlaceholder, ICommand loginCommand, ICommand registerCommand);
}
