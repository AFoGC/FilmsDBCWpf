using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using WpfApp.Helper;
using WpfApp.Models;

namespace WpfApp.Services
{
    public class ProfilesService
    {
        public event Action UsedProfileChanged;

        private readonly string _profilesPath;
        private ProfileModel _usedProfile;
        private ObservableCollection<ProfileModel> _profiles;

        public IEnumerable<ProfileModel> Profiles => _profiles;
        public ProfileModel UsedProfile => _usedProfile;

        public ProfilesService()
        {
            _profiles = new ObservableCollection<ProfileModel>();
            _profilesPath = PathHelper.ProfilesPath;
            LoadProfiles();
        }

        public bool SetUsedProfile(ProfileModel profile)
        {
            if (Profiles.Contains(profile) == false)
                return false;

            return InnerSetUsedProfile(profile);
        }

        public bool SetUsedProfile(string profileName)
        {
            bool profileChanged = false;
            ProfileModel profile = Profiles.Where(x => x.Name == profileName).FirstOrDefault();

            if (profile != null)
                profileChanged = InnerSetUsedProfile(profile);

            return profileChanged;
        }

        private bool InnerSetUsedProfile(ProfileModel profile)
        {
            _usedProfile = profile;
            UsedProfileChanged?.Invoke();
            return true;
        }

        public ProfileModel GetProfile(string name)
        {
            foreach (ProfileModel profile in _profiles)
                if (profile.Name == name)
                    return profile;

            return null;
        }

        public bool AddProfile(string profileName)
        {
            if (_profiles.Any(p => p.Name == profileName) == false)
            {
                ProfileModel profile = new ProfileModel(profileName, _profilesPath);
                _profiles.Add(profile);

                return true;
            }

            return false;
        }

        public bool RemoveProfile(ProfileModel profile)
        {
            return _profiles.Remove(profile);
        }

        public void ImportProfile(string filePath)
        {
            int i = 1;
            string profName = "import";
            while (AddProfile(profName + i) == false)
            {
                i++;
            }

            ProfileModel profile = GetProfile(profName);
            string profileFilePath = profile.ProfileFilePath;
            File.Copy(filePath, profileFilePath, true);
        }

        private void LoadProfiles()
        {
            _profiles.Clear();

            string profilesPath = _profilesPath;
            DirectoryInfo directory = Directory.CreateDirectory(profilesPath);
            DirectoryInfo[] directories = directory.GetDirectories();

            foreach (DirectoryInfo item in directories)
                AddProfile(item.Name);

            if (directories.Length == 0)
                AddProfile("Main");

            _usedProfile = _profiles[0];
        }
    }
}
