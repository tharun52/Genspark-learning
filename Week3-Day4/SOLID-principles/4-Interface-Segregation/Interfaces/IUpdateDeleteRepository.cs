public interface IUpdateDeleteRepository<K, T> where T : class
{
    T Update(T item);
    T Delete(K id);
    T GetById(K id);
}
