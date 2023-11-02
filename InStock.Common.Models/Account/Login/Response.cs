using System;
using InStock.Common.Models.Base;

namespace InStock.Common.Models.Account.Login
{
	public class Response : IResponse
	{
		public bool IsSuccessfulStatusCode { get; } = true;
	}
}

