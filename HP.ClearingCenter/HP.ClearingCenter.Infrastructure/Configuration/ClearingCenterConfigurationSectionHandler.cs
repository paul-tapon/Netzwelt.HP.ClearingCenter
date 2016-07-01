using System.Configuration;

namespace HP.ClearingCenter.Infrastructure.Configuration
{
    public class ClearingCenterConfigurationSectionHandler : ConfigurationSection
    {
        const string CLEARINGCENTERCONFIGSECTION_KEY = "clearing-center-settings";
        const string ACTIVEENVIRONMENT_KEY = "activeEnvironment";
        const string ENVIRONMENTS_KEY = "environments";

        public static ClearingCenterConfigurationSectionHandler GetInstance()
        {
            return ConfigurationManager.GetSection(CLEARINGCENTERCONFIGSECTION_KEY) as ClearingCenterConfigurationSectionHandler;
        }

        [ConfigurationProperty(ACTIVEENVIRONMENT_KEY, IsRequired = true)]
        public string ActiveEnvironment
        {
            get { return base[ACTIVEENVIRONMENT_KEY].ToString(); }
            set { base[ACTIVEENVIRONMENT_KEY] = value; }
        }

      
        [ConfigurationProperty(ENVIRONMENTS_KEY, IsRequired = true)]
        public EnvironmentConfigurationCollection Environments
        {
            get
            {
                return base[ENVIRONMENTS_KEY] as EnvironmentConfigurationCollection;
            }
        }

    }
}
