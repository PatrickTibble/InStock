namespace InStock.Frontend.Abstraction.Models
{
    public class CreateAccountResult
    {
        public bool IsSuccessful { get; set; }

        public static CreateAccountResult Default { get; } = new CreateAccountResult {  IsSuccessful = false };
    }
}