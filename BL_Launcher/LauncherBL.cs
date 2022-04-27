using BO_Launcher;
using DAL_Launcher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL_Launcher
{
    public static class LauncherBL
    {
        public static void AddUpdate(byte[] programFile)
        {
            LauncherBO program = new LauncherBO
            {
                LauncherFile = programFile
            };
            new LauncherDAL().AddUpdate(program);
        }

        public static LauncherBO GetLastUpdate()
        {
            return new LauncherDAL().GetLastUpdate();
        }
    }
}
