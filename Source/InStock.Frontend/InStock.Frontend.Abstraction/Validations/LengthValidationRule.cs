namespace InStock.Frontend.Abstraction.Validations
{
    public class LengthValidationRule : IValidationRule
    {
        public int MinLength { get; }
        public int MaxLength { get; }

        /// <summary>
        /// Ensure the length of the input is between the min and max length
        /// </summary>
        /// <param name="minLength"></param>
        /// <param name="maxLength"></param>
        /// <param name="message">The validation message to show</param>
        public LengthValidationRule(int minLength, int maxLength, string? message = null)
        {
            MinLength = minLength;
            MaxLength = maxLength;

            ValidationMessage = message ?? $"Length must be between {minLength} and {maxLength}";
        }

        public string ValidationMessage { get; set; }

        public bool Check(string? userInput)
        {
            if (userInput == null)
            {
                return false;
            }

            var length = userInput.Length;

            return length >= MinLength && length <= MaxLength;
        }
    }
}