﻿namespace InStock.Common.AccountService.Abstraction.TransferObjects.Login
{
	public class LoginResponse
	{
        public static object? Default { get; set; }
        public string? AccessToken { get; set; }
    }
}