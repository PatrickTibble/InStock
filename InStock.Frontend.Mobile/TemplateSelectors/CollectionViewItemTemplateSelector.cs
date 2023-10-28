using InStock.Fontend.Mobile.Views.ListItems;
using InStock.Frontend.Core.ViewModels.ListItems;

namespace InStock.Fontend.Mobile.TemplateSelectors
{
    public class CollectionViewItemTemplateSelector : MappableTemplateSelector
	{
		public CollectionViewItemTemplateSelector()
		{
			TemplateMap = new Dictionary<Type, DataTemplate>
			{
				{ typeof(MenuItemViewModel), new DataTemplate(typeof(MenuItemView)) }
			};
		}
	}
}

