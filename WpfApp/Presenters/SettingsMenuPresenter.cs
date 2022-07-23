using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp.Models;
using WpfApp.Views;
using WpfApp.Views.Interfaces;

namespace WpfApp.Presenters
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
    }
}
