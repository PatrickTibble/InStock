using InStock.Fontend.Mobile.Views.ListItems;
using InStock.Frontend.Core.ViewModels.Headers;
using InStock.Frontend.Core.ViewModels.ListItems;
using InStock.Frontend.Mobile.Views.Headers;

namespace InStock.Fontend.Mobile.TemplateSelectors
{
    public class CollectionViewItemTemplateSelector : MappableTemplateSelector
	{
		public CollectionViewItemTemplateSelector()
		{
			TemplateMap = new Dictionary<Type, DataTemplate>
			{
				{ typeof(MenuItemViewModel), new DataTemplate(typeof(MenuItemView)) },
				{ typeof(PrimaryHeaderViewModel), new DataTemplate(typeof(PrimaryHeaderView)) }
			};
		}
	}
}