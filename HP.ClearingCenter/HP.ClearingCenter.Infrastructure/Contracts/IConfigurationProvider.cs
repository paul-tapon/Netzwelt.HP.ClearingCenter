using HP.ClearingCenter.Infrastructure.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HP.ClearingCenter.Infrastructure.Contracts
{
    public interface IConfigurationProvider
    {
        bool IsShortcutEditModeEnabled { get; }

        SessionData SessionData { get; } 
    }
}
