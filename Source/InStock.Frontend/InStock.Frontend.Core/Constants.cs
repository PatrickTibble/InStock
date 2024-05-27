namespace InStock.Frontend.Core
{
    internal static class Constants
    {
        internal static class Settings
        {
            internal const string AccessToken = nameof(AccessToken);
            internal const string RefreshToken = nameof(RefreshToken);
            internal const string DeviceId = nameof(DeviceId);
        }

        internal static class RuleConstants
        {
            internal static readonly char[] SpecialChars = ['!', '@', '#', '$', '%', '^', '&', '*', '(', ')', '-', '_', '+', '=', '{', '}', '[', ']', '|', '\\', ':', ';', '"', '\'', '<', '>', ',', '.', '?', '/', '`', '~'];
            internal static readonly char[] Numbers = ['0', '1', '2', '3', '4', '5', '6', '7', '8', '9'];
            internal static readonly char[] UppercaseLetters = ['A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z'];
            internal static readonly char[] LowercaseLetters = ['a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z'];
        }
    }
}