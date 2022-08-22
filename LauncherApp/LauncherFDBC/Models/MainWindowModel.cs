using BL_Launcher;
using BO_Launcher;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace LauncherFDBC.Models
{
    public class MainWindowModel
    {
        public string LauncherUpdaterPath { get; private set; }
        public string LauncherProgPath { get; private set; }
        public string LauncherPath { get; private set; }
        public string FdbcPath { get; private set; }
        public string FdbcProgPath { get; private set; }

        public MainWindowModel()
        {
            LauncherPath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            LauncherUpdaterPath = Path.Combine(LauncherPath, "LauncherUpdater.exe");
            LauncherProgPath = Path.Combine(LauncherPath, "LauncherFDBC.exe");
            FdbcPath = Path.Combine(LauncherPath, "FilmsDBC");
            FdbcProgPath = Path.Combine(FdbcPath, "WpfApp.exe");

            IsProgramFileExist = File.Exists(FdbcProgPath);
        }

        public bool IsProgramFileExist { get; set; }
        

        public List<ProgramBO> GetPatches()
        {
            if (ProgramBL.IsDBOnline())
            {
                return ProgramBL.GetPatchNote();
            }
            else
            {
                return new List<ProgramBO>();
            }
        }

        public string GetFileVersion()
        {
            String export = String.Empty;
            if (IsProgramFileExist)
                export = FileVersionInfo.GetVersionInfo(FdbcProgPath).ProductVersion;
            return export;
        }
    }
}
