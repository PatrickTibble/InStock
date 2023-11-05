﻿using InStock.Backend.AccountService.Abstraction.Entities;
using InStock.Backend.AccountService.Abstraction.Repositories;
using InStock.Backend.AccountService.Abstraction.Services;
using InStock.Backend.AccountService.Abstraction.TransferObjects.CreateAccount;
using InStock.Backend.AccountService.Abstraction.TransferObjects.SessionState;

namespace InStock.Backend.AccountService.Core.Services.Account
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;

        public AccountService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public async Task<CreateAccountResponse> CreateAccountAsync(CreateAccountRequest request)
        {
            var response = new CreateAccountResponse();

            var user = await _accountRepository.GetUserByUsernameAsync(request.Username);

            if (user == null)
            {
                // Try to create the account
                user = new UserAccount
                {
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    Username = request.Username
                };

                var result = await _accountRepository.AddUserAsync(user);
                
            }

            return response;
        }

        public Task<SessionStateResponse> GetSessionStateAsync(string accessToken)
        {
            // retrieve user information using access token

            // retrieve session information using user info

            // return session info as response
            return Task.FromResult(new SessionStateResponse
            {
                CurrentSessionId = Guid.NewGuid(),
                IsCurrentSessionActive = true,
            });
        }
    }
}