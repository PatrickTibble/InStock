using System;
namespace InStock.Common.IoC
{
	public class TinyDependencyContainer : IDependencyContainer
	{
		private TinyIoC.TinyIoCContainer _container;
		public TinyDependencyContainer()
		{
			_container = new TinyIoC.TinyIoCContainer();
		}

		public bool CanResolve<T>()
			where T : class
			=> _container.CanResolve<T>();

		public T Construct<T>()
			where T : class
		{
			if (_container.CanResolve<T>())
			{
				return _container.Resolve<T>();
			}

			_container.Register<T>();
			var constructed = _container.Resolve<T>();
			_container.Unregister<T>();
			return constructed;
		}

        public void Register<T>(T implementation)
            where T : class
            => _container.Register(implementation);

        public void Register<TInterface, TImplementation>()
            where TInterface : class
            where TImplementation : class, TInterface
            => _container.Register<TInterface, TImplementation>();

        public T Resolve<T>()
            where T : class
            => _container.Resolve<T>();

    }
}

