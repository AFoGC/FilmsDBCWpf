using System;
using System.Collections.Generic;
using System.Text;

namespace WpfApp.Config
{
	public class ProfileCollection
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

		public Profile[] Profiles
		{
			get { return profiles.ToArray(); }
		}

		public Profile this[int index]
		{
			get { return profiles[index]; }
		}

		public Profile GetProfileToUsed(String name)
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

		public Profile GetProfileToUsed(Profile profile)
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
			profiles.Remove(import);
		}

		public void GetProfilesFromDB()
        {

        }
	}
}
