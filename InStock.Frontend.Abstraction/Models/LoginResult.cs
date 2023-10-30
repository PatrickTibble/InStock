namespace InStock.Frontend.Abstraction.Models
{
	public class LoginResult
	{
        public bool IsSuccessful { get; set; }

        public static LoginResult Default { get; } = new LoginResult
        {
            IsSuccessful = false
        };
    }
}