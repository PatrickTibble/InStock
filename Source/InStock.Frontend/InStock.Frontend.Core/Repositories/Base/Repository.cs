using InStock.Common.Abstraction.Repositories.Base;
using InStock.Frontend.Abstraction.Models;

namespace InStock.Frontend.Core.Repositories.Base
{
    public class Repository<TModel> : IRepository<TModel> where TModel : class, IIdentifiable
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

        public TModel? Get(int id)
        {
            if (_items.Find(i => i.Id.CompareTo(id) == 0) is TModel item)
            {
                return item;
            }
            return default;
        }

        public IEnumerable<TModel> GetAll()
        {
            return _items;
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