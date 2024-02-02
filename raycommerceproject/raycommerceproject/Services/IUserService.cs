using raycommerceproject.Models;

namespace raycommerceproject.Services
{
    public interface IUserService
    {
        void RegisterUser(User user);
        User AuthenticateUser(string email, string password);

        string GenerateJwtToken(User user);
    }
}
