using Microsoft.Extensions.DependencyInjection;

namespace InStock.Common.IoC
{
	public static class Resolver
	{
        private static IServiceHelper? _container;
        public static IServiceHelper Container => _container ?? throw new InvalidOperationException("Resolver has not been initialized.");

        public static void SetServiceHelper(IServiceHelper container)
            => _container = container;

        public static T Resolve<T>()
            where T : class
            => Container.Provider.GetService<T>() ?? throw new InvalidOperationException($"Unable to resolve type {typeof(T).FullName}");
	}
}

