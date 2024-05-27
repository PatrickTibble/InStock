using InStock.Frontend.Abstraction.Builders;
using InStock.Frontend.Abstraction.Enums;
using InStock.Frontend.Abstraction.Models;
using InStock.Frontend.Abstraction.Validations;
using InStock.Frontend.Core.ViewModels.Cards;
using InStock.Frontend.Core.ViewModels.Input;
using InStock.Frontend.Core.ViewModels.Labels;
using System.ComponentModel;
using System.Windows.Input;

namespace InStock.Frontend.Core.Builders;

public class ViewModelBuilder : IViewModelBuilder
{
    private IList<INotifyPropertyChanged> _viewModels;

    public ViewModelBuilder()
    {
        _viewModels = new List<INotifyPropertyChanged>();
    }

    public IViewModelBuilder AddButton(string title, ICommand command)
    {
        _viewModels.Add(new ButtonViewModel
        {
            Title = title,
            Command = command
        });
        return this;
    }

    public IViewModelBuilder AddChartView()
    {
        _viewModels.Add(new ChartViewModel());
        return this;
    }

    public IViewModelBuilder AddHeaderLabel(string text, ICommand? tapCommand = default)
    {
        _viewModels.Add(new SingleLabelViewModel
        {
            Text = text,
            IsEnabled = tapCommand != null,
            LabelStyle = LabelStyle.HeaderDark,
            Margins = new MarginSet(12, 5),
        });
        return this;
    }

    public IViewModelBuilder AddImageRow(Images image, ICommand? tapCommand = default)
    {

        return this;
    }

    public IViewModelBuilder AddParagraphSmallLabel(string text, ICommand? tapCommand = default)
    {
        _viewModels.Add(new SingleLabelViewModel
        {
            Text = text,
            IsEnabled = tapCommand != null,
            LabelStyle = LabelStyle.BodyDark,
            Margins = new MarginSet(12, 5),
        });
        return this;
    }

    public IViewModelBuilder AddPasswordEntry(string placeholder)
    {
        _viewModels.Add(new PrimaryEntryViewModel
        {
            Placeholder = placeholder,
            IsPassword = true
        });
        return this;
    }

    public IViewModelBuilder AddTextEntry(string placeholder)
    {
        _viewModels.Add(new PrimaryEntryViewModel
        {
            Placeholder = placeholder
        });
        return this;
    }

    public IViewModelBuilder AddTitleLabel(string text, ICommand? tapCommand = default)
    {
        _viewModels.Add(new SingleLabelViewModel
        {
            Text = text,
            IsEnabled = tapCommand != null,
            LabelStyle = LabelStyle.TitleDark,
            Margins = new MarginSet(12, 5),
        });
        return this;
    }

    public IList<INotifyPropertyChanged> Build()
    {
        var vms = _viewModels;
        _viewModels = new List<INotifyPropertyChanged>();
        return vms;
    }

    public IViewModelBuilder WithValidations(params IValidationRule[]? validationRules)
    {
        if (_viewModels?.LastOrDefault() is IValidatable validatable && validationRules != null)
        {
            validatable.ValidationRules ??= new List<IValidationRule>();
            foreach (var validationRule in validationRules)
            {
                validatable.ValidationRules.Add(validationRule);
            }
        }
        return this;
    }
}
