using System.Security.Cryptography;

namespace Business.Handlers
{
    public class PasswordHashHandler
    {
        private static int _iterationCount = 100000;
        private static int _saltSize = 16;
        private static int _hashSize = 32;
        private static RandomNumberGenerator _randomNumberGenerator = RandomNumberGenerator.Create();

        public static string HashPassword(string password)
        {
            var salt = new byte[_saltSize];
            _randomNumberGenerator.GetBytes(salt);
            using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, _iterationCount, HashAlgorithmName.SHA256))
            {
                byte[] hash = pbkdf2.GetBytes(_hashSize);

                byte[] hashBytes = new byte[_saltSize + _hashSize];
                Array.Copy(salt, 0, hashBytes, 0, _saltSize);
                Array.Copy(hash, 0, hashBytes, _saltSize, _hashSize);

                return Convert.ToBase64String(hashBytes);
            }
        }
        public static bool VerifyPassword(string password, string hashedPassword)
        {
            byte[] hashBytes = Convert.FromBase64String(hashedPassword);
            byte[] salt = new byte[_saltSize];
            Array.Copy(hashBytes, 0, salt, 0, _saltSize);

            using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, _iterationCount, HashAlgorithmName.SHA256))
            {
                byte[] hash = pbkdf2.GetBytes(_hashSize);

                for (int i = 0; i < _hashSize; i++)
                {
                    if (hashBytes[i + _saltSize] != hash[i])
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}