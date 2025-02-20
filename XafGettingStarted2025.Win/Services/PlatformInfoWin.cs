using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XafGettingStarted2025.Module.Services;

namespace XafGettingStarted2025.Win.Services
{
    public class PlatformInfoWin: PlatformInfo, IPlatformInfo
    {
        
        public PlatformInfoWin()
        {
            
        }

        public override string GetPlatformName()
        {
            return "Windows";
        }
    }
}
