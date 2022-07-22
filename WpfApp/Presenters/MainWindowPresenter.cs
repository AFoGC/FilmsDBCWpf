using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WpfApp.Models;

namespace WpfApp.Presenters
{
    public class MainWindowPresenter
    {
        private readonly MainWindowModel model;

        public MainWindowPresenter(MainWindowModel model)
        {
            this.model = model;
        }

        public bool InfoUnsaved => model.InfoUnsaved;

        public void WindowClosed()
        {
            LanguageHelper.SaveLang();
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
