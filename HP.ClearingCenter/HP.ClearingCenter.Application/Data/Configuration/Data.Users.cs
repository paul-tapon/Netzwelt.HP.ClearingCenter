using HP.ClearingCenter.Application.Data.Entities;
using HP.ClearingCenter.Infrastructure.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HP.ClearingCenter.Application.Data.Configuration
{
    public static partial class Data
    {
        /// 395lu1/G8mmrdqu
        public const string SYSTEM_PASSWORD = "Mzk1bHUxL0c4bW1yZHF1";

        /// g4C8P$JKPBkaNxO
        public const string ADMINISTRATOR_PASSWORD = "ZzRDOFAkSktQQmthTnhP";

        public const string TEST_API_KEY = "VGhlIHF1aWNrIGJyb3duIGZveCBqdW1wcyBvdmVyIHRoZSBsYXp5IGRvZy4=";

        private static void Users(ClearingCenterDataContext db) 
        {
            var system = db.ApplicationUsers.Add(new ApplicationUser
            {
                Username = "system",
                DisplayName = "System",
                Password = new Dto.PasswordData(SYSTEM_PASSWORD.Base64Decode()),
                DefaultLanguageIsoCode = "en",
                IsAdmin = true,
                IsClearingEnabled = true,
                IsLoginEnabled = false,
                IsReceivingEnabled = true,
                IsProductManagementEnabled = true
            });

            var administrator = db.ApplicationUsers.Add(new ApplicationUser
            {
                Username = "administrator",
                DisplayName = "Administrator",
                Password = new Dto.PasswordData(ADMINISTRATOR_PASSWORD.Base64Decode()),
                DefaultLanguageIsoCode = "en",
                IsAdmin = true,
                IsClearingEnabled = true,
                IsReceivingEnabled = true,
                IsProductManagementEnabled = true,
                IsLoginEnabled = true,
            });

            var testAgent = db.ApplicationUsers.Add(new ApplicationUser
            {
                Username = "testagent",
                DisplayName = "Test Agent",
                Password = new Dto.PasswordData("test"),
                DefaultLanguageIsoCode = "en",
                IsAdmin = false,
                IsClearingEnabled = true,                
                IsReceivingEnabled = true,
                IsProductManagementEnabled = false,
                IsLoginEnabled = true,
            });

            var apiUser = db.ApplicationUsers.Add(new ApplicationUser
            {
                Username = "api_user",
                DisplayName = "Test Agent",
                Password = new Dto.PasswordData("api_user"),
                ApiKey = "VGhlIHF1aWNrIGJyb3duIGZveCBqdW1wcyBvdmVyIHRoZSBsYXp5IGRvZy4=",
                IsApiAccessEnabled = true,
                DefaultLanguageIsoCode = "en",
                IsAdmin = false,
                IsClearingEnabled = true,
                IsReceivingEnabled = true,
                IsProductManagementEnabled = false,
                IsLoginEnabled = true,
            });
        }
    }
}
