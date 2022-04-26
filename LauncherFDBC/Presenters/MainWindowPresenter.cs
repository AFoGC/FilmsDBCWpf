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
	public class MainWindowPresenter
	{
		private readonly MainWindowModel model;
		private readonly IMainWindowView view;
		public MainWindowPresenter(MainWindowModel model, IMainWindowView view)
		{
			this.model = model;
			this.view = view;
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

			if (!Directory.Exists(model.FdbcPath))
				Directory.CreateDirectory(model.FdbcPath);

			if (File.Exists(model.FdbcProgPath))
			{
				File.Delete(model.FdbcProgPath);
			}
			File.WriteAllBytes(model.FdbcProgPath, programBO.ProgramFile);


			SaveUpdateInfo();
        }

		public bool IsProgramVersionsEqual()
		{
			if (!File.Exists(model.SettingPath)) 
				return false;
			else LoadUpdateInfo();

			ProgramBO DBUpdate = ProgramBL.GetLastUpdate();

			if (DBUpdate.ID == model.LocalUpdate.ID) 
				return true;
			else return false;
		}

		public void LoadUpdateInfo()
		{
			using (StreamReader fs = new StreamReader(model.SettingPath, Encoding.UTF8))
			{
				model.LocalUpdate = (ProgramBO)model.Formatter.Deserialize(fs);
			}
		}

		public void SaveUpdateInfo()
		{
			using (StreamWriter fs = new StreamWriter(model.SettingPath, false, Encoding.UTF8))
			{
				model.Formatter.Serialize(fs, model.LocalUpdate);
			}
		}

		public void RunProgram()
        {
			ProcessStartInfo startInfo = new ProcessStartInfo
			{
				FileName = model.FdbcProgPath
			};

			Process.Start(startInfo);
			System.Windows.Application.Current.Shutdown();
		}
	}
}
