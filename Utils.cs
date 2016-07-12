using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace Jerry1333.Libs
{
    public static class Utils
    {
        public static string Sha1(string hashStr, Encoding encoding = null)
        {
            if (encoding == null) encoding = Encoding.UTF8;
            var sha1 = new SHA1CryptoServiceProvider();
            var hash = sha1.ComputeHash(encoding.GetBytes(hashStr));
            return BitConverter.ToString(hash).Replace("-", "").ToLower();
        }

        public static string FormatAdress(string city, string postalCode, string postalCity, string street, string house)
        {
            try
            {
                var adres = street.IsNullOrEmpty() ? $"{city} {house}, " : $"ul. {street} {house}, ";
                if (city != postalCity)
                    adres += $"{city}, ";

                adres += $"{postalCode.FormatPostalCode()} {postalCity}";

                return adres;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static bool RegonValidate(string regonVal)
        {
            try
            {
                byte[] weights;
                ulong regon;

                if (ulong.TryParse(regonVal, out regon).Equals(false)) return false;

                switch (regonVal.Length)
                {
                    case 7:
                        weights = new byte[] {2, 3, 4, 5, 6, 7};
                        break;

                    case 9:
                        weights = new byte[] {8, 9, 2, 3, 4, 5, 6, 7};
                        break;

                    case 14:
                        weights = new byte[] {2, 4, 8, 5, 0, 9, 7, 3, 6, 1, 2, 4, 8};
                        break;

                    default:
                        return false;
                }

                var sRegon = regon.ToString();
                var digits = new byte[sRegon.Length];

                for (var i = 0; i < sRegon.Length; i++)
                {
                    if (byte.TryParse(sRegon[i].ToString(), out digits[i]).Equals(false)) return false;
                }

                var checksum = 0;

                for (var i = 0; i < weights.Length; i++)
                {
                    checksum += weights[i]*digits[i];
                }

                return (checksum%11%10).Equals(digits[digits.Length - 1]);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static string Regon14zn(string regon)
        {
            try
            {
                regon = regon.RemoveNonNumbers();
                if (regon.Length == 9) return regon + "00000";
                if (regon.Length == 14) return regon;
                return null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static bool NipValidate(string nipVal)
        {
            try
            {
                const byte lenght = 10;

                ulong nip;
                var weights = new byte[] {6, 5, 7, 2, 3, 4, 5, 6, 7};

                if (nipVal.Length.Equals(lenght).Equals(false)) return false;

                if (ulong.TryParse(nipVal, out nip).Equals(false)) return false;


                var sNip = nipVal;
                var digits = new byte[lenght];

                for (var i = 0; i < lenght; i++)
                {
                    if (byte.TryParse(sNip[i].ToString(), out digits[i]).Equals(false)) return false;
                }

                var checksum = 0;

                for (var i = 0; i < lenght - 1; i++)
                {
                    checksum += digits[i]*weights[i];
                }

                return (checksum%11%10).Equals(digits[digits.Length - 1]);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static IEnumerable<Tuple<T, string>> GetValueDescriptionEnumerable<T>() where T : struct
        {
            if (!typeof(T).IsEnum) throw new InvalidOperationException();
            foreach (T item in Enum.GetValues(typeof(T)))
            {
                var fi = typeof(T).GetField(item.ToString());
                var attribute = fi.GetCustomAttributes(typeof(DescriptionAttribute), false).FirstOrDefault() as DescriptionAttribute;
                var description = attribute == null ? item.ToString() : attribute.Description;
                yield return new Tuple<T, string>(item, description);
            }
        }

        public static void PreserveStackTrace(Exception exception)
        {
            var preserveStackTrace = typeof(Exception).GetMethod("InternalPreserveStackTrace", BindingFlags.Instance | BindingFlags.NonPublic);
            preserveStackTrace.Invoke(exception, null);
        }

        public static Version GetVersion(Type param)
        {
            return param.Assembly.GetName().Version;
        }

        public static bool VerifyRegEx(string testPattern)
        {
            var isValid = true;
            if (!testPattern.IsNullOrEmpty())
            {
                try
                {
                    Regex.Match("", testPattern);
                }
                catch (Exception)
                {
                    isValid = false;
                }
            }
            else
            {
                isValid = false;
            }
            return isValid;
        }

        #region STRING EXTENSION

        public static string RemoveNonNumbers(this string text)
        {
            try
            {
                var rgx = new Regex("[^0-9]");
                return rgx.Replace(text, "");
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static string RemoveNonAplhaNumbers(this string text)
        {
            try
            {
                var rgx = new Regex("[^a-zA-Z0-9]");
                return rgx.Replace(text, "");
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static string RemoveNonAplha(this string text)
        {
            try
            {
                var rgx = new Regex("[^a-zA-Z]");
                return rgx.Replace(text, "");
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static string RemoveRegexPattern(this string text, string pattern)
        {
            try
            {
                if (pattern.IsNullOrEmpty()) throw new ArgumentNullException(nameof(pattern));
                if (!VerifyRegEx(pattern)) throw new ArgumentException(nameof(pattern));

                var rgx = new Regex(pattern);
                return rgx.Replace(text, "");
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static bool IsNullOrEmpty(this string text)
        {
            try
            {
                return string.IsNullOrEmpty(text);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static string FormatNip(this string nip)
        {
            try
            {
                nip = nip.RemoveNonNumbers();
                return nip.Length == 10 ? $"{nip.Substring(0, 3)}-{nip.Substring(3, 3)}-{nip.Substring(6, 2)}-{nip.Substring(8, 2)}" : null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static string FormatPostalCode(this string code)
        {
            try
            {
                code = code.RemoveNonNumbers();
                return code.Length == 5 ? $"{code.Substring(0, 2)}-{code.Substring(2)}" : null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static string[] SplitWithCheckSeparator(this string line, char separator, char checkSeparator, bool eraseCheckSeparator)
        {
            var separatorsIndexes = new List<int>();
            var open = false;

            for (var i = 0; i < line.Length; i++)
            {
                if (line[i] == checkSeparator)
                    open = !open;
                if (!open && line[i] == separator)
                    separatorsIndexes.Add(i);
            }

            separatorsIndexes.Add(line.Length);

            var result = new string[separatorsIndexes.Count];

            var first = 0;

            for (var j = 0; j < separatorsIndexes.Count; j++)
            {
                var tempLine = line.Substring(first, separatorsIndexes[j] - first);
                result[j] = eraseCheckSeparator ? tempLine.Replace(checkSeparator, ' ').Trim() : tempLine;
                first = separatorsIndexes[j] + 1;
            }

            return result;
        }

        #endregion
    }
}
