using BL_Films;
using BO_Films;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using TablesLibrary.Interpreter;
using TablesLibrary.Interpreter.Table;

namespace ProfilesConfig
{
	public class ProfileCollection : IEnumerable
	{
		private List<Profile> profiles = null;
		private Profile usedProfile = null;
		public Profile UsedProfile
		{
			get
			{
				return usedProfile;
			}
		}

		public void SetUsedProfile(Profile profile)
        {
			bool hasInCollection = false;
			foreach (Profile item in profiles)
			{
				if (item.Name == profile.Name)
				{
					usedProfile = item;
					hasInCollection = true;
				}
			}
			if (!hasInCollection)
			{
				usedProfile = profiles[0];
			}
		}

		public void SetUsedProfile(String profileName)
        {
			bool hasInCollection = false;
			foreach (Profile item in profiles)
			{
				if (item.Name == profileName)
				{
					usedProfile = item;
					hasInCollection = true;
				}
			}
			if (!hasInCollection)
			{
				usedProfile = profiles[0];
			}
		}

		public String ProfilesPath { get; private set; }

		public ProfileCollection(string path)
		{
			profiles = new List<Profile>();
			ProfilesPath = Path.Combine(path, "Profiles");
			LoadProfiles();
			usedProfile = profiles[0];
		}

		public Profile[] ToArray()
		{
			return profiles.ToArray();
		}

		public Profile this[int index]
		{
			get { return profiles[index]; }
		}

		private Profile GetProfileToUsed(String name)
		{
			foreach (Profile prof in profiles)
			{
				if (prof.Name == name)
				{
					return prof;
				}
			}

			return profiles[0];
		}

		private Profile GetProfileToUsed(Profile profile)
		{
			foreach (Profile prof in profiles)
			{
				if (prof.Name == profile.Name)
				{
					return prof;
				}
			}

			return profiles[0];
		}

		public void LoadProfiles()
		{
			profiles.Clear();
			foreach (Profile profile in GetAllProfiles)
			{
				profiles.Add(profile);
				profile.ParentCollection = this;
			}
		}

		public bool HasProfileName(string name)
		{
			foreach (Profile profile in this)
			{
				if (profile.Name == name) return true;
			}
			return false;
		}

		public Profile AddProfile(String name)
        {
			Profile profile = new Profile(name);
			if (AddProfile(profile)) return profile;
			else return profile;

        }

		public bool AddProfile(Profile newProfile)
		{
			bool exclusive = true;
			foreach (Profile prof in this)
			{
				if (prof.Name == newProfile.Name) exclusive = false;
			}

			if (exclusive)
			{
				Directory.CreateDirectory(newProfile.ProfilePath);
				using (FileStream fs = File.Create(newProfile.MainFilePath)) { }

				TableCollection tc = TLTables.GetDefaultTableCollectionData();
				tc.TableFilePath = newProfile.MainFilePath;

				tc.SaveTables();

				newProfile.ParentCollection = this;
				profiles.Add(newProfile);
			}

			return exclusive;
		}

		public void AddProfiles(Profile[] import)
		{
			foreach (Profile prof in import)
			{
				AddProfile(prof);
			}
		}

		public void RemoveProfile(Profile import)
		{
			Directory.Delete(import.ProfilePath, true);
			profiles.Remove(import);
		}

		public void GetProfilesFromDB(UserBO user)
		{
			ProfileBO[] DBProfiles = ProfileBL.GetAllUserProfiles(user);

			while (profiles.Count > 0)
			{
				profiles.Remove(profiles[0]);
			}

			foreach (ProfileBO profileBO in DBProfiles)
			{
				Profile profile = new Profile(profileBO.Name);
				AddProfile(profile);
				profile.SetMainFile(profileBO.Lastsave);
			}
		}

		public Profile[] GetAllProfiles
		{
			get
			{
				List<Profile> export = new List<Profile>();
				if (Directory.Exists(ProfilesPath))
				{
					foreach (String str in Directory.GetDirectories(ProfilesPath))
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
					Directory.CreateDirectory(ProfilesPath + "\\Main");
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

		public void SendProfilesToDB(UserBO user)
		{
			ProfileBL.SendProfiles(ProfilesToProfileBO(), user);
		}

		public ProfileBO[] ProfilesToProfileBO(UserBO user)
        {
			ProfileBO[] DBProfiles = new ProfileBO[profiles.Count];
			int i = 0;

			foreach (Profile profile in profiles)
			{
				DBProfiles[i++] = profile.ToProfileBO(user);
			}

			return DBProfiles;
		}

		public ProfileBO[] ProfilesToProfileBO()
        {
			ProfileBO[] DBProfiles = new ProfileBO[profiles.Count];
			int i = 0;

			foreach (Profile profile in profiles)
			{
				DBProfiles[i++] = profile.ToProfileBO();
			}

			return DBProfiles;
		}

		public bool Contains(Profile profile) => profiles.Contains(profile);

        public IEnumerator GetEnumerator()
        {
			return new ProfileEnum(profiles);
        }

        private class ProfileEnum : IEnumerator
        {
			IEnumerator enumerator;
			public ProfileEnum(List<Profile> profiles)
            {
				enumerator = profiles.GetEnumerator();
            }

			public object Current => enumerator.Current;

            public bool MoveNext()
            {
				return enumerator.MoveNext();
            }

            public void Reset()
            {
				enumerator.Reset();
            }
        }
    }
}
