namespace FirstApi.Interfaces
{
    public interface IRepository<K, T> where T : class
    {
        T GetById(K id);
        T Add(T item);
        T Update(T item);
        T Delete(K item);
        ICollection<T> GetAll();
    }
}