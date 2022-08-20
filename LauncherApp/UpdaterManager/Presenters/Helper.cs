using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace UpdaterManager.Presenters
{
    public static class Helper
    {
        public static bool IsEqualVersions(byte[] serverFile, byte[] internalFile)
        {
            Assembly serverAss = Assembly.Load(serverFile);
            Assembly internalAss = Assembly.Load(internalFile);
            String serverVersion = serverAss.GetName().Version.ToString();
            String internalVersion = internalAss.GetName().Version.ToString();

            return serverVersion == internalVersion;
        }
    }
}
