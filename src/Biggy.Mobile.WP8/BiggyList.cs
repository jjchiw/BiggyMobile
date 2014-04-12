using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Biggy
{
    public class BiggyList<T> : IBiggy<T> where T : new()
    {
        private readonly IBiggyStore<T> _store;
        private readonly IQueryableBiggyStore<T> _queryableStore;
        private readonly IUpdateableBiggyStore<T> _updateableBiggyStore;
        private List<T> _items;

        public  BiggyList(IBiggyStore<T> store)
        {
            _store = store;
            _queryableStore = _store as IQueryableBiggyStore<T>;
            _updateableBiggyStore = _store as IUpdateableBiggyStore<T>;
        }

        public async Task<bool> LoadItemsAsync()
        {
            _items = await _store.LoadAsync();
            return true;
        }

        public bool LoadItems()
        {
            throw new NotImplementedException();
        }

        public virtual IEnumerator<T> GetEnumerator()
        {
            return _items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public virtual void Clear()
        {
            Task.Factory.StartNew(async () =>
            {
                await _store.ClearAsync();
            });
            _items.Clear();
            Fire(Changed, items: null);
        }

        public virtual int Count()
        {
            return _items.Count;
        }

        public virtual T Update(T item)
        {
            if (_updateableBiggyStore != null)
            {
                Task.Factory.StartNew(async () =>
                {
                    await _updateableBiggyStore.UpdateAsync(item);
                });
            }
            else
            {
                _store.SaveAllAsync(_items);
            }
            Fire(Changed, item: item);
            return item;
        }

        public virtual T Remove(T item)
        {
            _items.Remove(item);
            if (_updateableBiggyStore != null)
            { 
                Task.Factory.StartNew(async () =>
                {
                    await _updateableBiggyStore.RemoveAsync(item);
                });
            }
            else
            {
                _store.SaveAllAsync(_items);
            }
            return item;
        }


        public List<T> Remove(List<T> items)
        {
            if (_updateableBiggyStore != null)
            {
                Task.Factory.StartNew(async () =>
                {
                    await _updateableBiggyStore.RemoveAsync(items);
                });

                foreach (var item in items)
                {
                    _items.Remove(item);
                }
            }
            else
            {
                throw new InvalidOperationException("You must Implement IUpdatableBiggySotre to call this operation");
            }
            return items;
        }

        public virtual T Add(T item)
        {
            Task.Factory.StartNew(async () =>
            {
                await _store.AddAsync(item);
            });
            
            _items.Add(item);
            Fire(ItemAdded, item: item);
            return item;
        }

        public virtual List<T> Add(List<T> items)
        {
            Task.Factory.StartNew(async () =>
            {
                await _store.AddAsync(items);
            });
            _items.AddRange(items);
            //foreach (var item in items)
            //{
            //    _items.Add(item);
            //}
            Fire(ItemsAdded, items: items);
            return items;
        }

        public virtual IQueryable<T> AsQueryable()
        {
            if (_store is IQueryableBiggyStore<T>)
            {
                return ((IQueryableBiggyStore<T>)_store).AsQueryable();
            }
            return _items.AsQueryable();
        }

        protected virtual void Fire(EventHandler<IBiggyEventArgs<T>> @event, T item = default(T), IList<T> items = null)
        {
            if (@event != null)
            {
                var args = new BiggyEventArgs<T> { Item = item, Items = items };
                @event(this, args);
            }
        }

        public event EventHandler<IBiggyEventArgs<T>> ItemRemoved;
        public event EventHandler<IBiggyEventArgs<T>> ItemAdded;
        public event EventHandler<IBiggyEventArgs<T>> ItemsAdded;

        public event EventHandler<IBiggyEventArgs<T>> Changed;
        public event EventHandler<IBiggyEventArgs<T>> Loaded;
        public event EventHandler<IBiggyEventArgs<T>> Saved;
    }
}