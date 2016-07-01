using HP.ClearingCenter.Infrastructure.Services;
using System;
using System.Security.Cryptography;
using System.Text;
namespace HP.ClearingCenter.Infrastructure.Helpers
{
    public static class StringExtensions
    {
        public static bool IsNullOrEmpty(this string arg)
        {
            return string.IsNullOrEmpty(arg);
        }

        public static string WithTokens(this string format, params object[] tokens)
        {
            if (format.IsNullOrEmpty()) return string.Empty;

            if (tokens == null || tokens.Length == 0) return format;

            return string.Format(format, tokens);
        }

        public static void ShouldNotBeEmpty(this string input, string paramName = "input")
        {
            string value = (input ?? string.Empty).Trim();

            if (value.IsNullOrEmpty())
            {
                throw new InvalidOperationException("{0} should not be null or empty.".WithTokens(paramName));
            }
        }

        public static string ForceNullIfEmpty(this string input)
        {
            return input == null || input.Length == 0 ?
                null : input.Trim();
        }

        public static string Ellipsify(this string value, int maxLength = 100)
        {
            const string ellipsis = "...";

            if (value.IsNullOrEmpty())
            {
                return string.Empty;
            }

            if (value.Length <= maxLength)
                return value;

            return value.Substring(0, maxLength - ellipsis.Length) + ellipsis;
        }

        public static string AESEncrypt(this string input) {
            var aes = new SimpleAES();
            return aes.EncryptToString(input);
        }

        public static string AESDecrypt(this string encryptedInput)
        {
            var aes = new SimpleAES();
            return aes.DecryptString(encryptedInput);
        }

        public static string Base64Encode(this string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        public static string Base64Decode(this string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }

        public static string Md5(this string input)
        {
            input = input.ToLowerInvariant();

            // Use input string to calculate MD5 hash
            MD5 md5 = System.Security.Cryptography.MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
            byte[] hashBytes = md5.ComputeHash(inputBytes);

            // Convert the byte array to hexadecimal string
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hashBytes.Length; i++)
            {
                sb.Append(hashBytes[i].ToString("X2"));
            }

            return sb.ToString().ToLowerInvariant();
        }
    }
}
