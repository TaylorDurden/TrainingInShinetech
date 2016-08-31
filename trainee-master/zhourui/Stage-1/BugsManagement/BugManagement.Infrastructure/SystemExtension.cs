using System;
using System.Text.RegularExpressions;

namespace BugManagement.Infrastructure
{
    public static class SystemExtension
    {
        public static bool IsNumber(this string value)
        {
            try
            {
                return Regex.IsMatch(value, @"^[+-]?\d*$");
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool IsEmail(this string email)
        {
            var r = new Regex("^\\s*([A-Za-z0-9_-]+(\\.\\w+)*@(\\w+\\.)+\\w{2,5})\\s*$");
            return r.IsMatch(email);
        }
    }
}