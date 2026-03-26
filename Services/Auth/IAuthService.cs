using TaskManager.Models.Auth;

namespace TaskManager.Services.Auth
{
    public interface IAuthService
    {
        Task<AuthResponse> Login(LoginModel model);
        Task Logout(int userId);
        Task<AuthResponse> Refresh(string refreshToken);
        Task<AuthResponse> Register(RegisterModel model);
    }
}