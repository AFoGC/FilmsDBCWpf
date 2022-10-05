using BL_Films;
using BO_Films;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using TablesLibrary.Interpreter;

namespace WpfApp.Models
{
	public class ProfileCollection : IEnumerable, INotifyPropertyChanged, INotifyCollectionChanged
	{
        public event PropertyChangedEventHandler PropertyChanged;
		public event NotifyCollectionChangedEventHandler CollectionChanged;

		private ObservableCollection<Profile> profiles = null;
		private Profile usedProfile = null;

		public Profile UsedProfile
		{
			get
			{
				return usedProfile;
			}
			set
			{
                bool hasInCollection = false;
                UsedProfile.IsSelected = false;
                if (value != null)
                {
                    foreach (Profile item in profiles)
                    {
                        if (item.Name == value.Name)
                        {
                            usedProfile = item;
                            hasInCollection = true;
                            break;
                        }
                    }
                }
                if (!hasInCollection)
                {
                    usedProfile = profiles[0];
                }
                UsedProfile.IsSelected = true;
				OnPropertyChanged();
            }
		}

		public void SetUsedProfile(String profileName)
		{
			Profile profile = profiles.Where(o => o.Name == profileName).FirstOrDefault();
			UsedProfile = profile;
		}

		public String ProfilesPath { get; private set; }

		public ProfileCollection(string path)
		{
			profiles = new ObservableCollection<Profile>();
			ProfilesPath = Path.Combine(path, "Profiles");
			Directory.CreateDirectory(ProfilesPath);

			profiles.CollectionChanged += ProfilesChanged;

			LoadProfiles();
			usedProfile = profiles[0];
		}

		private void ProfilesChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
            NotifyCollectionChangedEventHandler handler = CollectionChanged;
            if (handler != null)
                handler(this, e);
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
			if (AddProfile(profile))
			{
				return profile;
			}
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

		public IEnumerator GetEnumerator() => profiles.GetEnumerator();

        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var e = new PropertyChangedEventArgs(propertyName);
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
                handler(this, e);
        }
	}
}
