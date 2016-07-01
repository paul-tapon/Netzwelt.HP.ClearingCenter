using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HP.ClearingCenter.Infrastructure.Contracts
{
    public interface IDebugSettings
    {
        bool IsShortcutEditModeEnabled { get; set; }
        bool IsTranslatorCachingEnabled { get; set; }
    }
}
