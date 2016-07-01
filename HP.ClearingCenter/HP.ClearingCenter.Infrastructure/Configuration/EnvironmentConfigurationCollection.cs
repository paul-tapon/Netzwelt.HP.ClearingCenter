using System.Configuration;

namespace HP.ClearingCenter.Infrastructure.Configuration
{
    public class EnvironmentConfigurationCollection : ConfigurationElementCollection
    {
        public EnvironmentConfiguration this[int index]
        {
            get
            {
                return base.BaseGet(index) as EnvironmentConfiguration;
            }
        }

        public new EnvironmentConfiguration this[string key]
        {
            get
            {
                return base.BaseGet(key) as EnvironmentConfiguration;
            }
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new EnvironmentConfiguration();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((EnvironmentConfiguration)element).Environment;
        }
    }
}
