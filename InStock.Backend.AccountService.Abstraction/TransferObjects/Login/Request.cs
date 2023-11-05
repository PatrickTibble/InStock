using System;
using InStock.Common.Models.Base;

namespace InStock.Common.Models.Account.Login
{
    public class Request : IRequest
	{
        public string? Username { get; set; }
        public string? Password { get; set; }

        public bool IsValid => !string.IsNullOrEmpty(Username) && !string.IsNullOrEmpty(Password);

        public IList<string> Claims { get; set; }
    }
}

