namespace InStock.Frontend.Abstraction.Managers
{
    public interface IStorageManager
    {
        Task<string?> GetAccessTokenAsync();

        Task<string?> GetRefreshTokenAsync();

        Task<bool> SetAccessTokenAsync(string? accessToken);

        Task<bool> SetRefreshTokenAsync(string? refreshToken);

        Task<bool> RemoveAccessTokenAsync();

        Task<bool> RemoveRefreshTokenAsync();

        Task<Guid?> GetDeviceIdAsync();
    }
}