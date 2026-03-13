using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using TaskManager.Data;
using TaskManager.Entities;
using TaskManager.Models.Auth;

namespace TaskManager.Services.Auth
{
    public class AuthService
    {
        private readonly IJwtService _jwtService;
        private readonly IPasswordHasher _passwordHasher;
        private readonly AuthDbContext _context;

        public AuthService(
            IJwtService jwtService, 
            IPasswordHasher passwordHasher, 
            AuthDbContext context)
        {
            _jwtService = jwtService;   
            _passwordHasher = passwordHasher;
            _context = context;
        }

        public async Task<AuthResponse> Register(RegisterModel model)
        {
            var userNameCheck = _context.Users.AnyAsync(x => x.UserName == model.UserName);
            var emailCheck = _context.Users.AnyAsync(x => x.Email == model.Email);

            await Task.WhenAll(userNameCheck, emailCheck);

            bool userNameExists = await userNameCheck;
            bool emailExists = await emailCheck;

            if (userNameExists && emailExists)
                throw new ArgumentException("User with this username and email already exists");
            if (emailExists)
                throw new ArgumentException("User with this email already exists");
            if (userNameExists)
                throw new ArgumentException("User with this username already exists");

            var passwordHash = _passwordHasher.HashPassword(model.Password);

            UserItem user = new UserItem
            {
                UserName = model.UserName,
                PasswordHash = passwordHash,
                Email = model.Email,
                RoleId = 1
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            var accessToken = _jwtService.GenerateAccessToken(user);
            var refreshToken = _jwtService.GenerateRefreshToken();

            var rt = new RefreshTokenItem
            {
                Token = refreshToken,
                UserId = user.Id,
                ExpiresAt = DateTime.UtcNow.AddDays(7)
            };

            _context.RefreshTokens.Add(rt);
            await _context.SaveChangesAsync();

            AuthResponse auth = new AuthResponse
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };

            return auth;
        }

        public async Task<AuthResponse> Login(LoginModel model)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == model.Email);

            if (user is null)
                throw new ArgumentException("No user found with this email");

            var verify = _passwordHasher.VerifyPassword(model.Password, user.PasswordHash);

            if (!verify)
                throw new ArgumentException("Invalid password");

            var aсcessToken = _jwtService.GenerateAccessToken(user);
            var refreshToken = _jwtService.GenerateRefreshToken();

            AuthResponse auth = new AuthResponse
            {
                AccessToken = aсcessToken,
                RefreshToken = refreshToken
            };

            var rt = new RefreshTokenItem
            {
                Token = refreshToken,
                UserId = user.Id,
                ExpiresAt = DateTime.UtcNow.AddDays(7)
            };

            _context.RefreshTokens.Add(rt);
            await _context.SaveChangesAsync();

            return auth;
        }
    }
}
