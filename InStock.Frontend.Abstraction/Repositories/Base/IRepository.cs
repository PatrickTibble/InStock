using InStock.Frontend.Abstraction.Models;

namespace InStock.Common.Abstraction.Repositories.Base
{
    public interface IRepository<TModelType> : IRepository<TModelType, int> where TModelType : class, IIdentifiable<int>
	{

	}

	public interface IRepository<TModelType, TKey> where TModelType : class, IIdentifiable<TKey> where TKey : IComparable
	{
		TModelType? Get(TKey id);
		IEnumerable<TModelType> GetAll();
		void Add(TModelType item);
		void Update(TModelType item);
		void Delete(TModelType item);
	}
}