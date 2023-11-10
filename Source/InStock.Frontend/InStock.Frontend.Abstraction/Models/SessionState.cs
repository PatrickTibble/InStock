namespace InStock.Frontend.Abstraction.Models
{
	public class SessionState
	{
		public bool IsValid { get; set; }

		public Guid? SessionId { get; set; }

		public static SessionState Default { get; } = new SessionState
		{
			IsValid = false,
			SessionId = null
		};
	}
}