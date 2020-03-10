using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SSO.Utils
{
    public sealed class CryptorEngine
    {
        // Fields
        private const string INTERNAL_KEY = "sdt-21-02-2010-@#$111111";

        // Methods
        public CryptorEngine() { }
        public static string Decrypt(string cipherString)
        {
            return Decrypt(cipherString, true, INTERNAL_KEY);
        }
        public static string Decrypt(string cipherString, string key)
        {
            return Decrypt(cipherString, true, key);
        }
        public static string Decrypt(string cipherString, bool useHashing)
        {
            return Decrypt(cipherString, useHashing, INTERNAL_KEY);
        }
        public static string Decrypt(string cipherString, bool useHashing, string key)
        {
            byte[] buffer;
            //byte[] inputBuffer = HttpServerUtility.UrlTokenDecode(cipherString);
            byte[] inputBuffer = Convert.FromBase64String(cipherString);
            if (useHashing)
            {
                MD5CryptoServiceProvider provider = new MD5CryptoServiceProvider();
                buffer = provider.ComputeHash(Encoding.UTF8.GetBytes(key));
                provider.Clear();
            }
            else
            {/**/
                buffer = Encoding.UTF8.GetBytes(key);
            }
            TripleDESCryptoServiceProvider provider2 = new TripleDESCryptoServiceProvider
            {
                Key = buffer,
                Mode = CipherMode.ECB,
                Padding = PaddingMode.PKCS7
            };
            byte[] bytes = provider2.CreateDecryptor().TransformFinalBlock(inputBuffer, 0, inputBuffer.Length);
            provider2.Clear();
            return Encoding.UTF8.GetString(bytes);
        }
        public static string Encrypt(string toEncrypt)
        {
            return Encrypt(toEncrypt, true, INTERNAL_KEY);
        }
        public static string Encrypt(string toEncrypt, string publishKey)
        {

            return Encrypt(toEncrypt, true, publishKey);
        }
        public static string Encrypt(string toEncrypt, bool useHashing)
        {
            return Encrypt(toEncrypt, useHashing, INTERNAL_KEY);
        }

        public static string Encrypt(string toEncrypt, bool useHashing, string key)
        {
            byte[] buffer;
            byte[] bytes = Encoding.UTF8.GetBytes(toEncrypt);
            if (useHashing)
            {
                MD5CryptoServiceProvider provider = new MD5CryptoServiceProvider();
                buffer = provider.ComputeHash(Encoding.UTF8.GetBytes(key));
                provider.Clear();
            }
            else
            {
                buffer = Encoding.UTF8.GetBytes(key);
            }
            TripleDESCryptoServiceProvider provider2 = new TripleDESCryptoServiceProvider
            {
                Key = buffer,
                Mode = CipherMode.ECB,
                Padding = PaddingMode.PKCS7
            };
            byte[] input = provider2.CreateEncryptor().TransformFinalBlock(bytes, 0, bytes.Length);
            provider2.Clear();
            return Convert.ToBase64String(input);
        }
    }
    public class CryptoEngine
    {
        private static readonly byte[] IV = new byte[] { 240, 3, 0x2d, 0x1d, 0, 0x4c, 0xad, 0x3b };
        private static readonly TripleDESCryptoServiceProvider provider = new TripleDESCryptoServiceProvider();

        public static string Decrypt(string encryptedQueryString, string publishKey)
        {
            string str;
            try
            {
                byte[] inputBuffer = Convert.FromBase64String(encryptedQueryString);
                provider.Key = new MD5CryptoServiceProvider().ComputeHash(Encoding.UTF8.GetBytes(publishKey));
                provider.IV = IV;
                str = Encoding.UTF8.GetString(provider.CreateDecryptor().TransformFinalBlock(inputBuffer, 0, inputBuffer.Length));
            }
            catch (CryptographicException cEx)
            {
                throw cEx;
            }
            catch (FormatException fEx)
            {
                throw fEx;
            }
            return str;
        }

        public static string Encrypt(string serializedQueryString, string publishKey)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(serializedQueryString);
            provider.Key = new MD5CryptoServiceProvider().ComputeHash(Encoding.UTF8.GetBytes(publishKey));
            provider.IV = IV;
            return Convert.ToBase64String(provider.CreateEncryptor().TransformFinalBlock(bytes, 0, bytes.Length));
        }

        public static string MD5Encrypt(string plainText)
        {
            byte[] data, output;
            var encoder = new UTF8Encoding();
            var hasher = new MD5CryptoServiceProvider();

            data = encoder.GetBytes(plainText);
            output = hasher.ComputeHash(data);

            return BitConverter.ToString(output).Replace("-", "").ToLower();
        }
        public static string Md5x2(string str)
        {
            MD5CryptoServiceProvider provider = new MD5CryptoServiceProvider();
            byte[] bytes = Encoding.UTF8.GetBytes(str);
            bytes = provider.ComputeHash(bytes);
            StringBuilder builder = new StringBuilder();
            foreach (byte num in bytes)
            {
                builder.Append(num.ToString("x2").ToLower());
            }
            return builder.ToString();
        }
    }
}
