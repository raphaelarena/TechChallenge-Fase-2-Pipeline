using WebApplication1.Entity;

namespace WebApplication1.Services
{
    public interface ITokenService
    {
        string GetToken(User user);
    }
}
