using System;

namespace Jerry1333.Utils
{
    public static partial class Utils
    {
        public enum NipFormat
        {
            Fiz, // XXX-XXX-XX-XX
            Praw, // XXX-XX-XX-XXX
            Simple // XXXXXXXXXX
        }

        public static bool ValidateNip(string nipVal)
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
                    checksum += digits[i] * weights[i];
                }

                return (checksum % 11 % 10).Equals(digits[digits.Length - 1]);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static string FormatNip(this string nipVal, NipFormat format = NipFormat.Simple)
        {
            try
            {
                nipVal = nipVal.RemoveNonNumbers();

                if (nipVal.Length != 10) throw new ArgumentException(nameof(nipVal));

                switch (format)
                {
                    case NipFormat.Fiz:
                        nipVal = $"{nipVal.Substring(0, 3)}-{nipVal.Substring(3, 3)}-{nipVal.Substring(6, 2)}-{nipVal.Substring(8, 2)}";
                        break;
                    case NipFormat.Praw:
                        nipVal = $"{nipVal.Substring(0, 3)}-{nipVal.Substring(3, 2)}-{nipVal.Substring(5, 2)}-{nipVal.Substring(7, 3)}";
                        break;
                    case NipFormat.Simple:
                        break;
                }
                return nipVal;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
