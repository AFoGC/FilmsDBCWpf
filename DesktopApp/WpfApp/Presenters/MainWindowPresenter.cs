using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Threading;
using WpfApp.Models;
using WpfApp.Views.Interfaces;

namespace WpfApp.Presenters
{
    public class MainWindowPresenter
    {
        private readonly MainWindowModel model;
        private readonly IMainWindowView view;
        private DispatcherTimer SaveTimer => model.Settings.SaveTimer;

        public MainWindowPresenter(MainWindowModel model, IMainWindowView view)
        {
            this.model = model;
            this.view = view;

            model.Settings.SaveTimer.Tick += OnTimerSave;

            model.TableCollection.TableSave += OnSaveStatus;
            model.TableCollection.CellInTablesChanged += OnTablesChanged;
        }

        private void OnTimerSave(object sender, EventArgs e)
        {
            model.TableCollection.SaveTables();
            SaveTimer.Stop();
        }

        private void OnTablesChanged(object sender, EventArgs e)
        {
            StatusInfo status = StatusInfo.GetInfo(StatusEnum.UnSaved, view);
            view.Status = status;
            if (model.Settings.IsSaveTimerEnabled)
            {
                SaveTimer.Stop();
                SaveTimer.Start();
            }
        }

        private void OnSaveStatus(object sender, EventArgs e)
        {
            StatusInfo status = StatusInfo.GetInfo(StatusEnum.Saved, view);
            view.Status = status;
            SaveTimer.Stop();
        }

        public bool InfoUnsaved => model.TableCollection.IsInfoUnsaved;

        public void WindowClosed()
        {
            if (model.IsLoggedIn)
            {
                model.Settings.Profiles.SendProfilesToDB(model.LoggedInUser);
            }
            model.Settings.SaveSettings();
        }

        public void SaveTables()
        {
            model.TableCollection.SaveTables();
        }
    }
}
