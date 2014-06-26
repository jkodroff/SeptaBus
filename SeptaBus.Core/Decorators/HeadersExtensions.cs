using System;
using System.Collections.Generic;

namespace SeptaBus.Decorators
{
    public static class HeadersExtensions
    {
        public static string By(this IHasHeaders message)
        {
            return message.GetHeader<string>("By");
        }

        public static T By<T>(this T message, string value) where T : IHasHeaders
        {
            return message.SetHeader<T>("By", value);
        }

        public static DateTime On(this IHasHeaders message)
        {
            return message.GetHeader<DateTime>("On");
        }

        public static T On<T>(this T message, DateTime value) where T : IHasHeaders
        {
            return message.SetHeader<T>("On", value);
        }

        public static IEnumerable<string> Roles(this IHasHeaders message)
        {
            return message.GetHeader<IEnumerable<string>>("Roles");
        }

        public static T Roles<T>(this T message, IEnumerable<string> roles) where T : IHasHeaders
        {
            return message.SetHeader<T>("Roles", roles);
        }

        private static T GetHeader<T>(this IHasHeaders message, string key)
        {
            return (T)message.GetHeader(key);
        }

        private static T SetHeader<T>(this T message, string key, object value) where T : IHasHeaders
        {
            message.SetHeader(key, value);
            return message;
        }
    }
}