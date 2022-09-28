using Jwt.Models;
using Microsoft.EntityFrameworkCore;

namespace Jwt.Database
{
    public class JwtDbContext : DbContext
    {
        public DbSet<Account> Accounts => Set<Account>();

        public JwtDbContext(DbContextOptions options) : base(options)
        {

        }
    }
}
