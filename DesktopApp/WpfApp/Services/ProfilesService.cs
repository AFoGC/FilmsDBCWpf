using System;
using System.Collections;
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

        private ProfileModel _usedProfile;
        private ObservableCollection<ProfileModel> _profiles;

        public IEnumerable<ProfileModel> Profiles => _profiles;
        public ProfileModel UsedProfile => _usedProfile;

        public ProfilesService()
        {
            _profiles = new ObservableCollection<ProfileModel>();
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

        public bool AddProfile(string profileName)
        {
            if (_profiles.Any(p => p.Name == profileName) == false)
            {
                _profiles.Add(new ProfileModel(profileName));
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

            File.Copy(filePath, PathHelper.GetProfileFilePath(profName), true);
        }

        private void LoadProfiles()
        {
            _profiles.Clear();

            string profilesPath = PathHelper.ProfilesPath;
            DirectoryInfo directory = Directory.CreateDirectory(profilesPath);
            DirectoryInfo[] directories = directory.GetDirectories();

            foreach (DirectoryInfo item in directories)
                _profiles.Add(new ProfileModel(item.Name));

            if (directories.Length == 0)
                _profiles.Add(new ProfileModel("Main"));

            _usedProfile = _profiles[0];
        }
    }
}
