using System.Configuration;

namespace HP.ClearingCenter.Infrastructure.Configuration
{
    public class AppSetting : ConfigurationElement
    {

        const string KEY = "key";
        const string VALUE = "value";

        [ConfigurationProperty(KEY, IsRequired = true)]
        public string Key
        {
            get
            {
                return this[KEY].ToString();
            }
        }

        public string Value
        {
            get
            {
                return this[VALUE].ToString();
            }
        }
    }
}
