using System;

namespace Jerry1333.Utils
{
    public static partial class Utils
    {
        public static string FormatAdress(string city, string postalCode, string postalCity, string street, string house)
        {
            try
            {
                var adres = street.IsNullOrEmpty() ? $"{city} {house}, " : $"ul. {street} {house}, ";
                if (city != postalCity) adres += $"{city}, ";

                adres += $"{FormatPostalCode(postalCode)} {postalCity}";

                return adres;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static string FormatPostalCode(string code)
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
    }
}