using BO_Films;
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
			this.name = "Main";
        }
		public Profile(String name)
		{
			if (name == String.Empty)
				this.name = "Main";
			else this.name = name;
			
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

		private String FilePath
        {
			get { return ProfilePath + "\\Films.fdbc"; }
        }

		public String MainFilePath
		{
			get
			{
				if (File.Exists(FilePath))
				{
					// Тут нужно сделать проверку первой строки файла
					//
				}
				else
				{
					using (FileStream fs = File.Create(FilePath)) { }

					var tables = TLTables.GetDefaultTableCollectionData();
					tables.TableFilePath = FilePath;
					tables.SaveTables();

				}

				return FilePath;
			}
		}

		public void SetMainFile(byte[] fileBytes)
        {
            if (File.Exists(FilePath))
            {
				File.Delete(FilePath);
            }
			File.WriteAllBytes(FilePath, fileBytes);
        }

		public Byte[] GetMainFile()
        {
			return File.ReadAllBytes(MainFilePath);
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

		public ProfileBO ToProfileBO()
        {
			ProfileBO profileBO = new ProfileBO();
			profileBO.Name = this.name;
			profileBO.Lastsave = this.GetMainFile();
			return profileBO;
        }

		public ProfileBO ToProfileBO(UserBO user)
        {
			ProfileBO profileBO = ToProfileBO();
			profileBO.UserId = user.Id;
			return profileBO;
        }

		public override string ToString()
		{
			return name;
		}
	}
}
