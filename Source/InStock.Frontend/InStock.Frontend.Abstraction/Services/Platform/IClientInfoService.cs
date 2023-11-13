using InStock.Frontend.Abstraction.Models;

namespace InStock.Frontend.Abstraction.Services.Platform
{
    /// <summary>
    /// Provide information about the client.
    /// </summary>
    public interface IClientInfoService
    {
        /// <summary>
        /// Software version of the application.
        /// </summary>
        SoftwareVersion AppVersion { get; }
    }
}