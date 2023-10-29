using System;
namespace InStock.Frontend.API.Models.Account.SessionStatus
{
	public class Response
	{
		public Response()
		{
		}

        public bool IsCurrentSessionActive { get; set; }
        public Guid? CurrentSessionId { get; set; }
    }
}

