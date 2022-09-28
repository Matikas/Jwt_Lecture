namespace Jwt.Services
{
    public interface IJwtService
    {
        string GetJwtToken(string username);
    }
}
