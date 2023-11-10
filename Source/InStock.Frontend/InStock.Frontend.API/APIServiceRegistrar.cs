using Refit;

namespace InStock.Frontend.API
{
	public class APIServiceRegistrar : IServiceRegistrar
	{
        public T GetService<T>(HttpClient client)
#if USE_MOCKS
            => MockService.For<T>();
#else
            => RestService.For<T>(client);
#endif
    }
}