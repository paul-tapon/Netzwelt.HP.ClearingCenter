using HP.ClearingCenter.Infrastructure.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace HP.ClearingCenter.Application.Data.Dto
{
    [ComplexType]
    [Serializable]
    public class PasswordData
    {
        public PasswordData() {}

        public PasswordData(string rawPassword)
        {
            this.Salt = GenerateSalt();            
            this.Hash = GenerateHash(GenerateRawInput(rawPassword.Trim(), this.Salt));
        }

        [StringLength(512)]
        public string Hash { get; set; }

        [StringLength(512)]
        public string Salt { get; set; }

        public bool IsRawInputValid(string rawPassword)
        {
            string rawInput = GenerateRawInput(rawPassword.Trim(), this.Salt);
            string generatedHash = GenerateHash(rawInput);
            return this.Hash == generatedHash;
        }

        private static string GenerateRawInput(string rawPassword, string salt)
        {
            return "{0}{1}".WithTokens(rawPassword, salt);
        }

        private static string GenerateHash(string rawInput)
        {
            System.Security.Cryptography.SHA256Managed crypt = new System.Security.Cryptography.SHA256Managed();
            System.Text.StringBuilder hash = new System.Text.StringBuilder();
            byte[] crypto = crypt.ComputeHash(Encoding.UTF8.GetBytes(rawInput), 0, Encoding.UTF8.GetByteCount(rawInput));
            foreach (byte theByte in crypto)
            {
                hash.Append(theByte.ToString("x2"));
            }
            return hash.ToString();
        }

        private static string GenerateSalt()
        {
            var random = new RNGCryptoServiceProvider();

            // Maximum length of salt
            int max_length = 32;

            // Empty salt array
            byte[] salt = new byte[max_length];

            // Build the random bytes
            random.GetNonZeroBytes(salt);

            // Return the string encoded salt
            return Convert.ToBase64String(salt);
        }
    }
}
