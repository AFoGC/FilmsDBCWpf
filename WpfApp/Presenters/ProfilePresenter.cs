﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp.Models;
using WpfApp.Views.Interfaces;

namespace WpfApp.Presenters
{
    public class ProfilePresenter
    {
        private readonly ProfileModel model;
        private readonly IProfileView view;
        private readonly ProgramSettings settings;
        private readonly ProfileContainerPresenter parentPresenter;

        public ProfilePresenter(ProfileModel model, IProfileView view, ProgramSettings settings, ProfileContainerPresenter parentPresenter)
        {
            this.model = model;
            this.view = view;
            this.settings = settings;
            this.parentPresenter = parentPresenter;
            SetSelected(settings.Profiles.UsedProfile);
        }

        public void SetSelected(ProfileModel usedProfile)
        {
            if (usedProfile == model)
                view.SetVisualSelected();
            else view.SetVisualDefault();
        }

        public void ChangeProfile()
        {
            settings.Profiles.SetUsedProfile(model);
            settings.SaveSettings();
            parentPresenter.SetSelectedInfCollection(settings.Profiles.UsedProfile);
        }

        public void DeleteThisProfile()
        {
            if (model != settings.Profiles.UsedProfile)
            {
                settings.Profiles.RemoveProfile(model);
                parentPresenter.RefreshControl();
            }
        }
    }
}
