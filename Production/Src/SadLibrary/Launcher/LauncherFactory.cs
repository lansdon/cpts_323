using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public enum LauncherType
{
    LAUNCH_TYPE_MOCK, LAUCH_TYPE_USB
}

namespace SadLibrary.Launcher
{

    public class LauncherFactory
    {
        // Make a launcher! Any kind you want, as long as it's one of the two we support.
        public static ILauncher NewLauncher(LauncherType type)
        {
            if ( type == LauncherType.LAUCH_TYPE_USB )
                return new missileLauncher();
            else if ( type == LauncherType.LAUNCH_TYPE_MOCK )
                return new mockLauncher();
            else
                return null;
        }
    }
}
