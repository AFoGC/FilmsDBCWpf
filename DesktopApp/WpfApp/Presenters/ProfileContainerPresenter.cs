using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TablesLibrary.Interpreter;
using WpfApp.Models;
using WpfApp.Views;
using WpfApp.Views.Interfaces;

namespace WpfApp.Presenters
{
    public class ProfileContainerPresenter
    {
        private readonly IProfileSettingsContainerView view;
        private readonly ProfileCollectionModel model;
        private readonly ProgramSettings settings;

        public ProfileContainerPresenter(IProfileSettingsContainerView view, ProgramSettings settings)
        {
            this.view = view;
            this.settings = settings;
            this.model = settings.Profiles;
            RefreshControl();
        }

        public void RefreshControl()
        {
            view.AddProfileText = string.Empty;
            view.ProfileControls.Clear();

            foreach (ProfileModel profile in model)
            {
                view.ProfileControls.Add(new ProflieView(profile, model, this));
            }
        }

        public void SetSelectedInfCollection(ProfileModel profile)
        {
            foreach (IProfileView profileView in view.ProfileControls)
            {
                profileView.SetSelected(profile);
            }
            settings.SaveSettings();
        }

        public void AddProfile()
        {
            ProfileCollectionModel profileCollection = model;
            if (view.AddProfileText != "")
            {
                profileCollection.AddProfile(view.AddProfileText);
                RefreshControl();
            }
        }

        public void ImportProfile(string filePath)
        {
            ProfileCollectionModel profColl = model;
            int i = 1;
            string profName = "import";
            while (profColl.HasProfileName(profName + i))
            {
                i++;
            }
            profName += i;
            ProfileModel profile = profColl.AddProfile(profName);


            File.Copy(filePath, profile.MainFilePath, true);
        }

        public string AllProfilesPath => model.ProfilesPath;
        public TableCollection TableCollection => settings.TableCollection;
    }
}
