using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HP.ClearingCenter.Infrastructure.Contracts
{
    public interface IDebugSettingsProvider
    {   
        IDebugSettings GetCurrentSettings();
        void UpdateSettings(IDebugSettings settings);
    }
}
