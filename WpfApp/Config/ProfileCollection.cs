using BL_Films;
using BO_Films;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace WpfApp.Config
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
            set
            {
				bool hasInCollection = false;
                foreach (Profile profile in profiles)
                {
                    if (profile.Name == value.Name)
                    {
						usedProfile = profile;
						hasInCollection = true;
                    }
                }
                if (!hasInCollection)
                {
					usedProfile = profiles[0];
                }
				MainInfo.TableCollection.TableFilePath = usedProfile.MainFilePath;
				MainInfo.TableCollection.LoadTables();
			}
        }

		public ProfileCollection()
		{
			profiles = new List<Profile>();
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
			foreach (Profile profile in Profile.GetAllProfiles)
			{
				AddProfile(profile);
			}
		}

		public void AddProfile(Profile import)
		{
			profiles.Add(import);
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

		public void SendProfilesToDB(UserBO user)
        {
			ProfileBO[] DBProfiles = new ProfileBO[profiles.Count];
			int i = 0;
			ProfileBO profileBO;

			foreach (Profile profile in profiles)
            {
				profileBO = new ProfileBO();
				profileBO.Name = profile.Name;
				profileBO.Lastsave = profile.GetMainFile();
				profileBO.UserId = user.Id;

				DBProfiles[i++] = profileBO;
            }

			ProfileBL.SendProfiles(DBProfiles, user);
        }

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
