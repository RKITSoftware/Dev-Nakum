using System.Security.Cryptography;

namespace SocialMediaAPI.BL
{
    /// <summary>
    /// Business Logic class for hashing related operations.
    /// </summary>
    public class BLHashing
    {
        #region Private Member
        private readonly int _saltSize = 32;
        private readonly int _iteration = 10000;
        private readonly int _keySize = 32;
        #endregion

        #region Private Method

        /// <summary>
        /// Generates a random salt for password hashing.
        /// </summary>
        /// <returns>Randomly generated salt.</returns>
        private byte[] GenerateSalt()
        {
            using (var rng = new RNGCryptoServiceProvider())
            {
                byte[] salt = new byte[_saltSize];
                rng.GetBytes(salt);
                return salt;
            }
        }

        /// <summary>
        /// Hashes the password using the provided salt and iterations.
        /// </summary>
        /// <param name="password">Password to be hashed.</param>
        /// <param name="salt">Salt used in hashing.</param>
        /// <returns>Hashed password.</returns>
        private string HashPassword(string password, byte[] salt)
        {
            using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, _iteration))
            {
                byte[] hash = pbkdf2.GetBytes(_keySize);
                return Convert.ToBase64String(salt) + ":" + Convert.ToBase64String(hash);
            }
        }

        /// <summary>
        /// Verifies if the provided password matches the stored hashed password.
        /// </summary>
        /// <param name="password">Password to be verified.</param>
        /// <param name="storedHash">Stored hashed password.</param>
        /// <returns>True if the password is verified, false otherwise.</returns>
        private bool VerifyPassword(string password, string storedHash)
        {
            string[] parts = storedHash.Split(':');
            if (parts.Length != 2)
            {
                return false;
            }

            byte[] salt = Convert.FromBase64String(parts[0]);
            byte[] storedHashByte = Convert.FromBase64String(parts[1]);

            string newHash = HashPassword(password, salt);

            return storedHash.Equals(newHash);
        }

        #endregion

        #region Public Method

        /// <summary>
        /// Hashes the provided password and generates a random salt.
        /// </summary>
        /// <param name="password">Password to be hashed.</param>
        /// <returns>Hashed password with salt.</returns>
        public string HashPassword(string password)
        {
            byte[] salt = GenerateSalt();
            return HashPassword(password, salt);
        }

        /// <summary>
        /// Verifies if the provided password matches the stored hashed password.
        /// </summary>
        /// <param name="password">Password to be verified.</param>
        /// <param name="storedHash">Stored hashed password.</param>
        /// <returns>True if the password is verified, false otherwise.</returns>
        public bool Verify(string password, string storedHash)
        {
            return VerifyPassword(password, storedHash);
        }

        #endregion
    }
}
