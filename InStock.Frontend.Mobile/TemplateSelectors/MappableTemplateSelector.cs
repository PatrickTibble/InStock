using System;
namespace InStock.Frontend.Mobile.TemplateSelectors
{
    public class MappableTemplateSelector : DataTemplateSelector
	{
		public IDictionary<Type, DataTemplate> TemplateMap { get; set; }

		public MappableTemplateSelector()
		{
		}

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            if (TemplateMap?.ContainsKey(item.GetType()) ?? false)
            {
                return TemplateMap[item.GetType()];
            }

            throw new ArgumentException($"Unable to find DataTemplate for type {item.GetType().FullName}");
        }
    }
}

