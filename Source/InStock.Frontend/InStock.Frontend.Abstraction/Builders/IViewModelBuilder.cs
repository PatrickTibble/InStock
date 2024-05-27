using InStock.Frontend.Abstraction.Enums;
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
    IViewModelBuilder AddHeaderLabel(string text, ICommand? tapCommand = default);
    IViewModelBuilder AddTitleLabel(string text, ICommand? tapCommand = default);
    IViewModelBuilder AddParagraphSmallLabel(string text, ICommand? tapCommand = default);
    IViewModelBuilder AddImageRow(Images image, ICommand? tapCommand = default);
    IViewModelBuilder AddButton(string title, ICommand command);

    IViewModelBuilder WithValidations(params IValidationRule[]? validationRules);
}
