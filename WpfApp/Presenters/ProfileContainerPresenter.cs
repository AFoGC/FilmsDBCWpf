using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp.Models;
using WpfApp.Services;
using WpfApp.Views;
using WpfApp.Views.Interfaces;

namespace WpfApp.Presenters
{
    public class ProfileContainerPresenter
    {
        private readonly IProfileSettingsContainerView view;
        private readonly ProgramSettings settings;

        public ProfileContainerPresenter(IProfileSettingsContainerView view, ProgramSettings settings)
        {
            this.view = view;
            this.settings = settings;
            RefreshControl();
        }

        public void RefreshControl()
        {
            view.AddProfileText = string.Empty;
            view.ProfileControls.Clear();

            foreach (Profile profile in settings.Profiles)
            {
                view.ProfileControls.Add(new ProflieView(profile, settings, this));
            }
        }

        public void SetSelectedInfCollection(Profile profile)
        {
            foreach (IProfileView profileView in view.ProfileControls)
            {
                profileView.SetSelected(profile);
            }
        }

        public void AddProfile()
        {
            ProfileCollection profileCollection = settings.Profiles;
            if (view.AddProfileText != "")
            {
                profileCollection.AddProfile(view.AddProfileText);
                RefreshControl();
            }
        }

        public void ImportProfile(string filePath)
        {
            ProfileCollection profColl = settings.Profiles;
            int i = 1;
            string profName = "import";
            while (profColl.HasProfileName(profName + i))
            {
                i++;
            }
            profName += i;
            Profile profile = profColl.AddProfile(profName);


            File.Copy(filePath, profile.MainFilePath, true);
        }

        public string AllProfilesPath => settings.Profiles.ProfilesPath;
    }
}
