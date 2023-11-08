using InStock.Backend.Common.Attributes;
using InStock.Common.IdentityService.Abstraction.Services;
using InStock.Common.IdentityService.Abstraction.TransferObjects.ContinueSession;
using InStock.Common.IdentityService.Abstraction.TransferObjects.EndSession;
using InStock.Common.IdentityService.Abstraction.TransferObjects.SessionState;
using InStock.Common.Models.Base;

namespace InStock.Backend.IdentityService.Core.Services
{
    public class SessionService : ISessionService
    {
        public Task<Result<ContinueSessionResponse>> ContinueSessionAsync([AccessTokenHeader] string accessToken)
        {
            throw new NotImplementedException();
        }

        public Task<Result<EndSessionResponse>> EndSessionAsync([AccessTokenHeader] string accessToken)
        {
            throw new NotImplementedException();
        }

        public Task<Result<SessionStateResponse>> GetSessionStateAsync([AccessTokenHeader] string accessToken)
        {
            throw new NotImplementedException();
        }
    }
}