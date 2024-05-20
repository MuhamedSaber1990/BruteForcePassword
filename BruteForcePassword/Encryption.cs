using System;
using System.Security.Cryptography;
using System.IO;

namespace BruteForcePassword
{
    public class Password
    {
        public string? password; // Field to store the password

        public (byte[] key, byte[] IV, byte[] encrypted) encryptPassword(string password)
        {
            byte[] key, IV, encrypted;
            using (Aes myAes = Aes.Create()) // Create a new AES instance
            {
                encrypted = EncryptStringToBytes_Aes(password, myAes.Key, myAes.IV); // Encrypt the password
                key = myAes.Key; // Get the encryption key
                IV = myAes.IV; // Get the initialization vector
            }

            return (key, IV, encrypted); // Return the encryption parameters
        }

        public byte[] EncryptStringToBytes_Aes(string plainText, byte[] Key, byte[] IV)
        {
            if (plainText == null || plainText.Length <= 0)
                throw new ArgumentNullException("plainText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("IV");

            byte[] encrypted;

            using (Aes aesAlg = Aes.Create()) // Create a new AES instance
            {
                aesAlg.Key = Key; // Set the encryption key
                aesAlg.IV = IV; // Set the initialization vector

                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV); // Create an encryptor

                using (MemoryStream msEncrypt = new MemoryStream()) // Create a memory stream for encryption
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write)) // Create a crypto stream
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt)) // Create a stream writer
                        {
                            swEncrypt.Write(plainText); // Write the plaintext to the stream
                        }
                        encrypted = msEncrypt.ToArray(); // Get the encrypted bytes
                    }
                }
            }

            return encrypted; // Return the encrypted bytes
        }

        public string DecryptStringFromBytes_Aes(byte[] cipherText, byte[] Key, byte[] IV)
        {
            if (cipherText == null || cipherText.Length <= 0)
                throw new ArgumentNullException("cipherText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("IV");

            string? plaintext = null;

            using (Aes aesAlg = Aes.Create()) // Create a new AES instance
            {
                aesAlg.Key = Key; // Set the decryption key
                aesAlg.IV = IV; // Set the initialization vector

                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV); // Create a decryptor

                using (MemoryStream msDecrypt = new MemoryStream(cipherText)) // Create a memory stream for decryption
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read)) // Create a crypto stream
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt)) // Create a stream reader
                        {
                            plaintext = srDecrypt.ReadToEnd(); // Read the decrypted text
                        }
                    }
                }
            }

            return plaintext; // Return the decrypted text
        }
    }
}
