using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WpfApp.Models;
using WpfApp.Views.Interfaces;

namespace WpfApp.Presenters
{
    public class MainWindowPresenter
    {
        private readonly MainWindowModel model;
        private readonly IMainWindowView view;

        public MainWindowPresenter(MainWindowModel model, IMainWindowView view)
        {
            this.model = model;
            this.view = view;
        }

        public bool InfoUnsaved => model.TableCollection.IsInfoUnsaved;

        public void WindowClosed()
        {
            if (model.IsLoggedIn)
            {
                model.Settings.Profiles.SendProfilesToDB(model.LoggedInUser);
            }
        }

        public void SaveTables()
        {
            model.TableCollection.SaveTables();
        }
    }
}
