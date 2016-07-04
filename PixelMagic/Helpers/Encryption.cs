using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
#pragma warning disable 618

namespace PixelMagic.Helpers
{
    internal static class Encryption
    {
        private static string E(string plainText, string passPhrase, string saltValue, string hashAlgorithm, int passwordIterations, string initVector, int keySize)
        {
            var initVectorBytes = Encoding.ASCII.GetBytes(initVector);
            var saltValueBytes = Encoding.ASCII.GetBytes(saltValue);
            var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            var password = new PasswordDeriveBytes(passPhrase, saltValueBytes, hashAlgorithm, passwordIterations);
            var keyBytes = password.GetBytes(keySize / 8);
            var symmetricKey = new RijndaelManaged {Mode = CipherMode.CBC};
            var encryptor = symmetricKey.CreateEncryptor(keyBytes, initVectorBytes);
            var memoryStream = new MemoryStream();
            var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);
            cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
            cryptoStream.FlushFinalBlock();
            var cipherTextBytes = memoryStream.ToArray();
            memoryStream.Close();
            cryptoStream.Close();
            var cipherText = Convert.ToBase64String(cipherTextBytes);
            return cipherText;
        }

        private static string D(string cipherText, string passPhrase, string saltValue, string hashAlgorithm, int passwordIterations, string initVector, int keySize)
        {
            if (cipherText == string.Empty)
                return "";

            var initVectorBytes = Encoding.ASCII.GetBytes(initVector);
            var saltValueBytes = Encoding.ASCII.GetBytes(saltValue);
            var cipherTextBytes = Convert.FromBase64String(cipherText);
            var password = new PasswordDeriveBytes(passPhrase, saltValueBytes, hashAlgorithm, passwordIterations);
            var keyBytes = password.GetBytes(keySize / 8);
            var symmetricKey = new RijndaelManaged {Mode = CipherMode.CBC};
            var decryptor = symmetricKey.CreateDecryptor(keyBytes, initVectorBytes);
            var memoryStream = new MemoryStream(cipherTextBytes);
            var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
            var plainTextBytes = new byte[cipherTextBytes.Length];
            var decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
            memoryStream.Close();
            cryptoStream.Close();
            var plainText = Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
            return plainText;
        }

        internal static string Encrypt(string text)
        {
            const string passPhrase = "Ydfv324232r!23%47%7^&ex>,1"; // can be any string
            const string saltValue = "s@1tValue"; // can be any string
            const string hashAlgorithm = "SHA1"; // can be "MD5"
            const int passwordIterations = 101; // can be any number
            const string initVector = "@1B2vQ94eZF6g7H1"; // must be 16 bytes
            const int keySize = 256; // can be 192 or 128

            var ret = E(text, passPhrase, saltValue, hashAlgorithm, passwordIterations, initVector, keySize);

            return ret;
        }

        internal static string Decrypt(string text)
        {
            const string passPhrase = "Ydfv324232r!23%47%7^&ex>,1"; // can be any string
            const string saltValue = "s@1tValue"; // can be any string
            const string hashAlgorithm = "SHA1"; // can be "MD5"
            const int passwordIterations = 101; // can be any number
            const string initVector = "@1B2vQ94eZF6g7H1"; // must be 16 bytes
            const int keySize = 256; // can be 192 or 128

            var ret = D(text, passPhrase, saltValue, hashAlgorithm, passwordIterations, initVector, keySize);

            return ret;
        }
    }
}
