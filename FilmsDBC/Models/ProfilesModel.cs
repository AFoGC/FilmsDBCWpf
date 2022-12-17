using System.Collections.Generic;
using WpfApp.Services;

namespace WpfApp.Models
{
    public class ProfilesModel
    {
        private readonly ProfilesService _profilesService;
        private readonly TablesService _tablesService;

        public IEnumerable<ProfileModel> Profiles => _profilesService.Profiles;
        public ProfileModel UsedProfile => _profilesService.UsedProfile;

        public ProfilesModel(TablesService tablesFileService, ProfilesService profilesService)
        {
            _profilesService = profilesService;
            _tablesService = tablesFileService;
            _profilesService.UsedProfileChanged += OnProfileChange;
        }

        private void OnProfileChange()
        {
            _tablesService.LoadTables(UsedProfile.Name);
        }

        public bool SetUsedProfile(ProfileModel profile)
        {
            return _profilesService.SetUsedProfile(profile);
        }

        public bool AddProfile(string profileName)
        {
            return _profilesService.AddProfile(profileName);
        }

        public bool RemoveProfile(ProfileModel profile)
        {
            return _profilesService.RemoveProfile(profile);
        }

        public void ImportProfile(string filePath)
        {
            _profilesService.ImportProfile(filePath);
        }
    }
}
