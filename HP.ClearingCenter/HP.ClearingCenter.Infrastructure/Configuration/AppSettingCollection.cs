using System.Configuration;

namespace HP.ClearingCenter.Infrastructure.Configuration
{
    public class AppSettingCollection : ConfigurationElementCollection
    {

        public new AppSetting this[string key]
        {
            get
            {
                return (AppSetting)base.BaseGet(key);
            }
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new AppSetting();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((AppSetting)element).Key;
        }
    }
}
