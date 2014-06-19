using System;
using System.Collections.Generic;

namespace SeptaBus
{
    public static class MessageBaseExtensions
    {
        public static string By(this MessageBase message)
        {
            return message.GetHeader<string>("By");
        }

        public static DateTime On(this MessageBase message)
        {
            return message.GetHeader<DateTime>("On");
        }

        public static IEnumerable<string> Roles(this MessageBase message)
        {
            return message.GetHeader<IEnumerable<string>>("Roles");
        }

        private static T GetHeader<T>(this MessageBase message, string key)
        {
            return (T)message.GetHeader(key);
        }
    }
}