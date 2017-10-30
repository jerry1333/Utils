using System;

namespace Jerry1333.Utils
{
    public static partial class Utils
    {
        public static bool ValidateRegon(string regonVal)
        {
            try
            {
                byte[] weights;

                if (ulong.TryParse(regonVal, out var regon).Equals(false)) return false;

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
                    default: return false;
                }

                var sRegon = regon.ToString();
                var digits = new byte[sRegon.Length];

                for (var i = 0; i < sRegon.Length; i++)
                    if (byte.TryParse(sRegon[i].ToString(), out digits[i]).Equals(false))
                        return false;

                var checksum = 0;

                for (var i = 0; i < weights.Length; i++)
                    checksum += weights[i] * digits[i];

                return (checksum % 11 % 10).Equals(digits[digits.Length - 1]);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static string Regon9To14(string regonVal)
        {
            try
            {
                regonVal = regonVal.RemoveNonNumbers();
                if (regonVal.Length == 9) return $"{regonVal}00000";
                if (regonVal.Length == 14) return regonVal;
                return null;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}