namespace InStock.Common.Abstraction.Repositories.Base
{
	public interface IRepository<TModelType, TKey> where TModelType : class, IIdentifiable<TKey> where TKey : IComparable
	{
		TModelType? Get(TKey id);
		void Add(TModelType item);
		void Update(TModelType item);
		void Delete(TModelType item);
	}
}