using System;
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
            byte[] hash = sha1.ComputeHash(encoding.GetBytes(hashStr));
            return BitConverter.ToString(hash).Replace("-", "").ToLower();
        }

        public static string FormatAdres(string miejscowosc, string kod, string poczta, string ulica, string dom)
        {
            try
            {
                string adres = ulica.IsNullOrEmpty() ? $"{miejscowosc} {dom}, " : $"ul. {ulica} {dom}, ";
                if (miejscowosc != poczta)
                    adres += $"{miejscowosc}, ";

                adres += $"{kod} {poczta}";

                return adres;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        static public bool RegonValidate(string regonVal)
        {
            try
            {
                byte[] weights;
                ulong regon = ulong.MinValue;
                byte[] digits;

                if (ulong.TryParse(regonVal, out regon).Equals(false)) return false;

                switch (regonVal.Length)
                {
                    case 7:
                        weights = new byte[] { 2, 3, 4, 5, 6, 7 };
                        break;

                    case 9:
                        weights = new byte[] { 8, 9, 2, 3, 4, 5, 6, 7 };
                        break;

                    case 14:
                        weights = new byte[] { 2, 4, 8, 5, 0, 9, 7, 3, 6, 1, 2, 4, 8 };
                        break;

                    default:
                        return false;
                }

                string sRegon = regon.ToString();
                digits = new byte[sRegon.Length];

                for (int i = 0; i < sRegon.Length; i++)
                {
                    if (byte.TryParse(sRegon[i].ToString(), out digits[i]).Equals(false)) return false;
                }

                int checksum = 0;

                for (int i = 0; i < weights.Length; i++)
                {
                    checksum += weights[i] * digits[i];
                }

                return (checksum % 11 % 10).Equals(digits[digits.Length - 1]);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        static public string Regon14zn(string regon)
        {
            try
            {
                regon = regon.RemoveNonNumbers();
                if (regon.Length == 9) return regon + "00000";
                else if (regon.Length == 14) return regon;
                else return null;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        static public bool NipValidate(string nipVal)
        {
            try
            {
                const byte Lenght = 10;

                ulong nip = ulong.MinValue;
                byte[] digits;
                var weights = new byte[] { 6, 5, 7, 2, 3, 4, 5, 6, 7 };

                if (nipVal.Length.Equals(Lenght).Equals(false)) return false;

                if (ulong.TryParse(nipVal, out nip).Equals(false)) return false;
                else
                {
                    string sNip = nipVal.ToString();
                    digits = new byte[Lenght];

                    for (int i = 0; i < Lenght; i++)
                    {
                        if (byte.TryParse(sNip[i].ToString(), out digits[i]).Equals(false)) return false;
                    }

                    int checksum = 0;

                    for (int i = 0; i < Lenght - 1; i++)
                    {
                        checksum += digits[i] * weights[i];
                    }

                    return (checksum % 11 % 10).Equals(digits[digits.Length - 1]);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        #region STRING EXTENSION

        static public string RemoveNonNumbers(this string text)
        {
            try
            {
                var rgx = new Regex("[^0-9]");
                return rgx.Replace(text, "");
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static bool IsNullOrEmpty(this string tekst)
        {
            try
            {
                if (string.IsNullOrEmpty(tekst) || tekst.Length == 0) return true;
                return false;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static string FormatNip(this string nip)
        {
            try
            {
                nip = nip.RemoveNonNumbers();
                return nip.Length == 10 ? $"{nip.Substring(0, 3)}-{nip.Substring(3, 3)}-{nip.Substring(6, 2)}-{nip.Substring(8, 2)}" : null;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static string FormatKodPocztowy(this string kod)
        {
            try
            {
                kod = kod.RemoveNonNumbers();
                return kod.Length == 5 ? $"{kod.Substring(0, 2)}-{kod.Substring(2)}" : null;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        #endregion
    }
}
