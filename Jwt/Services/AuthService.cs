using Jwt.Database;
using Jwt.Models;
using System.Security.Cryptography;
using System.Text;

namespace Jwt.Services
{
    public class AuthService : IAuthService
    {
        private readonly IDbRepository _repository;

        public AuthService(IDbRepository repository)
        {
            _repository = repository;
        }

        public async Task<(bool authenticationSuccessful, string? role)> LoginAsync(string username, string password)
        {
            var account = await _repository.GetAccountAsync(username);
            if (account == null)
            {
                return (false, null);
            }

            return (VerifyPasswordHash(password, account.PasswordHash, account.PasswordSalt), account.Role);
        }

        public async Task<Account> SignupNewAccountAsync(string username, string password)
        {
            var account = CreateAccount(username, password);

            _repository.Add(account);
            await _repository.CommitAsync();

            return account;
        }

        private Account CreateAccount(string username, string password)
        {
            var (passwordHash, passwordSalt) =  CreatePasswordHash(password);
            return new Account
            {
                Username = username,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Role = "User"
            };
        }

        private (byte[] hash, byte[] salt) CreatePasswordHash(string password)
        {
            using var hmac = new HMACSHA512();
            var salt = hmac.Key;
            var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

            return (hash, salt);
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using var hmac = new HMACSHA512(passwordSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

            return computedHash.SequenceEqual(passwordHash);
        }
    }
}
