using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Biggy
{
    public interface IBiggyStore<T>
    {
        List<T> Load();
        void SaveAll(List<T> items);
        void Clear();     
        T Add(T item);
		List<T> Add(List<T> items);

		Task<List<T>> LoadAsync();
		void SaveAllAsync(List<T> items);
		Task<bool> ClearAsync();     
		Task<T> AddAsync(T item);
		Task<List<T>> AddAsync(List<T> items);
    }

    public interface IUpdateableBiggyStore<T> : IBiggyStore<T>
    {
        T Update(T item);
        T Remove(T item);
        List<T> Remove(List<T> items);

		Task<T> UpdateAsync(T item);
		Task<T> RemoveAsync(T item);
		Task<List<T>> RemoveAsync(List<T> items);
    }

    public interface IQueryableBiggyStore<T> : IBiggyStore<T>
    {
        IQueryable<T> AsQueryable();
    }
}