using InStock.Common.AccountService.Abstraction.Services;
using InStock.Common.AccountService.Abstraction.TransferObjects.CreateAccount;
using InStock.Common.AccountService.Abstraction.TransferObjects.Login;
using InStock.Common.Models.Base;
using Refit;

namespace InStock.Frontend.API.Mocks
{
    public class MockAccountService : BaseMockService, IAccountService
    {
        private IList<CreateAccountRequest> _accounts;

        public MockAccountService()
        {
            _accounts = new List<CreateAccountRequest>
            {
                new CreateAccountRequest
                {
                    FirstName = "Test",
                    LastName = "User",
                    ClientDescription = "Test User",
                    ClientId = Guid.NewGuid(),
                    ClientName = "Test",
                    Password = "Test",
                    Username = "test"
                }
            };
        }

        public Task<Result<LoginResponse>> CreateAccountAsync([Body] CreateAccountRequest request)
        {
            _accounts.Add(request);
            var data = new LoginResponse
            {
                AccessToken = "eyJSampleToken",
                RefreshToken = "eyJSampleToken"
            };

            return Delay()
                .ContinueWith(t => new Result<LoginResponse>(data));
        }

        public Task<Result<LoginResponse>> LoginAsync([Body] LoginRequest request)
        {
            var account = _accounts.FirstOrDefault(x => x.Username!.Equals(request.Username, StringComparison.OrdinalIgnoreCase) && x.Password!.Equals(request.Password, StringComparison.Ordinal));
            var result = default(Result<LoginResponse>);
            if (account != default)
            {
                result = new Result<LoginResponse>(new LoginResponse
                {
                    AccessToken = "eyJSampleToken",
                    RefreshToken = "eyJSampleToken"
                });
            }
            else
            {
                result = new Result<LoginResponse>(400, "Invalid username or password");
            }

            return Delay()
                .ContinueWith(_ => result);
        }
    }
}