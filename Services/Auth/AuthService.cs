using Microsoft.EntityFrameworkCore;
using TaskManager.Data;
using TaskManager.Entities;
using TaskManager.Exceptions;
using TaskManager.Models.Auth;

namespace TaskManager.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly IJwtService _jwtService;
        private readonly IPasswordHasher _passwordHasher;
        private readonly AuthDbContext _context;
        private readonly ILogger<AuthService> _logger;

        public AuthService(
            IJwtService jwtService,
            IPasswordHasher passwordHasher,
            AuthDbContext context,
            ILogger<AuthService> logger)
        {
            _jwtService = jwtService;
            _passwordHasher = passwordHasher;
            _context = context;
            _logger = logger;
        }

        public async Task<AuthResponse> Register(RegisterModel model)
        {
            var userNameCheck = await _context.Users.AnyAsync(x => x.UserName == model.UserName);
            var emailCheck = await _context.Users.AnyAsync(x => x.Email == model.Email);

            if (userNameCheck && emailCheck)
                throw new ConflictException("User with this username and email already exists");
            if (emailCheck)
                throw new ConflictException("User with this email already exists");
            if (userNameCheck)
                throw new ConflictException("User with this username already exists");

            var passwordHash = _passwordHasher.HashPassword(model.Password);

            var userRole = await _context.Roles.FirstAsync(x => x.Name == "User");

            UserItem user = new UserItem
            {
                UserName = model.UserName,
                PasswordHash = passwordHash,
                Email = model.Email,
                RoleId = userRole.Id
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            user = _context.Users
                .Include(x => x.Role)
                .FirstOrDefault(x => x.Id == user.Id)!;


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

            _logger.LogInformation("User {userid} registered", user.Id);

            return auth;
        }

        public async Task<AuthResponse> Login(LoginModel model)
        {
            var user = await _context.Users
                .Include(x => x.Role)
                .FirstOrDefaultAsync(x => x.Email == model.Email);

            if (user is null)
            {
                _logger.LogWarning("Failed login attempt: user with email {Email} not found", model.Email);
                throw new UnauthorizedException("No user found with this email");
            }    

            var verify = _passwordHasher.VerifyPassword(model.Password!, user.PasswordHash);

            if (!verify)
            {
                _logger.LogWarning("Failed login attempt: invalid password for user {userId}", user.Id);
                throw new UnauthorizedException("Invalid password");
            }

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

            _logger.LogInformation("User {userId} logged in", user.Id);

            return auth;
        }

        public async Task Logout(int userId)
        {
            var user = await _context.Users.FindAsync(userId);

            if (user is null)
            {
                _logger.LogWarning("Logout failed: user {userId} not found", userId);
                throw new UnauthorizedException("Invalid user");
            }

            var refreshTokens = await _context.RefreshTokens.Where(x => x.UserId == userId).ToListAsync();

            _context.RefreshTokens.RemoveRange(refreshTokens);
            await _context.SaveChangesAsync();

            _logger.LogInformation("User {userId} logged out", userId);
        }

        public async Task<AuthResponse> Refresh(string refreshToken)
        {
            var existedRefreshToken = await _context.RefreshTokens.FirstOrDefaultAsync(x => x.Token == refreshToken);

            if (existedRefreshToken is null)
            {
                _logger.LogWarning("Failed refresh attempt: invalid refresh token");
                throw new ArgumentException("Invalid refresh token");
            }    

            if (existedRefreshToken.ExpiresAt < DateTime.UtcNow)
            {
                _logger.LogWarning("Failed refresh attempt: refresh token expired");
                throw new ArgumentException("Refresh token expired");
            }

            var existedUser = await _context.Users
                .Include(x => x.Role)
                .FirstOrDefaultAsync(x => x.Id == existedRefreshToken.UserId);

            if (existedUser is null)
            {
                _logger.LogWarning("Failed refresh attempt: invalid user");
                throw new ArgumentException("Invalid user");
            }    

            _context.RefreshTokens.Remove(existedRefreshToken);
            await _context.SaveChangesAsync();

            AuthResponse auth = new AuthResponse
            {
                AccessToken = _jwtService.GenerateAccessToken(existedUser),
                RefreshToken = _jwtService.GenerateRefreshToken()
            };

            RefreshTokenItem newRefreshToken = new RefreshTokenItem
            {
                Token = auth.RefreshToken,
                UserId = existedUser.Id,
                ExpiresAt = DateTime.UtcNow.AddDays(7)
            };

            _context.RefreshTokens.Add(newRefreshToken);
            await _context.SaveChangesAsync();

            _logger.LogInformation("User {userId} refreshed tokens", existedUser.Id);

            return auth;
        }
    }
}
