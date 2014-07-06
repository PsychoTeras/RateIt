using System;
using System.Text.RegularExpressions;

namespace RateIt.Common.Helpers
{
    internal static class CommonHelper
    {
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

        public static bool FuzzyMatchingForTwoString(string string1, string string2, int treshold)
        {
            int len, matching = 0;
            if ((len = string1.Length) == string2.Length)
            {
                for (int i = 0; i < len; i++)
                {
                    if (string1[i] != string2[i])
                    {
                        matching++;
                        if (matching > treshold)
                        {
                            return false;
                        }
                    }
                }
                return true;
            }
            return false;
        }
    }
}
