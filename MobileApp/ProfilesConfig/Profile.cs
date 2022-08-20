using BO_Films;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ProfilesConfig
{
	[Serializable]
	public class Profile
	{
		internal Profile()
		{
			this.name = "Main";
		}
		internal Profile(String name)
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

		public ProfileCollection ParentCollection { get; internal set; }

		public String ProfilePath
		{
			get 
			{
				string export = Path.Combine(ParentCollection.ProfilesPath, name);
				Directory.CreateDirectory(export);
				return export;
			} 
		}

		private String FilePath => Path.Combine(ProfilePath, "Films.fdbc");

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

		public bool RenameProfile(String newName)
		{
			bool export = true;

			foreach (Profile prof in ParentCollection.GetAllProfiles)
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
