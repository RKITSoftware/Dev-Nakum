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

        /// <summary>
        /// create the object of the AesCryptoServiceProvider
        /// </summary>
        private AesCryptoServiceProvider _objAES;
        #endregion

        #region Constructor

        /// <summary>
        /// initialize the object of the AesCryptoServiceProvider
        /// </summary>
        public AES()
        {
            _objAES = new AesCryptoServiceProvider();
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
            //convert into bytes
            byte[] plainBytes = Encoding.UTF8.GetBytes(plainText);  
            using(ICryptoTransform objCryptoTransform = _objAES.CreateEncryptor())
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

            using(ICryptoTransform objCryptoTransform = _objAES.CreateDecryptor())
            {
                byte[] descryptedByte = objCryptoTransform.TransformFinalBlock(encryptedTextByte, 0, encryptedTextByte.Length); 
                return Encoding.UTF8.GetString(descryptedByte);
            }
        }
        #endregion
    }

}
