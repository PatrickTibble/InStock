using InStock.Frontend.Abstraction.Managers;

namespace InStock.Frontend.Core.Managers
{
    public class SessionManager : ISessionManager
    {
        private bool isValid = false;
        public Task<bool> ValidateSessionAsync()
        {
            var valid = isValid;
            isValid = !isValid;
            return Task.FromResult(valid);
        }
    }
}