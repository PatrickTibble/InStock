using InStock.Common.Abstraction.Services.Logger;
using InStock.Common.IoC;

namespace InStock.Frontend.Core.Managers.Base
{
    public abstract class BaseManager
    {
        protected readonly ILogger Logger;

        protected BaseManager()
            : this(Resolver.Resolve<ILogger>())
        {

        }

        protected BaseManager(ILogger logger)
        {
            Logger = logger;
        }
    }
}
