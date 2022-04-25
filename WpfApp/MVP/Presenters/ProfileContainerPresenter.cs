﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp.Config;
using WpfApp.MVP.Models;
using WpfApp.MVP.Views;
using WpfApp.MVP.ViewsInterface;

namespace WpfApp.MVP.Presenters
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
            view.AddProfileText = String.Empty;
            view.ProfileControls.Clear();
            view.Height = view.DefaultHeight;

            foreach (Profile profile in settings.Profiles)
            {
                view.ProfileControls.Add(new ProflieView(profile, settings, this));
                view.Height += 20;
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
                Profile newProfile = new Profile(view.AddProfileText);
                profileCollection.AddProfile(newProfile);
                this.RefreshControl();
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
            Profile profile = new Profile(profName);
            profColl.AddProfile(profile);

            File.Copy(filePath, profile.MainFilePath, true);
        }
    }
}
