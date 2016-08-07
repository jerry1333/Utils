using System;
using System.Security.Cryptography;
using System.Text;

namespace Jerry1333.Utils
{
    public static partial class Utils
    {
        public static string Sha1(string hashStr, Encoding encoding = null)
        {
            if (encoding == null) encoding = Encoding.UTF8;
            var sha1 = new SHA1CryptoServiceProvider();
            var hash = sha1.ComputeHash(encoding.GetBytes(hashStr));
            return BitConverter.ToString(hash).Replace("-", "").ToLower();
        }
    }
}
