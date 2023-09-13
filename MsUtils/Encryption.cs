using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MsUtils
{
    public class Encryption
    {
        public static string EncryptString(string InputText, string EncryptKey = "p@ssw0rd")
        {
            byte[] IV = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };
            try
            {
                byte[] bykey = System.Text.Encoding.UTF8.GetBytes(EncryptKey);
                byte[] InputByteArray = System.Text.Encoding.UTF8.GetBytes(InputText);
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(bykey, IV), CryptoStreamMode.Write);
                cs.Write(InputByteArray, 0, InputByteArray.Length);
                cs.FlushFinalBlock();
                return Convert.ToBase64String(ms.ToArray());
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public static string DecryptString(string InputText, string DecryptKey = "p@ssw0rd")
        {
            if (InputText == null) return "";
            byte[] IV = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };
            byte[] inputByteArray = new byte[InputText.Length + 1];
            try
            {
                byte[] byKey = System.Text.Encoding.UTF8.GetBytes(DecryptKey);
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                inputByteArray = Convert.FromBase64String(InputText);
                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(byKey, IV), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();
                System.Text.Encoding encoding = System.Text.Encoding.UTF8;
                return encoding.GetString(ms.ToArray());
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public static string EncodeSHA1(string pass)

        {

            SHA1CryptoServiceProvider sha1 = new SHA1CryptoServiceProvider();

            byte[] bs = System.Text.Encoding.UTF8.GetBytes(pass);

            bs = sha1.ComputeHash(bs);

            System.Text.StringBuilder s = new System.Text.StringBuilder();

            foreach (byte b in bs)
            {
                s.Append(b.ToString("x1").ToLower());
            }

            pass = s.ToString();

            return pass;

        }

        public static byte[] Encrypt_AES(byte[] data, byte[] key, byte[] iv)
        {
            using (var aes = Aes.Create())
            {
                aes.KeySize = 128;
                aes.BlockSize = 128;
                aes.Padding = PaddingMode.Zeros;
                aes.Mode = CipherMode.CBC;

                aes.Key = key;
                aes.IV = iv;

                using (var encryptor = aes.CreateEncryptor(aes.Key, aes.IV))
                {
                    return PerformCryptography(data, encryptor);
                }
            }
        }

        public static byte[] Decrypt_AES(byte[] data, byte[] key, byte[] iv)
        {
            using (var aes = Aes.Create())
            {
                aes.KeySize = 128;
                aes.BlockSize = 128;
                aes.Padding = PaddingMode.Zeros;
                aes.Mode = CipherMode.CBC;

                aes.Key = key;
                aes.IV = iv;

                using (var decryptor = aes.CreateDecryptor(aes.Key, aes.IV))
                {
                    return PerformCryptography(data, decryptor);
                }
            }
        }

        private static byte[] PerformCryptography(byte[] data, ICryptoTransform cryptoTransform)
        {
            using (var ms = new MemoryStream())
            using (var cryptoStream = new CryptoStream(ms, cryptoTransform, CryptoStreamMode.Write))
            {
                cryptoStream.Write(data, 0, data.Length);
                cryptoStream.FlushFinalBlock();

                return ms.ToArray();
            }
        }

        public static string CreateMD5(string input)
        {
            // Use input string to calculate MD5 hash
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                // Convert the byte array to hexadecimal string
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }
                return sb.ToString();
            }
        }

        public static byte[] TrippleDESEncrypt(string plainText, byte[] Key, byte[] IV)
        {
            byte[] encrypted;
            // Create a new TripleDESCryptoServiceProvider.  
            using (TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider())
            {
                tdes.KeySize = 128;
                tdes.Padding = PaddingMode.PKCS7;
                tdes.Key = Key;
                tdes.IV = IV;
                // Create encryptor  
                ICryptoTransform encryptor = tdes.CreateEncryptor();
                // Create MemoryStream  
                using (MemoryStream ms = new MemoryStream())
                {
                    // Create crypto stream using the CryptoStream class. This class is the key to encryption  
                    // and encrypts and decrypts data from any given stream. In this case, we will pass a memory stream  
                    // to encrypt  
                    using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    {
                        // Create StreamWriter and write data to a stream  
                        using (StreamWriter sw = new StreamWriter(cs))
                            sw.Write(plainText);
                        encrypted = ms.ToArray();
                    }
                }
            }
            // Return encrypted data  
            return encrypted;
        }
        public static string TrippleDESDecrypt(byte[] cipherText, byte[] Key, byte[] IV)
        {
            string plaintext = null;
            // Create TripleDESCryptoServiceProvider  
            using (TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider())
            {
                tdes.KeySize = 128;
                tdes.Padding = PaddingMode.PKCS7;
                tdes.Key = Key;
                tdes.IV = IV;
                // Create a decryptor  
                ICryptoTransform decryptor = tdes.CreateDecryptor();
                // Create the streams used for decryption.  
                using (MemoryStream ms = new MemoryStream(cipherText))
                {
                    // Create crypto stream  
                    using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                    {
                        // Read crypto stream  
                        using (StreamReader reader = new StreamReader(cs))
                            plaintext = reader.ReadToEnd();
                    }
                }
            }
            return plaintext;
        }
    }
}
