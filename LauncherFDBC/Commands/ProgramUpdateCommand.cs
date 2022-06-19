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
    public class ProgramUpdateCommand : BaseCommand
    {
        private readonly MainWindowModel model;
        private readonly MainViewModel vm;
        public ProgramUpdateCommand(MainViewModel vm)
        {
            this.vm = vm;
            this.model = vm.Model;
        }

        public override bool CanExecute(object parameter)
        {
            if (!ProgramBL.IsDBOnline()) return false;
            if (IsProgramVersionsEqual()) return false;

            return true;
        }

        public override void Execute(object parameter)
        {
            ProgramBO programBO = ProgramBL.GetLastUpdate();
            model.LocalUpdate = programBO;

            if (!IsProgramExist())
                Directory.CreateDirectory(model.FdbcPath);

            if (File.Exists(model.FdbcProgPath))
            {
                File.Delete(model.FdbcProgPath);
            }
            File.WriteAllBytes(model.FdbcProgPath, programBO.ProgramFile);

            vm.UpdateID = FileVersionInfo.GetVersionInfo(model.FdbcProgPath).ProductVersion;
            OnCanExecuteChanged();
        }

        public bool IsProgramExist()
        {
            return File.Exists(model.FdbcProgPath);
        }

        public bool IsProgramVersionsEqual()
        {
            if (!File.Exists(model.FdbcProgPath))
                return false;

            string version = ProgramBL.GetLastVersion();
            string localVersion = FileVersionInfo.GetVersionInfo(model.FdbcProgPath).FileVersion;

            return (version == localVersion);
        }
    }
}
