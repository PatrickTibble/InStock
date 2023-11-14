using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using InStock.Frontend.Abstraction.Managers;
using InStock.Frontend.Abstraction.Services.Alerts;
using InStock.Frontend.Abstraction.Services.Navigation;
using InStock.Frontend.Core.PageModels.Base;
using InStock.Frontend.Core.Resources.Localization;
using InStock.Frontend.Core.ViewModels.Input;

namespace InStock.Frontend.Core.PageModels.Login
{
    public partial class CreateAccountPageModel : BasePageModel
	{
        private readonly IAlertService _alertService;
        private readonly IAccountManager _accountManager;
        private readonly INavigationService _navigationService;

        [ObservableProperty]
        private bool _isLoading;

		public CreateAccountPageModel(
            INavigationService navigationService,
            IAccountManager accountRepository,
            IAlertService alertService)
		{
            _alertService = alertService;
            _accountManager = accountRepository;
            _navigationService = navigationService;

			FirstNameViewModel = new PrimaryEntryViewModel
			{
				Placeholder = Strings.Placeholder_FirstName
			};

			LastNameViewModel = new PrimaryEntryViewModel
            {
                Placeholder = Strings.Placeholder_LastName
            };

            UsernameViewModel = new PrimaryEntryViewModel
            {
                Placeholder = Strings.Placeholder_Username
            };

            PasswordViewModel = new PrimaryEntryViewModel
            {
                Placeholder = Strings.Placeholder_Password,
                IsPassword = true
            };

            ConfirmPasswordViewModel = new PrimaryEntryViewModel
            {
                Placeholder = Strings.Placeholder_Confirm_Password,
                IsPassword = true
            };

            CancelViewModel = new ButtonViewModel
            {
                Title = Strings.ButtonTitle_Cancel,
                Command = new AsyncRelayCommand(navigationService.PopAsync)
            };

            ConfirmViewModel = new ButtonViewModel
            {
                Title = Strings.ButtonTitle_Submit,
                Command = new AsyncRelayCommand(TryCreateAccountAsync)
            };
        }

        public PrimaryEntryViewModel FirstNameViewModel { get; }

        public PrimaryEntryViewModel LastNameViewModel { get; }

        public PrimaryEntryViewModel UsernameViewModel { get; }

        public PrimaryEntryViewModel PasswordViewModel { get; }

        public PrimaryEntryViewModel ConfirmPasswordViewModel { get; }

        public ButtonViewModel CancelViewModel { get; }

        public ButtonViewModel ConfirmViewModel { get; }

        private async Task TryCreateAccountAsync()
        {
            IsLoading = true;

            var result = await _accountManager
                .CreateAccountAsync(
                    firstName: FirstNameViewModel.Text,
                    lastName: LastNameViewModel.Text,
                    username: UsernameViewModel.Text,
                    password: PasswordViewModel.Text)
                .ConfigureAwait(false);

            if (!result.Result)
            {
                IsLoading = false;

                await _alertService
                    .ShowServiceAlert(
                        title: Strings.AlertTitle_CreateAccount_Failed,
                        message: Strings.AlertBody_CreateAccount_Failed,
                        confirm: Strings.AlertAction_Confirm)
                    .ConfigureAwait(false);
                return;
            }

            await _navigationService
                .PopAsync()
                .ConfigureAwait(false);
        }
	}
}