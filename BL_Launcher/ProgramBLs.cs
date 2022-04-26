using BO_Launcher;
using DAL_Launcher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL_Launcher
{
    public static class ProgramBLs
    {
        public static ProgramBO GetLastUpdate()
        {
            return new ProgramDAL().GetLastUpdate();
        }

        public static void AddUpdate(string updateInfo, byte[] programFile)
        {
            ProgramBO program = new ProgramBO
            { 
                UpdateInfo = updateInfo, 
                ProgramFile = programFile 
            };
            new ProgramDAL().AddUpdate(program);
        }

        public static void IsDBOnline() => BaseConnection.IsDatabaseOnline();
    }
}
