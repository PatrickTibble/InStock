using System;
using InStock.Common.Models.Base;

namespace InStock.Common.Models.Account.Signout
{
    public class Response : IResponse
	{
        public bool IsSuccessfulStatusCode { get; } = true;
    }
}

