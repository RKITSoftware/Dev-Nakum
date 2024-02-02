using System;
using System.Security.Cryptography;
using System.Text;

namespace Cryptography
{
    /// <summary>
    ///     encryption and decryption of the plain text
    /// </summary>
    public class AES
    {
        #region Private Member
        private AesCryptoServiceProvider objAES;
        #endregion

        #region Constructor
        public AES()
        {
            objAES = new AesCryptoServiceProvider();
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
            using(ICryptoTransform objCryptoTransform = objAES.CreateEncryptor())
            {
                byte[] encryptedByte = objCryptoTransform.TransformFinalBlock(plainBytes, 0, plainBytes.Length);        
                return Convert.ToBase64String(encryptedByte);
            }
        }
        
        /// <summary>
        /// Decryption of encrypted text
        /// </summary>
        /// <param name="ecncryptedString">encrypted string</param>
        /// <returns>original plain text</returns>
        public string Decrypt(string encryptedText)
        {
            byte[] encryptedTextByte = Convert.FromBase64String(encryptedText);

            using(ICryptoTransform objCryptoTransform = objAES.CreateDecryptor())
            {
                byte[] descryptedByte = objCryptoTransform.TransformFinalBlock(encryptedTextByte, 0, encryptedTextByte.Length); 
                return Encoding.UTF8.GetString(descryptedByte);
            }
        }
        #endregion
    }

}
