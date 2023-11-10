namespace InStock.Common.IdentityService.Abstraction
{
    public static class Constants
    {
        public const string GetToken = "api/v1/Identity/Token";
        public const string ValidateToken = "api/v1/Identity/Validate";
        public const string RefreshToken = "api/v1/Identity/Refresh";

        public const string BaseUrl = "https://localhost:44315";
    }
}