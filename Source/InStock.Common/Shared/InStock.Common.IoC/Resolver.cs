namespace InStock.Common.IoC
{
	public static class Resolver
	{
        private static IDependencyContainer? _container;
        public static IDependencyContainer Container
        {
            get
            {
                if (_container == null)
                {
                    _container = new TinyDependencyContainer();
                }
                return _container;
            }
        }

        public static void SetResolver(IDependencyContainer container)
            => _container = container;

        public static T Resolve<T>()
            where T : class
            => Container.Resolve<T>();
	}
}

