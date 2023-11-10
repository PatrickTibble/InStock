using InStock.Frontend.Abstraction.Models;

namespace InStock.Frontend.Abstraction.Repositories
{
	public interface ISessionRepository
    {
        Task<SessionState> GetSessionStateAsync();
    }
}

