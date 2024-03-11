using Microsoft.EntityFrameworkCore;
using WebApplication1.Entity;
using WebApplication1.Interface;

namespace WebApplication1.Repository
{
    public class EFRepository<T> : IRepository<T> where T : BaseEntity
    {
        protected ApplicationDbContext _context;

        protected DbSet<T> _dbSet;

        public EFRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public void Add(T entity)
        {
            _dbSet.Add(entity);
            _context.SaveChanges();
        }

        public void Delete(Guid id)
        {
            _dbSet.Remove(GetById(id));
            _context.SaveChanges();
        }

        public void Edit(T entity)
        {
            _dbSet.Update(entity);
            _context.SaveChanges();
        }

        public IList<T> GetAll()
        {
            return _dbSet.ToList();
        }

        public T GetById(Guid id)
        {
            return _dbSet.FirstOrDefault(t => t.Id == id);
        }
    }
}
