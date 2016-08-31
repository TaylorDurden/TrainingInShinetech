using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace BugManagement.Infrastructure
{
    public static class Cryptography
    {
        private static readonly string KEY_64 = "12345678";
        private static readonly string IV_64 = "98765432";

        public static string Encrypt(string data)
        {
            var byKey = Encoding.ASCII.GetBytes(KEY_64);
            var byIV = Encoding.ASCII.GetBytes(IV_64);

            var cryptoProvider = new DESCryptoServiceProvider();
            var i = cryptoProvider.KeySize;
            var ms = new MemoryStream();
            var cst = new CryptoStream(ms, cryptoProvider.CreateEncryptor(byKey, byIV), CryptoStreamMode.Write);

            var sw = new StreamWriter(cst);
            sw.Write(data);
            sw.Flush();
            cst.FlushFinalBlock();
            sw.Flush();

            return Convert.ToBase64String(ms.GetBuffer(), 0, (int) ms.Length);
        }

        public static string Decrypt(string data)
        {
            var byKey = Encoding.ASCII.GetBytes(KEY_64);
            var byIV = Encoding.ASCII.GetBytes(IV_64);

            var byEnc = Convert.FromBase64String(data);

            var cryptoProvider = new DESCryptoServiceProvider();

            var ms = new MemoryStream(byEnc);
            var cst = new CryptoStream(ms, cryptoProvider.CreateDecryptor(byKey, byIV), CryptoStreamMode.Read);
            var sr = new StreamReader(cst);

            return sr.ReadToEnd();
        }
    }
}
