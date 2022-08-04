using BL_Launcher;
using BO_Launcher;
using LauncherFDBC.Models;
using LauncherFDBC.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LauncherFDBC.Commands
{
    public class LauncherUpdateCommand : BaseCommand
    {
        private readonly MainWindowModel model;
        private readonly MainViewModel vm;
        public LauncherUpdateCommand(MainViewModel vm)
        {
            this.vm = vm;
            this.model = vm.Model;
        }

        public override bool CanExecute(object parameter)
        {
            if (!ProgramBL.IsDBOnline()) return false;
            if (IsLauncherVersionEqual()) return false;

            return true;
        }

        public override void Execute(object parameter)
        {
            if (!IsUpdaterExist()) 
                UpdateUpdater();

            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = model.LauncherUpdaterPath
            };

            System.Windows.Application.Current.Shutdown();
            Process.Start(startInfo);
        }

        public bool IsLauncherVersionEqual()
        {
            string version = LauncherBL.GetLastVersion();
            string localVersion = FileVersionInfo.GetVersionInfo(model.LauncherProgPath).FileVersion;
            return (version == localVersion);
        }

        public bool UpdateUpdater()
        {
            string localVersion = String.Empty;
            if (IsUpdaterExist())
            {
                localVersion = FileVersionInfo.GetVersionInfo(model.LauncherUpdaterPath).FileVersion;
            }
            
            if (UpdaterBL.GetLastVersion() != localVersion)
            {
                UpdaterBO updater = UpdaterBL.GetLastUpdate();
                if (IsUpdaterExist())
                    File.Delete(model.FdbcProgPath);
                File.WriteAllBytes(model.LauncherUpdaterPath, updater.UpdaterFile);
                return true;
            }

            return false;
        }

        public bool IsUpdaterExist()
        {
            return File.Exists(model.LauncherUpdaterPath);
        }
    }
}
