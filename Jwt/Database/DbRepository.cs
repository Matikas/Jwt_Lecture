using Jwt.Models;
using Microsoft.EntityFrameworkCore;

namespace Jwt.Database
{
    public class DbRepository : IDbRepository
    {
        private readonly JwtDbContext _context;

        public DbRepository(JwtDbContext context)
        {
            _context = context;
        }

        public void Add(Account account)
        {
            _context.Add(account);
        }

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<Account> GetAccountAsync(string username)
        {
            return await _context.Accounts.FirstOrDefaultAsync(a => a.Username == username);
        }
    }
}
