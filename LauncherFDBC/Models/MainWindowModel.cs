using BO_Launcher;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace LauncherFDBC.Models
{
    public class MainWindowModel
    {
        public MainWindowModel()
        {
            Formatter = new XmlSerializer(typeof(ProgramBO));
            LauncherPath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            LauncherUpdaterPath = LauncherPath + "\\LauncherUpdater.exe";
            LauncherProgPath = LauncherPath + "\\LauncherFDBC.exe";
            SettingPath = LauncherPath + "\\upversion.xml";
            FdbcPath = LauncherPath + "\\FilmsDBC";
            FdbcProgPath = FdbcPath + "\\WpfApp.exe";
            LocalUpdate = new ProgramBO();
        }

        public string LauncherUpdaterPath { get; private set; }
        public string LauncherProgPath { get; private set; }
        public string LauncherPath { get; private set; }
        public XmlSerializer Formatter { get; private set; }
        public string SettingPath { get; private set; }
        public ProgramBO LocalUpdate { get; set; }
        public string FdbcPath { get; private set; }
        public string FdbcProgPath { get; private set; }
    }
}
