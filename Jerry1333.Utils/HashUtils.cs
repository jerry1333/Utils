using System;
using System.Security.Cryptography;
using System.Text;

namespace Jerry1333.Utils
{
    public static partial class Utils
    {
        public static string Sha1(string hashStr, Encoding encoding = null)
        {
            try
            {
                if (hashStr == null) throw new ArgumentNullException(nameof(hashStr));
                if (encoding == null) encoding = Encoding.UTF8;
                var sha1 = new SHA1CryptoServiceProvider();
                var hash = sha1.ComputeHash(encoding.GetBytes(hashStr));
                return BitConverter.ToString(hash).Replace("-", "").ToLower();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static string Md5(string hashStr, Encoding encoding = null)
        {
            try
            {
                if (hashStr == null) throw new ArgumentNullException(nameof(hashStr));
                if (encoding == null) encoding = Encoding.UTF8;
                var md5 = new MD5CryptoServiceProvider();
                var hash = md5.ComputeHash(encoding.GetBytes(hashStr));
                return BitConverter.ToString(hash).Replace("-", "").ToLower();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}