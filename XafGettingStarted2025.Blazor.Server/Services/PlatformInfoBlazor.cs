using XafGettingStarted2025.Module.Services;

namespace XafGettingStarted2025.Blazor.Server.Services
{
    public class PlatformInfoBlazor : PlatformInfo
    {

        public PlatformInfoBlazor()
        {

        }

        public override string GetPlatformName()
        {
            return "Blazor";
        }
    }
}
