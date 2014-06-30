using System;
using System.Text.RegularExpressions;

namespace RateIt.Common.Helpers
{
    internal static class Helper
    {

#region Class static methods

        public static bool IsValidEmail(string email)
        {
            return new Regex(@"^.+@.+\..+$").IsMatch(email ?? string.Empty);
        }

        public static void SafeDispose<T>(ref T obj) where T : class, IDisposable
        {
            if (obj != null)
            {
                obj.Dispose();
                obj = null;
            }
        }

#endregion

    }
}
