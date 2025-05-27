using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FirstApi.Exceptions;
using FirstApi.Models;
using FirstApi.Interfaces;
using FirstApi.Repositories;

namespace FirstApi.Repositories
{
    public abstract class Repository<K, T> : IRepository<K, T> where T : class
    {
        protected List<T> _items = new List<T>();
        public abstract T GetById(K id);
        public abstract ICollection<T> GetAll();
        public abstract K GenerateId();

        public T Add(T item)
        {
            var id = GenerateId();
            var property = typeof(T).GetProperty("Id");
            if (property != null)
            {
                property.SetValue(item, id);
            }
            if (_items.Contains(item))
            {
                throw new DuplicateEntityException("Item already exists");
            }
            _items.Add(item);
            return item;
        }

        public T Delete(K id)
        {
            var item = GetById(id);
            if (item == null)
            {
                throw new KeyNotFoundException("Item not found");
            }
            _items.Remove(item);
            return item;
        }

        public T Update(T item)
        {
            var myItem = _items.FirstOrDefault(i => i.Equals(item));

            if (myItem == null)
            {
                throw new KeyNotFoundException("Item not found");
            }
            var index = _items.IndexOf(myItem);
            _items[index] = item;
            return item;
        }
    }
}