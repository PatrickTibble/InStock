namespace InStock.Frontend.API
{
	public interface IServiceRegistrar
    {
        T GetService<T>(HttpClient client);
    }
}