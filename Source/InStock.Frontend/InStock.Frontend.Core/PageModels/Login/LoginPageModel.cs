using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using InStock.Frontend.Abstraction.Directors;
using InStock.Frontend.Abstraction.Managers;
using InStock.Frontend.Abstraction.Models;
using InStock.Frontend.Abstraction.Services.Alerts;
using InStock.Frontend.Abstraction.Services.Navigation;
using InStock.Frontend.Abstraction.Services.Platform;
using InStock.Frontend.Core.PageModels.Base;
using InStock.Frontend.Core.PageModels.Dashboard;
using InStock.Frontend.Core.Resources.Localization;

namespace InStock.Frontend.Core.PageModels.Login;

public partial class LoginPageModel : BaseCollectionViewPageModel
{
    private readonly string UsernamePlaceholder = Strings.Placeholder_Username;
    private readonly string PasswordPlaceholder = Strings.Placeholder_Password;

    private readonly IAlertService _alertService;
    private readonly INavigationService _navigationService;
    private readonly IAccountManager _accountManager;
    private readonly IViewModelDirector _director;

    [ObservableProperty]
    private bool _isLoading;

    [ObservableProperty]
    private string? _appVersion;

    public LoginPageModel(
        INavigationService navigationService,
        IClientInfoService infoService,
        IAlertService alertService,
        IViewModelDirector director,
        IAccountManager accountManager)
    {
        _director = director;
        _alertService = alertService;
        _navigationService = navigationService;
        _accountManager = accountManager;

        AppVersion = infoService.AppVersion.ToString();
        Items = _director.CreateLoginPage(UsernamePlaceholder, PasswordPlaceholder, CompleteCommand, RegisterCommand);
    }

    protected override async Task OnCompleteValidatedAsync()
    {
        await base
            .OnCompleteValidatedAsync()
            .ConfigureAwait(false);

        IsLoading = true;

        var username = Items
            ?.OfType<IEntry>()
            .FirstOrDefault(e => e.Placeholder.Equals(UsernamePlaceholder))
            ?.Text;

        var password = Items
            ?.OfType<IEntry>()
            .FirstOrDefault(e => e.Placeholder.Equals(PasswordPlaceholder))
            ?.Text;

        await TryLoginWithCredentialsAsync(username, password).ConfigureAwait(false);

        IsLoading = false;
    }

    private async Task TryLoginWithCredentialsAsync(string? username, string? password)
    {
        await _navigationService.NavigateToAsync<LoginPageModel>().ConfigureAwait(false);
        return;

        var loginResult = await _accountManager
            .LoginAsync(username, password)
            .ConfigureAwait(false);

        if (loginResult.Result)
        {
            await _navigationService
                .NavigateToAsync<MainPageModel>(setRoot: true)
                .ConfigureAwait(false);

            return;
        }

        await _alertService
            .ShowServiceAlert(Strings.AlertTitle_LoginFailed, Strings.AlertBody_LoginFailed, Strings.AlertAction_Confirm)
            .ConfigureAwait(false);
    }

    [RelayCommand]
    private Task OnRegisterAsync()
        => _navigationService.NavigateToAsync<CreateAccountPageModel>();
}