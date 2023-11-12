namespace InStock.Frontend.Abstraction.Managers
{
    public interface ISessionManager
    {
        Task<bool> ValidateSessionAsync();
    }
}