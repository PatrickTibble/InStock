using System;
using InStock.Frontend.API.Models.Base;

namespace InStock.Frontend.API.Models.Account.Login
{
	public class Response : IResponse
	{
		public bool IsSuccessfulStatusCode { get; } = true;
	}
}

