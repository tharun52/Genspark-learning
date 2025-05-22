namespace WholeApplication.Repositories
{
public class UpdateDeleteRepository<K, T> : IUpdateDeleteRepository<K, T>
{

    protected List<T> _items = new List<T>();

    public abstract T GetById(K id);

    public T Update(T item)
    {
        //var myItem = GetById((K)item.GetType().GetProperty("Id").GetValue(item));
        var myItem = _items.FirstOrDefault(i => i.Equals(item));

        if (myItem == null)
        {
            throw new KeyNotFoundException("Item not found");
        }
        var index = _items.IndexOf(myItem);
        _items[index] = item;
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

}
