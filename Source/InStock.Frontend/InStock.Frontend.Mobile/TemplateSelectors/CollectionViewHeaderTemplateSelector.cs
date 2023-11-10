using InStock.Frontend.Mobile.Views.Headers;
using InStock.Frontend.Core.ViewModels.Headers;

namespace InStock.Frontend.Mobile.TemplateSelectors
{
    public class CollectionViewHeaderTemplateSelector : MappableTemplateSelector
    {
        public CollectionViewHeaderTemplateSelector()
        {
            TemplateMap = new Dictionary<Type, DataTemplate>()
            {
                { typeof(MainPageHeaderViewModel), new DataTemplate(typeof(MainPageHeaderView)) },
                { typeof(PrimaryHeaderViewModel), new DataTemplate(typeof(PrimaryHeaderView)) }
            };
        }
    }
}