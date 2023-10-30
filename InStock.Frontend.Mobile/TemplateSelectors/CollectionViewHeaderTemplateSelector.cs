using InStock.Fontend.Mobile.Views.Headers;
using InStock.Frontend.Core.ViewModels.Headers;

namespace InStock.Fontend.Mobile.TemplateSelectors
{
    public class CollectionViewHeaderTemplateSelector : MappableTemplateSelector
    {
        public CollectionViewHeaderTemplateSelector()
        {
            TemplateMap = new Dictionary<Type, DataTemplate>()
            {
                { typeof(MainPageHeaderViewModel), new DataTemplate(typeof(MainPageHeaderView)) }
            };
        }
    }
}