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
                Images.ChevronIcon => "chevron.png",
                Images.DashboardIcon => "home.png",
                Images.InventoryIcon => "inventory.png",
                Images.LocationsIcon => "location.png",
                Images.LockIcon => "lock.png",
                Images.MenuIcon => "paragraph.png",
                Images.OptionIcon => "option.png",
                Images.ScannerIcon => "checklist.png",
                Images.SearchIcon => "find_gradient.png",
                Images.UserIcon => "user.png",
                _ => string.Empty
            };
        }
    }
}