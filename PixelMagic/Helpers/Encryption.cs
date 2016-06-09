using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
#pragma warning disable 618

namespace PixelMagic.Helpers
{
    internal class Encryption
    {
        private static string E(string plainText, string passPhrase, string saltValue, string hashAlgorithm, int passwordIterations, string initVector, int keySize)
        {
            byte[] initVectorBytes = Encoding.ASCII.GetBytes(initVector);
            byte[] saltValueBytes = Encoding.ASCII.GetBytes(saltValue);
            byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            PasswordDeriveBytes password = new PasswordDeriveBytes(passPhrase, saltValueBytes, hashAlgorithm, passwordIterations);
            byte[] keyBytes = password.GetBytes(keySize / 8);
            RijndaelManaged symmetricKey = new RijndaelManaged {Mode = CipherMode.CBC};
            ICryptoTransform encryptor = symmetricKey.CreateEncryptor(keyBytes, initVectorBytes);
            MemoryStream memoryStream = new MemoryStream();
            CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);
            cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
            cryptoStream.FlushFinalBlock();
            byte[] cipherTextBytes = memoryStream.ToArray();
            memoryStream.Close();
            cryptoStream.Close();
            string cipherText = Convert.ToBase64String(cipherTextBytes);
            return cipherText;
        }

        private static string D(string cipherText, string passPhrase, string saltValue, string hashAlgorithm, int passwordIterations, string initVector, int keySize)
        {
            if (cipherText == string.Empty)
                return "";

            byte[] initVectorBytes = Encoding.ASCII.GetBytes(initVector);
            byte[] saltValueBytes = Encoding.ASCII.GetBytes(saltValue);
            byte[] cipherTextBytes = Convert.FromBase64String(cipherText);
            PasswordDeriveBytes password = new PasswordDeriveBytes(passPhrase, saltValueBytes, hashAlgorithm, passwordIterations);
            byte[] keyBytes = password.GetBytes(keySize / 8);
            RijndaelManaged symmetricKey = new RijndaelManaged {Mode = CipherMode.CBC};
            ICryptoTransform decryptor = symmetricKey.CreateDecryptor(keyBytes, initVectorBytes);
            MemoryStream memoryStream = new MemoryStream(cipherTextBytes);
            CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
            byte[] plainTextBytes = new byte[cipherTextBytes.Length];
            int decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
            memoryStream.Close();
            cryptoStream.Close();
            string plainText = Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
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

            string ret = E(text, passPhrase, saltValue, hashAlgorithm, passwordIterations, initVector, keySize);

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

            string ret = D(text, passPhrase, saltValue, hashAlgorithm, passwordIterations, initVector, keySize);

            return ret;
        }
    }
}
