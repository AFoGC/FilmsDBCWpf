using BL_Launcher;
using BO_Launcher;
using LauncherFDBC.Models;
using LauncherFDBC.Views;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LauncherFDBC.Presenters
{
    public class WindowProgramPresenter 
    {
        private readonly MainWindowModel model;
        private readonly IMainWindowView view;

        public WindowProgramPresenter(MainWindowModel model, IMainWindowView view)
        {
            this.model = model;
            this.view = view;
            try
            {
                view.UpdateID = FileVersionInfo.GetVersionInfo(model.FdbcProgPath).ProductVersion;
            }
            catch { }
        }

        public bool CanBeUpdated()
        {
            if (!ProgramBL.IsDBOnline()) return false;
            if (IsProgramVersionsEqual()) return false;

            return true;
        }

        public void GetUpdateFromDB()
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

            view.UpdateID = FileVersionInfo.GetVersionInfo(model.FdbcProgPath).ProductVersion;
        }

        public void GetPatchesNote()
        {
            List<ProgramBO> programs = ProgramBL.GetPatchNote();
            foreach (ProgramBO item in programs)
            {
                view.UpdateInfo += item.UpdateInfo;
                view.UpdateInfo += "\n\n\n";
            }
        }

        public bool IsProgramVersionsEqual()
        {
            if (!File.Exists(model.FdbcProgPath))
                return false;

            string version = ProgramBL.GetLastVersion();
            string localVersion = FileVersionInfo.GetVersionInfo(model.FdbcProgPath).FileVersion;

            return (version == localVersion);
        }

        public bool IsProgramExist()
        {
            return File.Exists(model.FdbcProgPath);
        }

        public void RunProgram()
        {
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = model.FdbcProgPath
            };

            System.Windows.Application.Current.Shutdown();
            Process.Start(startInfo);
        }
    }
}
