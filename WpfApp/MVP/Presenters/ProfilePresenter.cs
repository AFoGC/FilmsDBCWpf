﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp.Config;
using WpfApp.MVP.ViewsInterface;

namespace WpfApp.MVP.Presenters
{
    public class ProfilePresenter
    {
        private readonly Profile model;
        private readonly IProfileView view;
        private readonly ProgramSettings settings;
        private readonly ProfileContainerPresenter parentPresenter;

        public ProfilePresenter(Profile model, IProfileView view, ProgramSettings settings, ProfileContainerPresenter parentPresenter)
        {
            this.model = model;
            this.view = view;
            this.settings = settings;
            this.parentPresenter = parentPresenter;
            SetSelected(settings.UsedProfile);
        }

        public void SetSelected(Profile usedProfile)
        {
            if (usedProfile == model)
                view.SetVisualSelected();
            else view.SetVisualDefault();
        }

        public void ChangeProfile()
        {
            settings.UsedProfile = model;
            settings.SaveSettings();
            parentPresenter.SetSelectedInfCollection(settings.UsedProfile);
        }

        public void DeleteThisProfile()
        {
            if (model != settings.UsedProfile)
            {
                settings.Profiles.RemoveProfile(model);
                parentPresenter.RefreshControl();
            }
        }
    }
}
