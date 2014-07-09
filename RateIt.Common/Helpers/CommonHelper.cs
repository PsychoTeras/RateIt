using System;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace RateIt.Common.Helpers
{
    public static class CommonHelper
    {
        private static readonly HashAlgorithm _hashAlgorithm = new SHA1Managed();

        public static string GetHashSum(string utf8Str)
        {
            byte[] strBytes = Encoding.Unicode.GetBytes(utf8Str);
            byte[] hash = _hashAlgorithm.ComputeHash(strBytes, 0, strBytes.Length);
            return BitConverter.ToString(hash).Replace("-", "").ToLower();
        }
    }

    internal static class InternalHelper
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
