using System;
using InStock.Common.Models.Base;

namespace InStock.Common.Models.Account.SessionStatus
{
    public class Response : IResponse
	{
        public bool IsCurrentSessionActive { get; set; }
        public Guid? CurrentSessionId { get; set; }

        public bool IsSuccessfulStatusCode => IsCurrentSessionActive && CurrentSessionId.HasValue;
    }
}

