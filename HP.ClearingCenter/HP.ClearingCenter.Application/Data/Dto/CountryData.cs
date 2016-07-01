using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HP.ClearingCenter.Application.Data.Dto
{
    public class CountryData
    {
        static CountryData()
        {
            All = BuildList();
        }

        public static IEnumerable<CountryData> All { get; private set; }
        public string IsoCode { get; set; }
        public string Name { get; set; }

        #region buildlist
        private static IEnumerable<CountryData> BuildList()
        {
            yield return new CountryData { IsoCode = "ar", Name = "Argentina" };
            yield return new CountryData { IsoCode = "at", Name = "Austria" };
            yield return new CountryData { IsoCode = "au", Name = "Australia" };
            yield return new CountryData { IsoCode = "be", Name = "Belgium" };
            yield return new CountryData { IsoCode = "bg", Name = "Bulgaria" };
            yield return new CountryData { IsoCode = "br", Name = "Brasil" };
            yield return new CountryData { IsoCode = "ca", Name = "Canada" };
            yield return new CountryData { IsoCode = "ch", Name = "Switzerland" };
            yield return new CountryData { IsoCode = "cl", Name = "Chile" };
            yield return new CountryData { IsoCode = "cn", Name = "China" };
            yield return new CountryData { IsoCode = "co", Name = "Colombia" };
            yield return new CountryData { IsoCode = "cr", Name = "Costa Rica" };
            yield return new CountryData { IsoCode = "cy", Name = "Cyprus" };
            yield return new CountryData { IsoCode = "cz", Name = "Czech Republic" };
            yield return new CountryData { IsoCode = "de", Name = "Germany" };
            yield return new CountryData { IsoCode = "dk", Name = "Denmark" };
            yield return new CountryData { IsoCode = "ec", Name = "Ecuador" };
            yield return new CountryData { IsoCode = "ee", Name = "Estonia" };
            yield return new CountryData { IsoCode = "es", Name = "Spain" };
            yield return new CountryData { IsoCode = "fi", Name = "Finland" };
            yield return new CountryData { IsoCode = "fr", Name = "France" };
            yield return new CountryData { IsoCode = "gb", Name = "Great Britain" };
            yield return new CountryData { IsoCode = "gr", Name = "Greece" };
            yield return new CountryData { IsoCode = "hk", Name = "Hong Kong" };
            yield return new CountryData { IsoCode = "hr", Name = "Croatia" };
            yield return new CountryData { IsoCode = "hu", Name = "Hungary" };
            yield return new CountryData { IsoCode = "id", Name = "Indonesia" };
            yield return new CountryData { IsoCode = "ie", Name = "Ireland" };
            yield return new CountryData { IsoCode = "il", Name = "Israel" };
            yield return new CountryData { IsoCode = "in", Name = "India" };
            yield return new CountryData { IsoCode = "it", Name = "Italy" };
            yield return new CountryData { IsoCode = "jp", Name = "Japan" };
            yield return new CountryData { IsoCode = "kr", Name = "Korea" };
            yield return new CountryData { IsoCode = "lt", Name = "Lithuania" };
            yield return new CountryData { IsoCode = "lu", Name = "Luxembourg" };
            yield return new CountryData { IsoCode = "lv", Name = "Latvia" };            
            yield return new CountryData { IsoCode = "mt", Name = "Malta" };
            yield return new CountryData { IsoCode = "mx", Name = "Mexico" };
            yield return new CountryData { IsoCode = "my", Name = "Malaysia" };
            yield return new CountryData { IsoCode = "nl", Name = "Netherlands" };
            yield return new CountryData { IsoCode = "no", Name = "Norway" };
            yield return new CountryData { IsoCode = "nz", Name = "New Zealand" };            
            yield return new CountryData { IsoCode = "pa", Name = "Panama" };
            yield return new CountryData { IsoCode = "pe", Name = "Peru" };
            yield return new CountryData { IsoCode = "ph", Name = "Philippines" };
            yield return new CountryData { IsoCode = "pl", Name = "Poland" };
            yield return new CountryData { IsoCode = "pt", Name = "Portugal" };
            yield return new CountryData { IsoCode = "ro", Name = "Romania" };
            yield return new CountryData { IsoCode = "sa", Name = "Saudi Arabia" };
            yield return new CountryData { IsoCode = "rs", Name = "Serbia" };
            yield return new CountryData { IsoCode = "qa", Name = "Qatar" };
            yield return new CountryData { IsoCode = "kw", Name = "Kuwait" };
            yield return new CountryData { IsoCode = "bh", Name = "Bahrain" };
            yield return new CountryData { IsoCode = "om", Name = "Oman" };
            yield return new CountryData { IsoCode = "ru", Name = "Russia" };
            yield return new CountryData { IsoCode = "se", Name = "Sweden" };
            yield return new CountryData { IsoCode = "sg", Name = "Singapore" };
            yield return new CountryData { IsoCode = "si", Name = "Slovenia" };
            yield return new CountryData { IsoCode = "sk", Name = "Slovakia" };
            yield return new CountryData { IsoCode = "th", Name = "Thailand" };
            yield return new CountryData { IsoCode = "tr", Name = "Turkey" };
            yield return new CountryData { IsoCode = "tw", Name = "Taiwan" };
            yield return new CountryData { IsoCode = "ua", Name = "Ukraine" };
            yield return new CountryData { IsoCode = "ae", Name = "United Arab Emirates" };
            yield return new CountryData { IsoCode = "us", Name = "United States" };
            yield return new CountryData { IsoCode = "uy", Name = "Uruguay" };
            yield return new CountryData { IsoCode = "vn", Name = "Vietnam" };
            yield return new CountryData { IsoCode = "za", Name = "South Africa" };

        }
        #endregion
    }
}
