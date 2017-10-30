using System;
using System.Text.RegularExpressions;

namespace Jerry1333.Utils
{
    public static partial class Utils
    {
        public static bool IsPatternValid(string testPattern)
        {
            var isValid = true;
            if (!testPattern.IsNullOrEmpty())
                try
                {
                    Regex.Match("", testPattern);
                }
                catch (Exception)
                {
                    isValid = false;
                }
            else
                isValid = false;
            return isValid;
        }

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
                if (!IsPatternValid(pattern)) throw new ArgumentException(nameof(pattern));

                var rgx = new Regex(pattern);
                return rgx.Replace(text, "");
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}