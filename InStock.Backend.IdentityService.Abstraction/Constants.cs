﻿namespace InStock.Backend.IdentityService.Abstraction
{
    public class Constants
    {
        public const string Register = "api/v1/Identity/Register";
        public const string VerifyEmail = "api/v1/Identity/VerifyEmail";
        public const string SendVerificationLink = "api/v1/Identity/SendVerificationLink";
        public const string Authenticate = "api/v1/Identity/Authenticate";
        public const string UserClaims = "api/v1/Identity/UserClaims";
        private Constants() { }
    }
}