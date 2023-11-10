using InStock.Common.AccountService.Abstraction.Services;
using InStock.Common.InventoryService.Abstraction.Services;
using InStock.Frontend.API.Mocks;

namespace InStock.Frontend.API
{
    internal class MockService
    {
        private static IDictionary<Type, object> _serviceMap = new Dictionary<Type, object>()
        {
            { typeof(IAccountService), new MockAccountService() },
            { typeof(IInventoryService), new MockInventoryService() }
        };

        internal static T For<T>()
        {
            if (_serviceMap.ContainsKey(typeof(T)) && _serviceMap[typeof(T)] is T instance)
            {
                return instance;
            }

            throw new ArgumentException($"Attempted to retrieve service for unregistered or improperly registered " +
                $"type {typeof(T).FullName}. Ensure the type is registered with an Instance that implements the type.");
        }
    }
}