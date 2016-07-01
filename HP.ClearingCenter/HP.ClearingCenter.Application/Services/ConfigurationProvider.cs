using HP.ClearingCenter.Infrastructure.Configuration;
using HP.ClearingCenter.Infrastructure.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HP.ClearingCenter.Application.Services
{
    public class ConfigurationProvider : IConfigurationProvider
    {
        public ConfigurationProvider()
        {
            this.SessionData = new SessionData();
        }
        
        public virtual bool IsShortcutEditModeEnabled { get; private set; }

        public virtual SessionData SessionData { get; private set; }
    }
}
