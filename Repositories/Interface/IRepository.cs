namespace API.Repositories.Interface;

public interface IRepository<Key, Entity> where Entity: class
{
    Task<IEnumerable<Entity>> GetAll(); //--> task menandakan asynchronous, IEnumerable hanya bisa read/view tdk bisa menambahkan data (GetAll)
    Task<Entity?> GetById(Key key);
    Task<int> Insert(Entity entity);
    Task<int> Update(Entity entity);
    Task<int> Delete(Key key);
}
