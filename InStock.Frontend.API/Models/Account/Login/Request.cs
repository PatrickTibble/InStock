using System;
using InStock.Frontend.API.Models.Base;

namespace InStock.Frontend.API.Models.Account.Login
{
    public class Request : IRequest
	{
        public string? Username { get; set; }
        public string? Password { get; set; }

        public bool IsValid => !string.IsNullOrEmpty(Username) && !string.IsNullOrEmpty(Password);
    }
}

