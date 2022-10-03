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
	public class ProfileCollectionModel : IEnumerable, INotifyPropertyChanged, INotifyCollectionChanged
	{
        public event PropertyChangedEventHandler PropertyChanged;
		public event NotifyCollectionChangedEventHandler CollectionChanged;

		private ObservableCollection<ProfileModel> profiles = null;
		private ProfileModel usedProfile = null;

		public ProfileModel UsedProfile
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
                    foreach (ProfileModel item in profiles)
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

		public void SetUsedProfile(ProfileModel profile)
		{
			UsedProfile = profile;
        }

		public void SetUsedProfile(String profileName)
		{
			ProfileModel profile = profiles.Where(o => o.Name == profileName).FirstOrDefault();
			UsedProfile = profile;
		}

		public String ProfilesPath { get; private set; }

		public ProfileCollectionModel(string path)
		{
			profiles = new ObservableCollection<ProfileModel>();
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
