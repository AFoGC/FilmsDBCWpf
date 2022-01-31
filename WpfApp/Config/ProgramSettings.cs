using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;
using TablesLibrary.Interpreter;

namespace WpfApp.Config
{
	[Serializable]
	public class ProgramSettings
	{
		public Profile UsedProfile
		{
			get
			{
				return Profiles.UsedProfile;
			}
			set 
			{
				Profiles.UsedProfile = value;
			}
		}
		internal ProfileCollection Profiles { get; private set; }
		public int MarkSystem { get; set; }
		public StartUserInfo StartUser { get; set; }

		static ProgramSettings()
        {
			formatter = new XmlSerializer(typeof(ProgramSettings));
			settingPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\ProgramSetting.xml";
		}

		private ProgramSettings()
        {
			this.Profiles = new ProfileCollection();
			this.StartUser = new StartUserInfo();
        }

		public static ProgramSettings Initialize()
        {
			ProgramSettings settings = new ProgramSettings();

            if (!File.Exists(settingPath))
            {
				using (StreamWriter fs = new StreamWriter(settingPath, false, Encoding.UTF8))
				{
					formatter.Serialize(fs, settings);
				}
			}

			settings.LoadSettings();

            

			return settings;
        }

		public void SaveSettings()
        {
			using (StreamWriter fs = new StreamWriter(settingPath, false, Encoding.UTF8))
			{
				formatter.Serialize(fs, this);
			}
		}

		public void LoadSettings()
        {
			ProgramSettings settings = new ProgramSettings();
			using (StreamReader fs = new StreamReader(settingPath, Encoding.UTF8))
			{
				settings = (ProgramSettings)formatter.Deserialize(fs);
			}

			copyValues(settings);
		}

		private void copyValues(ProgramSettings settings)
        {
			this.UsedProfile = settings.UsedProfile;
			this.MarkSystem = settings.MarkSystem;
			this.StartUser = settings.StartUser;
        }

		private static readonly String settingPath;
		private static readonly XmlSerializer formatter;

		[Serializable]
		public class StartUserInfo
        {
			public StartUserInfo()
            {
				LoggedIn = false;
				Email = String.Empty;
				Password = String.Empty;
            }
			public Boolean LoggedIn { get; set; }
			public String Email { get; set; }
			public String Password { get; set; }
        }
	}
}
