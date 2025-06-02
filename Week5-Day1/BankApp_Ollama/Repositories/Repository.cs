using BankApp.Contexts;
using BankApp.Interfaces;

namespace BankApp.Repositories
{
    public  abstract class Repository<K, T> : IRepository<K, T> where T:class
    {
        protected readonly BankContext _bankContext;

        public Repository(BankContext bankContext)
        {
            _bankContext = bankContext;
        }
        public async Task<T> Add(T item)
        {
            _bankContext.Add(item);
            await _bankContext.SaveChangesAsync();//generate and execute the DML quries for the objects whse state is in ['added','modified','deleted'],
            return item;
        }

        public async Task<T> Delete(K key)
        {
            var item = await Get(key);
            if (item != null)
            {
                _bankContext.Remove(item);
                await _bankContext.SaveChangesAsync();
                return item;
            }
            throw new Exception("No such item found for deleting");
        }

        public abstract Task<T> Get(K key);


        public abstract Task<IEnumerable<T>> GetAll();


        public async Task<T> Update(K key, T item)
        {
            var myItem = await Get(key);
            if (myItem != null)
            {
                _bankContext.Entry(myItem).CurrentValues.SetValues(item);
                await _bankContext.SaveChangesAsync();
                return item;
            }
            throw new Exception("No such item found for updation");
        }
    }
}