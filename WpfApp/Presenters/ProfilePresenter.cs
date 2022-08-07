using System;
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
        private readonly ProfileCollectionModel profiles;
        private readonly ProfileContainerPresenter parentPresenter;

        public ProfilePresenter(ProfileModel model, IProfileView view, ProfileCollectionModel profiles, ProfileContainerPresenter parentPresenter)
        {
            this.model = model;
            this.view = view;
            this.profiles = profiles;
            this.parentPresenter = parentPresenter;
            SetSelected(profiles.UsedProfile);
        }

        public void SetSelected(ProfileModel usedProfile)
        {
            if (usedProfile == model)
                view.SetVisualSelected();
            else view.SetVisualDefault();
        }

        public void ChangeProfile()
        {
            profiles.SetUsedProfile(model);
            parentPresenter.TableCollection.TableFilePath = profiles.UsedProfile.MainFilePath;
            parentPresenter.TableCollection.LoadTables();
            parentPresenter.SetSelectedInfCollection(profiles.UsedProfile);
        }

        public void DeleteThisProfile()
        {
            if (model != profiles.UsedProfile)
            {
                profiles.RemoveProfile(model);
                parentPresenter.RefreshControl();
            }
        }
    }
}
