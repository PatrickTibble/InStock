﻿using InStock.Common.Models.Base;

namespace InStock.Common.Models.Account.CreateAccount
{
    public class Request : BaseRequest
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }

        protected override bool Validate()
        {
            throw new NotImplementedException();
        }
    }
}