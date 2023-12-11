using InStock.Frontend.Mobile.Views.Cards;
using InStock.Frontend.Mobile.Views.Input;
using InStock.Frontend.Mobile.Views.ListItems;
using InStock.Frontend.Mobile.Views.Scrolls;
using InStock.Frontend.Core.ViewModels.Cards;
using InStock.Frontend.Core.ViewModels.Collections;
using InStock.Frontend.Core.ViewModels.Input;
using InStock.Frontend.Core.ViewModels.ListItems;

namespace InStock.Frontend.Mobile.TemplateSelectors
{
    public class CollectionViewItemTemplateSelector : MappableTemplateSelector
	{
		public CollectionViewItemTemplateSelector()
		{
			TemplateMap = new Dictionary<Type, DataTemplate>
			{
				{ typeof(MenuItemViewModel), new DataTemplate(typeof(MenuItemView)) },
				{ typeof(SearchBarViewModel), new DataTemplate(typeof(SearchBarView)) },
				{ typeof(CollectionViewModel<LocationCardViewModel>), new DataTemplate(typeof(HorizontalScrollView)) },
                { typeof(ChartViewModel), new DataTemplate(typeof(ChartCardView)) },
				{ typeof(LocationCardViewModel), new DataTemplate(typeof(LocationCardView)) }
			};
		}
	}
}