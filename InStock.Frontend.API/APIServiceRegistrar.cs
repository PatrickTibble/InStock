using Refit;

namespace InStock.Frontend.API
{
	public class APIServiceRegistrar
	{
        public T GetService<T>(HttpClient client) => RestService.For<T>(client);
    }
}

