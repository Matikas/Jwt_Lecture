using Jwt.Models;

namespace Jwt.Services
{
    public interface IAuthService
    {
        Task<bool> LoginAsync(string username, string password);
        Task<Account> SignupNewAccountAsync(string username, string password);
    }
}
