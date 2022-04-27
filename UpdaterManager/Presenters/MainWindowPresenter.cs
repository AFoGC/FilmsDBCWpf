using BL_Launcher;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UpdaterManager.Models;
using UpdaterManager.Views;

namespace UpdaterManager.Presenters
{
    public class MainWindowPresenter
    {
        private readonly MainWindowModel model;
        private readonly IMainWindowView view;
        public MainWindowPresenter(IMainWindowView view, MainWindowModel model)
        {
            this.model = model;
            this.view = view;
        }

        public void SendNewUpdate()
        {
            byte[] file = File.ReadAllBytes(view.FilePath);
            ProgramBL.AddUpdate(view.UpdateInfo, file);
        }

        public void SendNewLauncherUpdate()
        {
            /*
            var versionInfo = FileVersionInfo.GetVersionInfo(view.LauncherFilePath);
            string version = versionInfo.FileVersion;
            byte[] importFile = LauncherBL.GetLastUpdate().LauncherFile;
            Assembly assembly = Assembly.Load(byteArray)
            Dim currentVersion = assembly.GetName.Version
            */

            byte[] file = File.ReadAllBytes(view.LauncherFilePath);
            LauncherBL.AddUpdate(file);
        }
    }
}
