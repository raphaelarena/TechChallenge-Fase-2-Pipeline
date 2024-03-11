using Microsoft.EntityFrameworkCore;
using WebApplication1.Entity;
using WebApplication1.Interface;

namespace WebApplication1.Repository
{
    public class EFUserRepository : EFRepository<User>, IUserRepository
    {
        public EFUserRepository(ApplicationDbContext context) : base(context)
        {
                
        }


        public User GetUserByNameAndPassword(string userName, string password)
        {
            return _context.User.FirstOrDefault(u => u.UserName == userName && u.Password == password);
        }
    }
}
