using InStock.Frontend.Abstraction.Validations;
using System.ComponentModel;
using System.Windows.Input;

namespace InStock.Frontend.Abstraction.Builders;

public interface IViewModelBuilder
{
    IList<INotifyPropertyChanged> Build();
    IViewModelBuilder AddChartView();
    IViewModelBuilder AddTextEntry(string placeholder);
    IViewModelBuilder AddPasswordEntry(string placeholder);
    IViewModelBuilder AddHeaderLabel(string text, ICommand? tapCommand);
    IViewModelBuilder AddTitleLabel(string text, ICommand? tapCommand);
    IViewModelBuilder AddParagraphSmallLabel(string text, ICommand? tapCommand);
    IViewModelBuilder AddImageRow(string source);
    IViewModelBuilder AddButton(string title, ICommand command);

    IViewModelBuilder WithValidations(params IValidationRule[] validationRules);
}
