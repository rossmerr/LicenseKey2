using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace System
{
    internal class ApplicationException : Exception
    {
        public ApplicationException(string message) : base(message) { }
    }

    internal static class Extensions
    {
        public static IEnumerable<PropertyInfo> GetProperties(this Type type)
        {
            return type.GetTypeInfo().DeclaredProperties;
        }

        public static IEnumerable<Attribute> GetCustomAttributes(this PropertyInfo pi, Type attributeType, bool inherit)
        {
            return System.Reflection.CustomAttributeExtensions.GetCustomAttributes(pi, attributeType, inherit);
        }

        public static MethodInfo GetGetMethod(this PropertyInfo pi)
        {
            return pi.GetMethod;
        }

        public static string ToString(this char c, Globalization.CultureInfo cultureInfo )
        {
            return c.ToString();
        }
    }
}
