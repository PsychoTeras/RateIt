using System;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using RateIt.Common.Core.Constants;

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
        private static readonly Regex _regexUserNameVerify = 
            new Regex(@"^[A-Za-z][A-Za-z0-9]*(?:_[A-Za-z0-9]+)*$", RegexOptions.Compiled);
        private static readonly Regex _regexEmailVerify = 
            new Regex(@"^.+@.+\..+$", RegexOptions.Compiled);

        public static bool IsValidUserName(string userName)
        {
            return !string.IsNullOrEmpty(userName) &&
                   _regexUserNameVerify.IsMatch(userName);
        }

        public static bool IsValidEmail(string email)
        {
            return !string.IsNullOrEmpty(email) &&
                   email.Length <= GenericConstants.USER_EMAIL_LENGTH_MAX &&
                   _regexEmailVerify.IsMatch(email);
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
