using InStock.Backend.Common.Attributes;
using InStock.Common.IdentityService.Abstraction.TransferObjects.ContinueSession;
using InStock.Common.IdentityService.Abstraction.TransferObjects.EndSession;
using InStock.Common.IdentityService.Abstraction.TransferObjects.SessionState;
using InStock.Common.Models.Base;
using Refit;

namespace InStock.Common.IdentityService.Abstraction.Services
{
    public interface ISessionService
    {
        [Post($"/{Constants.SessionState}")]
        Task<Result<ContinueSessionResponse>> ContinueSessionAsync([AccessTokenHeader] string accessToken);

        // Login should create the session... I think
        //[Post($"/{Constants.CreateSession}")]
        //Task<Result<CreateSessionResponse>> CreateSessionAsync([AccessTokenHeader] string accessToken);

        [Post($"/{Constants.EndSession}")]
        Task<Result<EndSessionResponse>> EndSessionAsync([AccessTokenHeader] string accessToken);

        [Get($"/{Constants.SessionState}")]
        Task<Result<SessionStateResponse>> GetSessionStateAsync([AccessTokenHeader] string accessToken);
    }
}