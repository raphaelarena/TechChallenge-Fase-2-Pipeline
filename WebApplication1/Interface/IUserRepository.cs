using WebApplication1.Entity;

namespace WebApplication1.Interface
{
    public interface IUserRepository : IRepository<User>
    {
        User GetUserByNameAndPassword(string userName, string password);
    }
}
