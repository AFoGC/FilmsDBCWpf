using BO_Launcher;
using DAL_Launcher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BL_Launcher
{
    public static class UpdaterBL
    {
        public static void AddUpdate(byte[] updaterFile)
        {
            Assembly assembly = Assembly.Load(updaterFile);
            string version = assembly.GetName().Version.ToString();
            UpdaterBO program = new UpdaterBO
            {
                UpdaterFile = updaterFile,
                SubmitDate = DateTime.Now,
                Version = version
            };
            new UpdaterDAL().AddUpdate(program);
        }

        public static UpdaterBO GetLastUpdate()
        {
            return new UpdaterDAL().GetLastUpdate();
        }

        public static String GetLastVersion()
        {
            return new UpdaterDAL().GetLastVersion();
        }
    }
}
