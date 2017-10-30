using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace Jerry1333.Utils
{
    public static partial class Utils
    {
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

        public static Version GetUtilsVersion()
        {
            return GetVersion(typeof(Utils));
        }
    }
}