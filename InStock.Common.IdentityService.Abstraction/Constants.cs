namespace InStock.Common.IdentityService.Abstraction
{
    public static class Constants
    {
        public const string Register = "api/v1/Identity/Register";
        public const string VerifyEmail = "api/v1/Identity/VerifyEmail";
        public const string SendVerificationLink = "api/v1/Identity/SendVerificationLink";
        public const string Authenticate = "api/v1/Identity/Authenticate";
        public const string UserClaims = "api/v1/Identity/UserClaims";

        public const string BaseUrl = "https://localhost:44315";
    }
}