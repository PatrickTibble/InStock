namespace InStock.Common.IoC
{
	public interface IDependencyContainer
	{
        bool CanResolve<T>()
            where T : class;

        T Construct<T>()
            where T : class;

        void Register<T>(T implementation)
            where T : class;

        void Register<TInterface, TImplementation>()
            where TInterface : class
            where TImplementation : class, TInterface;

        T Resolve<T>()
            where T : class;
    }
}

