using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp.MVP.Models;
using WpfApp.MVP.Views;
using WpfApp.MVP.ViewsInterface;

namespace WpfApp.MVP.Presenters
{
    public class SettingsMenuPresenter
    {
        private readonly MainWindowModel mainModel;
        private readonly SettingsMenuModel model;
        private readonly ISettingsMenuView view;
        public SettingsMenuPresenter(SettingsMenuModel model, ISettingsMenuView view, MainWindowModel mainModel)
        {
            this.mainModel = mainModel;
            this.model = model;
            this.view = view;
        }

        public void SendToDB()
        {
            mainModel.Settings.Profiles.SendProfilesToDB(mainModel.LoggedInUser);
        }

        public void GetFromDB()
        {
            mainModel.Settings.Profiles.GetProfilesFromDB(mainModel.LoggedInUser);
            mainModel.TableCollection.LoadTables();
        }

        public void InitializeSettingsPanel()
        {
            view.SettingsList.Add(new ProfileContainerView(mainModel.Settings));
        }
    }
}
