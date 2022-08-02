using BL_Films;
using BO_Films;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TablesLibrary.Interpreter;

namespace WpfApp.Models
{
	public class ProfileCollectionModel : IEnumerable
	{
		private List<ProfileModel> profiles = null;
		private ProfileModel usedProfile = null;
		public ProfileModel UsedProfile
		{
			get
			{
				return usedProfile;
			}
		}

		public void SetUsedProfile(ProfileModel profile)
		{
			bool hasInCollection = false;
			foreach (ProfileModel item in profiles)
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
			foreach (ProfileModel item in profiles)
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

		public ProfileCollectionModel(string path)
		{
			profiles = new List<ProfileModel>();
			ProfilesPath = Path.Combine(path, "Profiles");
			DirectoryInfo info = Directory.CreateDirectory(ProfilesPath);
			LoadProfiles();
			usedProfile = profiles[0];
		}

		public ProfileModel[] ToArray()
		{
			return profiles.ToArray();
		}

		public ProfileModel this[int index]
		{
			get { return profiles[index]; }
		}

		private ProfileModel GetProfileToUsed(String name)
		{
			foreach (ProfileModel prof in profiles)
			{
				if (prof.Name == name)
				{
					return prof;
				}
			}

			return profiles[0];
		}

		private ProfileModel GetProfileToUsed(ProfileModel profile)
		{
			foreach (ProfileModel prof in profiles)
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
			foreach (ProfileModel profile in GetAllProfiles)
			{
				profiles.Add(profile);
				profile.ParentCollection = this;
			}
		}

		public bool HasProfileName(string name)
		{
			foreach (ProfileModel profile in this)
			{
				if (profile.Name == name) return true;
			}
			return false;
		}

		public ProfileModel AddProfile(String name)
		{
			ProfileModel profile = new ProfileModel(name);
			if (AddProfile(profile))
			{
				return profile;
			}
			else return profile;

		}

		public bool AddProfile(ProfileModel newProfile)
		{
			bool exclusive = true;
			foreach (ProfileModel prof in this)
			{
				if (prof.Name == newProfile.Name) exclusive = false;
			}

			if (exclusive)
			{
				newProfile.ParentCollection = this;
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

		public void AddProfiles(ProfileModel[] import)
		{
			foreach (ProfileModel prof in import)
			{
				AddProfile(prof);
			}
		}

		public void RemoveProfile(ProfileModel import)
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
				ProfileModel profile = new ProfileModel(profileBO.Name);
				AddProfile(profile);
				profile.SetMainFile(profileBO.Lastsave);
			}
		}

		public ProfileModel[] GetAllProfiles
		{
			get
			{
				List<ProfileModel> export = new List<ProfileModel>();
				if (Directory.Exists(ProfilesPath))
				{
					foreach (String str in Directory.GetDirectories(ProfilesPath))
					{
						export.Add(new ProfileModel(getDirecotryName(str)));
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

				export.Add(new ProfileModel("Main"));
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

			foreach (ProfileModel profile in profiles)
			{
				DBProfiles[i++] = profile.ToProfileBO(user);
			}

			return DBProfiles;
		}

		public ProfileBO[] ProfilesToProfileBO()
		{
			ProfileBO[] DBProfiles = new ProfileBO[profiles.Count];
			int i = 0;

			foreach (ProfileModel profile in profiles)
			{
				DBProfiles[i++] = profile.ToProfileBO();
			}

			return DBProfiles;
		}

		public bool Contains(ProfileModel profile) => profiles.Contains(profile);

		public IEnumerator GetEnumerator()
		{
			return new ProfileEnum(profiles);
		}

		private class ProfileEnum : IEnumerator
		{
			IEnumerator enumerator;
			public ProfileEnum(List<ProfileModel> profiles)
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
