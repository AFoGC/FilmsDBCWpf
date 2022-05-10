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
using System.Xml.Serialization;

namespace LauncherFDBC.Presenters
{
	public class WindowLauncherPresenter
	{
		private readonly MainWindowModel model;
		private readonly IMainWindowView view;
		public WindowLauncherPresenter(MainWindowModel model, IMainWindowView view)
		{
			this.model = model;
			this.view = view;
		}

		public bool LauncherCanBeUpdated()
        {
			if (!ProgramBL.IsDBOnline()) return false;
			if (IsLauncherVersionEqual()) return false;

			return true;
		}

		public void GetLauncherUpdateFromDB()
        {
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
			string localVersion = FileVersionInfo.GetVersionInfo(model.LauncherUpdaterPath).FileVersion;
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
