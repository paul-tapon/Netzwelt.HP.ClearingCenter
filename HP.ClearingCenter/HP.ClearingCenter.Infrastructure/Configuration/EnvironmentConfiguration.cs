using System.Configuration;
using HP.ClearingCenter.Infrastructure.Helpers;

namespace HP.ClearingCenter.Infrastructure.Configuration
{
    public class EnvironmentConfiguration : ConfigurationElement
    {
        const string ENVIRONMENT_KEY = "environment";
        const string APPSETTINGS_KEY = "appSettings";
        const string MAILSUBJECTPREFIX_KEY = "mailSubjectPrefix";
        const string DEFAULTEMAILRECIPIENT_KEY = "defaultEmailRecipient";
        const string DATABASECONNECTIONSTRINGNAME_KEY = "databaseConnectionStringName";

        static EnvironmentConfiguration()
        {
            lock (_syncLock)
            {
                Current = GetEnvironmentConfiguration(string.Empty);
            }
        }

        public static EnvironmentConfiguration GetEnvironmentConfiguration(string key)
        {
            var configSection = ClearingCenterConfigurationSectionHandler.GetInstance();
            string selectedEnvironment = key.IsNullOrEmpty() ? configSection.ActiveEnvironment : key;
            return configSection.Environments[selectedEnvironment].InitializeWith(configSection);
        }

        private EnvironmentConfiguration InitializeWith(ClearingCenterConfigurationSectionHandler configSection)
        {
            _configSection = configSection;
            return this;
        }

        private ClearingCenterConfigurationSectionHandler _configSection;

        public static EnvironmentConfiguration Current { get; private set; }

        static object _syncLock = new object();


        [ConfigurationProperty(ENVIRONMENT_KEY, IsRequired = true)]
        public string Environment
        {
            get
            {
                return this[ENVIRONMENT_KEY].ToString();
            }
        }

        [ConfigurationProperty(DATABASECONNECTIONSTRINGNAME_KEY, IsRequired = true)]
        public string DatabaseConnectionStringName
        {
            get
            {
                return this[DATABASECONNECTIONSTRINGNAME_KEY].ToString();
            }
        }

        [ConfigurationProperty(DEFAULTEMAILRECIPIENT_KEY, DefaultValue = "sangabriel@greenova.de")]
        public string DefaultEmailRecipient
        {
            get
            {
                return this[DEFAULTEMAILRECIPIENT_KEY].ToString();
            }
        }

        [ConfigurationProperty(MAILSUBJECTPREFIX_KEY, DefaultValue = "")]
        public string MailSubjectPrefix
        {
            get
            {
                return this[MAILSUBJECTPREFIX_KEY].ToString();
            }
        }

        [ConfigurationProperty(APPSETTINGS_KEY)]
        public AppSettingCollection AppSettings
        {
            get
            {
                return (AppSettingCollection)this[APPSETTINGS_KEY];
            }
        }


    }
}
