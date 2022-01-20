using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace WpfApp.Config
{
	[Serializable]
	public class Profile
	{
		public Profile()
        {
			
        }
		public Profile(String name)
		{
			this.name = name;
		}

		private String name = "";
		public String Name
		{
			get { return name; }
            set { name = value; }
		}

		public static String AllProfilesPath
		{
			get { return System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\Profiles"; }
		}

		public String ProfilePath
		{
			get { return AllProfilesPath + "\\" + name; }
		}

		public String MainFilePath
		{
			get
			{
				String filePath = ProfilePath + "\\Films.fdbc";
				if (File.Exists(filePath))
				{
					// Тут нужно сделать проверку первой строки файла
					//
				}
				else
				{
					using (FileStream fs = File.Create(filePath)) { }

					var tables = MainInfo.Tables.GetDefaultTableCollectionData();
					tables.TableFilePath = filePath;
					tables.SaveTables();

				}

				return filePath;
			}
		}

		public static Profile[] GetAllProfiles
		{
			get
			{
				List<Profile> export = new List<Profile>();
				if (Directory.Exists(AllProfilesPath))
				{
					foreach (String str in Directory.GetDirectories(AllProfilesPath))
					{

						export.Add(new Profile(getDirecotryName(str)));
					}
					if (export.Count != 0)
					{
						return export.ToArray();
					}
				}
				else
				{
					Directory.CreateDirectory(AllProfilesPath + "\\Main");
				}

				export.Add(new Profile("Main"));
				return export.ToArray();
			}
		}

		private static String getDirecotryName(String import)
		{
			int i = import.Length - 1;
			for (; i > 0; i--)
			{
				if (import[i] == '\\')
				{
					return import.Substring(++i);
				}
			}

			return import;
		}

		public bool RenameProfile(String newName)
		{
			bool export = true;

			foreach (Profile prof in GetAllProfiles)
			{
				if (prof.Name == newName) export = false;
			}

			if (export)
			{
				Profile np = new Profile(newName);
				Directory.Move(this.ProfilePath, np.ProfilePath);
				this.name = newName;
			}

			return export;
		}

		public override string ToString()
		{
			return name;
		}
	}
}
