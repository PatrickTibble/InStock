using InStock.Common.IoC;
using InStock.Frontend.Abstraction.Services.Navigation;

namespace InStock.Fontend.Mobile.Services.Navigation
{
    public class PageModelLocator : ILocator<Page>
    {
        private readonly IDictionary<Type, Type> _lookupTable;
        private readonly IDependencyContainer _container;

        public PageModelLocator(IDependencyContainer container)
        {
            _lookupTable = new Dictionary<Type, Type>();
            _container = container;
        }

        Page ILocator<Page>.CreatePageFor<TPageModel>()
        {
            if (_lookupTable.ContainsKey(typeof(TPageModel))
                && Activator.CreateInstance(_lookupTable[typeof(TPageModel)]) is Page page)
            {
                var viewModel = _container.Resolve<TPageModel>();
                page.Appearing += viewModel.Appearing;
                page.Disappearing += viewModel.Disappearing;

                page.BindingContext = viewModel;
                return page;
            }

            throw new ArgumentException($"Unable to find registration for type {typeof(TPageModel).FullName}");
        }

        void ILocator<Page>.RegisterPageAndPageModel<TPage, TPageModel>()
        {
            if (_lookupTable.ContainsKey(typeof(TPageModel)))
            {
                _lookupTable[typeof(TPageModel)] = typeof(TPage);
            }
            _lookupTable.Add(typeof(TPageModel), typeof(TPage));
        }
    }
}

