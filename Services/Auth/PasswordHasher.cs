using System.Security.Cryptography;

namespace TaskManager.Services.Auth
{
    public class PasswordHasher : IPasswordHasher
    {
        private const int SaltSize = 16;
        private const int KeySize = 32;
        private const int iterations = 100_000;
        private static readonly HashAlgorithmName HashAlgoritm = HashAlgorithmName.SHA256;
        private const char Delimeter = ';';

        public string HashPassword (string password)
        {
            byte[] salt = RandomNumberGenerator.GetBytes(SaltSize);

            byte[] hash = Rfc2898DeriveBytes.Pbkdf2(
                password,
                salt,
                iterations,
                HashAlgoritm,
                KeySize);

            return $"{Convert.ToBase64String(salt)}{Delimeter}{Convert.ToBase64String(hash)}";
        }

        public bool VerifyPassword (string password, string hashString)
        {
            string[] parts = hashString.Split(Delimeter);
            if (parts.Length != 2)
                return false;

            byte[] salt = Convert.FromBase64String(parts[0]);
            byte[] hash = Convert.FromBase64String(parts[1]);

            byte[] hashToVerify = Rfc2898DeriveBytes.Pbkdf2(
                password,
                salt,
                iterations,
                HashAlgoritm,
                hash.Length);

            return CryptographicOperations.FixedTimeEquals(hash, hashToVerify);
        }
    }
}
