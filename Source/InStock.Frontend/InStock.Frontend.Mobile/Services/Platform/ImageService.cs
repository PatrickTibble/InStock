using InStock.Frontend.Abstraction.Enums;
using InStock.Frontend.Core.Services.Platform;

namespace InStock.Frontend.Mobile.Services.Platform
{
    public class ImageService : IImageService
    {
        public string GetImage(Images imageEnum)
        {
            return imageEnum switch
            {
                Images.Chevron => "chevron.png",
                Images.Dashboard => "home.png",
                Images.Inventory => "inventory.png",
                Images.Locations => "location.png",
                Images.Lock => "lock.png",
                Images.Menu => "paragraph.png",
                Images.Option => "option.png",
                Images.Scanner => "checklist.png",
                Images.Search => "find_gradient.png",
                Images.User => "user.png",
                _ => string.Empty
            };
        }
    }
}