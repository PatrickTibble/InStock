using System;
using InStock.Frontend.API.Models.Base;

namespace InStock.Frontend.API.Models.Account.SessionStatus
{
    public class Response : IResponse
	{
        public bool IsCurrentSessionActive { get; set; }
        public Guid? CurrentSessionId { get; set; }

        public bool IsSuccessfulStatusCode => IsCurrentSessionActive && CurrentSessionId.HasValue;
    }
}

