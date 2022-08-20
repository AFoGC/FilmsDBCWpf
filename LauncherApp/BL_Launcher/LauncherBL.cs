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
    public static class LauncherBL
    {
        public static void AddUpdate(byte[] programFile)
        {
            Assembly assembly = Assembly.Load(programFile);
            string version = assembly.GetName().Version.ToString();
            LauncherBO program = new LauncherBO
            {
                LauncherFile = programFile,
                SubmitDate = DateTime.Now,
                Version = version
            };
            new LauncherDAL().AddUpdate(program);
        }

        public static LauncherBO GetLastUpdate()
        {
            return new LauncherDAL().GetLastUpdate();
        }

        public static String GetLastVersion()
        {
            return new LauncherDAL().GetLastVersion();
        }
    }
}
