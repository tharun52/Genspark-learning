using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using AppointmentApplication.Exception;
using AppointmentApplication.Exceptions;
using AppointmentApplication.Interfaces;

namespace AppointmentApplication.Repositories
{
    public abstract class Repository<K, T> : IRepository<K, T> where T : class
    {
        protected List<T> _items = new List<T>();

        protected abstract K GenerateId();

        public abstract ICollection<T> GetAll();

        public abstract T GetById(K id);


        public T Add(T item)
        {
            var id = GenerateId();
            var property = typeof(T).GetProperty("Id");
            if (property != null)
            {
                property.SetValue(item, id);
            }
            if(_items.Contains(item))
            {
                throw new DuplicateEntityException($"Entity with id {id} already exists.");
            }
            _items.Add(item);
            return item;
        }
    }
}
