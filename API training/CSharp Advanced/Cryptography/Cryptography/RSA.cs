using System;
using System.Security.Cryptography;
using System.Text;

namespace Cryptography
{
    /// <summary>
    /// encrypt and decrypt the plain text
    /// </summary>
    public class RSA
    {
        #region Private Member

        /// <summary>
        /// create the object of the RSACryptoServiceProvider
        /// </summary>
        private static RSACryptoServiceProvider _objRSA;
        #endregion

        #region Constructor

        /// <summary>
        /// initialize the object of the RSACryptoServiceProvider
        /// </summary>
        public RSA()
        {
            _objRSA = new RSACryptoServiceProvider();
        }
        #endregion

        #region Public Method
        
        /// <summary>
        /// Encryption of plain text
        /// </summary>
        /// <param name="plainText">user's plain text</param>
        /// <returns>encrypted message</returns>
        public string Encrypt(string plainText)
        {
            byte[] plainBytes = Encoding.UTF8.GetBytes(plainText);
            byte[] encryptedByte = _objRSA.Encrypt(plainBytes, true);

            return Convert.ToBase64String(encryptedByte);                                  
        }

        /// <summary>
        /// Decryption of encrypted text
        /// </summary>
        /// <param name="ecncryptedString">encrypted string</param>
        /// <returns>original plain text</returns>
        public string Decrypt(string encryptedString)
        {
            byte[] encryptedTextByte = Convert.FromBase64String(encryptedString);
            byte[] descryptedBytes = _objRSA.Decrypt(encryptedTextByte, true);

            return Encoding.UTF8.GetString(descryptedBytes);
        }
        #endregion
    }
}
