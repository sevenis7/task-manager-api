using System.Security.Claims;
using TaskManager.Entities;

namespace TaskManager.Services.Auth
{
    public interface IJwtService
    {
        string GenerateAccessToken(UserItem user);
        string GenerateRefreshToken();
        ClaimsPrincipal? ValidateAccessToken(string token);
    }
}
