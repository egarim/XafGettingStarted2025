using System;
using System.Linq;

namespace XafGettingStarted2025.Module.Services
{
    public abstract class PlatformInfo : IPlatformInfo
    {
        public abstract string GetPlatformName();

    }
}
