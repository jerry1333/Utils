using System.Collections.Generic;
using System.Linq;

namespace Jerry1333.Utils
{
    public static partial class Utils
    {
        /// <summary>
        ///     in check if element is in provided list
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">the value to check</param>
        /// <param name="values">List of values to look for</param>
        /// <returns>returns true if the value is in provided values else false</returns>
        public static bool IsIn<T>(this T value, params T[] values)
        {
            return values != null && values.Contains(value);
        }

        /// <summary>
        ///     in check if element is in provided values
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">the value to check</param>
        /// <param name="values">values to look for</param>
        /// <returns>returns true if the value is in provided values else false</returns>
        public static bool IsIn<T>(this T value, IEnumerable<T> values)
        {
            return values != null && values.Contains(value);
        }
    }
}