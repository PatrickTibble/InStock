namespace InStock.Common.AccountService.Abstraction
{
    public static class Constants
    {
        public const string CreateAccount = "api/v1/Account/CreateAccount";
        public const string Login = "api/v1/Account/Login";

        public const string UserProfile = "api/v1/User/UserProfile";
        public const string UserAddress = "api/v1/User/Address";
        public const string UserAddresses = "api/v1/User/UserAddresses";

        public const string SessionState = "api/v1/Session/SessionState";
        public const string ContinueSession = "api/v1/Session/Continue";
        public const string EndSession = "api/v1/Session/End";
        public const string CreateSession = "api/v1/Session/Create";

        public const string BaseUrl = "https://localhost:44356";
    }
}