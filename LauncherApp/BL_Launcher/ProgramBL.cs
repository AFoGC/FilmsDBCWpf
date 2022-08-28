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
    public static class ProgramBL
    {
        public static ProgramBO GetLastUpdate()
        {
            return new ProgramDAL().GetLastUpdate();
        }

        public static void AddUpdate(string updateInfo, byte[] programFile, byte[] zipFile)
        {
            Assembly assembly = Assembly.Load(programFile);
            string version = assembly.GetName().Version.ToString();
            ProgramBO program = new ProgramBO
            {
                UpdateInfo = updateInfo,
                ProgramFile = programFile,
                SubmitDate = DateTime.Now,
                Version = version,
                ZipFile = zipFile
            };
            new ProgramDAL().AddUpdate(program);
        }

        public static List<ProgramBO> GetPatchNote()
        {
            return new ProgramDAL().GetPatchNotes();
        }

        public static List<ProgramBO> GetPatchNote(ProgramBO last_patch)
        {
            return new ProgramDAL().GetNextPatchNotes(last_patch.ID);
        }

        public static string GetLastVersion()
        {
            return new ProgramDAL().GetLastVersion();
        }

        public static bool IsDBOnline() => BaseConnection.IsDatabaseOnline();
    }
}
