﻿using InStock.Backend.IdentityService.Abstraction.TransferObjects.Base;

namespace InStock.Backend.IdentityService.Abstraction.TransferObjects.VerifyEmail
{
    public class VerifyEmailResponse : BaseResponse
    {
        public bool IsVerified { get; set; }
    }
}