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
		private ProgramSettings()
        {
			this.profiles.LoadProfiles();
        }

		public static ProgramSettings Initialize()
        {
			String settingPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\ProgramSetting.xml";
			XmlSerializer formatter = new XmlSerializer(typeof(ProgramSettings));

			ProgramSettings settings = new ProgramSettings();

            if (!File.Exists(settingPath))
            {
				using (FileStream fs = new FileStream(settingPath, FileMode.OpenOrCreate))
				{
					formatter.Serialize(fs, settings);
				}
			}

			using (FileStream fs = new FileStream(settingPath, FileMode.Open))
			{
				settings = (ProgramSettings)formatter.Deserialize(fs);
			}

			return settings;
        }

		public void SaveSettings()
        {
			String settingPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\ProgramSetting.xml";
			XmlSerializer formatter = new XmlSerializer(typeof(ProgramSettings));
			using (FileStream fs = new FileStream(settingPath, FileMode.OpenOrCreate))
			{
				formatter.Serialize(fs, this);
			}
		}
		

		private Profile usedProfile = null;
		public Profile UsedProfile
		{
			get
			{
				if (usedProfile == null)
				{
					usedProfile = profiles[0];
					return usedProfile;
				}
				return usedProfile;
			}
			set { usedProfile = value; }
		}

		private ProfileCollection profiles = new ProfileCollection();
		public ProfileCollection Profiles
		{
			get { return profiles; }
		}

		public int MarkSystem { get; set; }
	}
}
