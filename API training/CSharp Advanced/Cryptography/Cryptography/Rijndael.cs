using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Cryptography
{
    /// <summary>
    /// Implementation of Rijndael encryption and decryption using the DESCryptoServiceProvider.
    /// </summary>
    public class Rijndael
    {
        #region Private Member
        /// <summary>
        /// private key
        /// </summary>
        private string _privateKey = "edjsdedj";
        #endregion

        #region Public Member

        /// <summary>
        /// public key
        /// </summary>
        public string publicKey = "mdjxedjw";

        /// <summary>
        /// private key bytes array
        /// </summary>
        public byte[] privateKeyByte = { };

        /// <summary>
        /// public key bytes array
        /// </summary>
        public byte[] publicKeyByte = { };
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor for the Rijndael class. Initializes the private and public key bytes.
        /// </summary>
        public Rijndael()
        {
            privateKeyByte = Encoding.UTF8.GetBytes(_privateKey);
            publicKeyByte = Encoding.UTF8.GetBytes(publicKey);
        }
        #endregion


        #region Public Method
        /// <summary>
        /// Encrypts the input plain text using Rijndael algorithm.
        /// </summary>
        /// <param name="plainText">The plain text to be encrypted.</param>
        /// <returns>Base64-encoded encrypted text.</returns>
        public string Encrypt(string plainText)
        {
            byte[] plainTextByte = Encoding.UTF8.GetBytes(plainText);

            using (DESCryptoServiceProvider objDESCryptoServiceProvider = new DESCryptoServiceProvider())
            {
                var objMemoryStream = new MemoryStream();
                var objCryptoStream = new CryptoStream(objMemoryStream, objDESCryptoServiceProvider.CreateEncryptor(publicKeyByte, privateKeyByte), CryptoStreamMode.Write);

                objCryptoStream.Write(plainTextByte, 0, plainTextByte.Length);
                objCryptoStream.FlushFinalBlock();

                return Convert.ToBase64String(objMemoryStream.ToArray());
            }
        }

        /// <summary>
        /// Decrypts the input encrypted text using Rijndael algorithm.
        /// </summary>
        /// <param name="encryptedText">The Base64-encoded encrypted text to be decrypted.</param>
        /// <returns>The decrypted plain text.</returns>
        public string Decrypt(string encryptedText)
        {
            byte[] encryptedTextByte = Convert.FromBase64String(encryptedText);

            using (DESCryptoServiceProvider objDESCryptoServiceProvider = new DESCryptoServiceProvider())
            {
                using (var objMemoryStream = new MemoryStream())
                {
                    using (var objCryptoStream = new CryptoStream(objMemoryStream, objDESCryptoServiceProvider.CreateDecryptor(publicKeyByte, privateKeyByte), CryptoStreamMode.Write))
                    {
                        objCryptoStream.Write(encryptedTextByte, 0, encryptedTextByte.Length);
                        objCryptoStream.FlushFinalBlock();
                    }

                    string decryptedText = Encoding.UTF8.GetString(objMemoryStream.ToArray());
                    return decryptedText;
                }
            }
        }
        #endregion
    }
}
