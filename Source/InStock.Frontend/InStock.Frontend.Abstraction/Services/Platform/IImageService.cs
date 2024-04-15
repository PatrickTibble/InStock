using InStock.Frontend.Abstraction.Enums;

namespace InStock.Frontend.Core.Services.Platform
{
    public interface IImageService
    {
        string GetImage(Images imageEnum);
    }
}