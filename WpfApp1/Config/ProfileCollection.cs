using System;
using System.Collections.Generic;
using System.Text;

namespace WpfApp.Config
{
	public class ProfileCollection
	{
		private List<Profile> profiles = null;

		public ProfileCollection()
		{
			profiles = new List<Profile>();
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

		public void LoadProfiles()
		{
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
	}
}
