using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using TablesLibrary.Interpreter;

namespace WpfApp.Config
{
	public class ProgramSettings : Cell
	{
		public ProgramSettings()
		{
			profiles.LoadProfiles();

			
			String settingPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\Program.properties";

			Comand comand = new Comand();
			using (System.IO.StreamReader sr = new System.IO.StreamReader(settingPath, System.Text.Encoding.Default))
			{
				comand.getComand(sr.ReadLine());
				if (comand.Paramert == "ProgramSettings")
				{
					this.loadCell(sr, comand);
				}
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

		private int markSystem = 0;
		public int MarkSystem
		{
			get { return markSystem; }
			set { markSystem = value; }
		}

		protected override void updateThisBody(Cell cell)
		{
			throw new NotImplementedException();
		}

		protected override void saveBody(StreamWriter streamWriter)
		{
			streamWriter.Write(FormatParam(nameof(usedProfile), usedProfile.ToString(), "", 1));
		}

		protected override void loadBody(Comand comand)
		{
			switch (comand.Paramert)
			{
				case "usedProfile":
					this.usedProfile = profiles.GetProfileToUsed(comand.Value);
					break;

				default:
					break;
			}
		}
	}
}
