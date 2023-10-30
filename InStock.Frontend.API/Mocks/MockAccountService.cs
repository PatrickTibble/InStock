﻿using InStock.Frontend.API.Account;
using Refit;

namespace InStock.Frontend.API.Mocks
{
    internal class MockAccountService : BaseMockService, IAccountService
    {
        public Task<Models.Account.SessionStatus.Response> GetSessionStatus(CancellationToken token)
        {
            return Delay(token)
                .ContinueWith(_ => new Models.Account.SessionStatus.Response
                {

                });
        }

        public Task<Models.Account.Login.Response> LoginAsync([Body] Models.Account.Login.Request request, CancellationToken token)
        {
            return Delay(token)
                .ContinueWith(_ => new Models.Account.Login.Response
                {

                });
        }

        public Task<Models.Account.Signout.Response> SignoutAsync([Body] Models.Account.Signout.Request request, CancellationToken token)
        {
            return Delay(token)
                .ContinueWith(_ => new Models.Account.Signout.Response
                {

                });
        }
    }
}