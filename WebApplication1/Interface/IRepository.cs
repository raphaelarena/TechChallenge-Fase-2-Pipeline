using WebApplication1.Entity;

namespace WebApplication1.Interface
{
    public interface IRepository<T> where T : BaseEntity
    {
        IList<T> GetAll();
        T GetById(Guid id);
        void Add(T entity);
        void Edit(T entity);
        void Delete(Guid id);
    }
}
