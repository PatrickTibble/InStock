namespace InStock.Common.IdentityService.Abstraction
{
    public static class Constants
    {
        public const string Register = "api/v1/Identity/Register";
        public const string VerifyEmail = "api/v1/Identity/VerifyEmail";
        public const string SendVerificationLink = "api/v1/Identity/SendVerificationLink";
        public const string Authenticate = "api/v1/Identity/Authenticate";
        public const string UserClaims = "api/v1/Identity/UserClaims";

        public const string UserProfile = "api/v1/User/UserProfile";
        public const string UserAddress = "api/v1/User/Address";
        public const string UserAddresses = "api/v1/User/UserAddresses";

        public const string SessionState = "api/v1/Session/SessionState";
        public const string ContinueSession = "api/v1/Session/Continue";
        public const string EndSession = "api/v1/Session/End";
        public const string CreateSession = "api/v1/Session/Create";

        public const string GetToken = "api/v1/Identity/Token";
        public const string ValidateToken = "api/v1/Identity/Validate";
        public const string RefreshToken = "api/v1/Identity/Refresh";

        public const string BaseUrl = "https://localhost:44315";
    }
}