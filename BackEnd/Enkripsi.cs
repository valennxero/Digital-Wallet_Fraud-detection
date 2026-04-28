// BackEnd/Enkripsi.cs
using System;
using System.Security.Cryptography;
using System.Text;

namespace DigitalWallet.BackEnd
{
    public class Enkripsi
    {
        // CIPHER 1: AES-256
        private static readonly string AES_KEY = "MySecretKey12345MySecretKey12345"; // 32 byte
        private static readonly string AES_IV = "MySecretIV123456";                 // 16 byte

        public static string AESEncrypt(string plainText)
        {
            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(AES_KEY);
                aes.IV = Encoding.UTF8.GetBytes(AES_IV);

                ICryptoTransform encryptor = aes.CreateEncryptor();
                byte[] inputBytes = Encoding.UTF8.GetBytes(plainText);
                byte[] encrypted = encryptor.TransformFinalBlock(inputBytes, 0, inputBytes.Length);
                return Convert.ToBase64String(encrypted);
            }
        }

        public static string AESDecrypt(string cipherText)
        {
            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(AES_KEY);
                aes.IV = Encoding.UTF8.GetBytes(AES_IV);

                ICryptoTransform decryptor = aes.CreateDecryptor();
                byte[] inputBytes = Convert.FromBase64String(cipherText);
                byte[] decrypted = decryptor.TransformFinalBlock(inputBytes, 0, inputBytes.Length);
                return Encoding.UTF8.GetString(decrypted);
            }
        }

        // CIPHER 2: Vigenere
        private static readonly string VIGENERE_KEY = "WALLETKEY";

        public static string VigenereEncrypt(string text)
        {
            StringBuilder result = new StringBuilder();
            int keyIndex = 0;
            foreach (char c in text)
            {
                int shift = char.ToUpper(VIGENERE_KEY[keyIndex % VIGENERE_KEY.Length]) - 'A';
                result.Append((char)(c + shift));
                keyIndex++;
            }
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(result.ToString()));
        }

        public static string VigenereDecrypt(string encoded)
        {
            string text = Encoding.UTF8.GetString(Convert.FromBase64String(encoded));
            StringBuilder result = new StringBuilder();
            int keyIndex = 0;
            foreach (char c in text)
            {
                int shift = char.ToUpper(VIGENERE_KEY[keyIndex % VIGENERE_KEY.Length]) - 'A';
                result.Append((char)(c - shift));
                keyIndex++;
            }
            return result.ToString();
        }

        // KOMBINASI: AES → lalu Vigenere
        public static string EncryptDouble(string plainText)
        {
            string aesResult = AESEncrypt(plainText);
            string doubleEncrypted = VigenereEncrypt(aesResult);
            return doubleEncrypted;
        }

        public static string DecryptDouble(string cipherText)
        {
            string vigenereResult = VigenereDecrypt(cipherText);
            string plainText = AESDecrypt(vigenereResult);
            return plainText;
        }

        // HASHING: SHA-256 + Salt (untuk Master Password)
        public static string GenerateSalt()
        {
            byte[] salt = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
                rng.GetBytes(salt);
            return Convert.ToBase64String(salt);
        }

        public static string HashPassword(string password, string salt)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                string combined = password + salt;
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(combined));
                return Convert.ToBase64String(bytes);
            }
        }

        public static bool VerifyPassword(string inputPassword, string storedHash, string salt)
        {
            string inputHash = HashPassword(inputPassword, salt);
            return inputHash == storedHash;
        }

        //  AUTO-GENERATE PASSWORD
        public static string GeneratePassword(int length = 16)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*";
            byte[] randomBytes = new byte[length];
            using (var rng = RandomNumberGenerator.Create())
                rng.GetBytes(randomBytes);

            StringBuilder sb = new StringBuilder(length);
            foreach (byte b in randomBytes)
                sb.Append(chars[b % chars.Length]);
            return sb.ToString();
        }
    }
}