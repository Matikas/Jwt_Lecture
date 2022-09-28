using Jwt.Models;

namespace Jwt.Database
{
    public interface IDbRepository
    {
        void Add(Account account);
        Task CommitAsync();
        Task<Account> GetAccountAsync(string username);
    }
}
