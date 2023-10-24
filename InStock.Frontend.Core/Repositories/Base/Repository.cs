using InStock.Common.Abstraction.Repositories.Base;

namespace InStock.Frontend.Core.Repositories.Base
{
    public class Repository<TModel, TKey> : IRepository<TModel, TKey> where TModel : class, IIdentifiable<TKey> where TKey : IComparable
    {
        private readonly List<TModel> _items;

        public Repository()
        {
            _items = new List<TModel>();
        }

        public void Add(TModel item)
        {
            if (!_items.Contains(item))
            {
                _items.Add(item);
            }
        }

        public void Delete(TModel item)
        {
            if (_items.Contains(item))
            {
                _items.Remove(item);
            }
        }

        public TModel? Get(TKey id)
        {
            if (_items.Find(i => i.Id.CompareTo(id) == 0) is TModel item)
            {
                return item;
            }
            return default;
        }

        public void Update(TModel item)
        {
            if (_items.Find(i => i.Id.CompareTo(item.Id) == 0) is TModel model)
            {
                var index = _items.IndexOf(model);
                _items.Remove(model);
                _items.Insert(index, item);
            }
        }
    }
}