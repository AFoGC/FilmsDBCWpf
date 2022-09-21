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
        private readonly DispatcherTimer saveTimer;

        public MainWindowPresenter(MainWindowModel model, IMainWindowView view)
        {
            this.model = model;
            this.view = view;

            saveTimer = new DispatcherTimer();
            saveTimer.Interval = TimeSpan.FromSeconds(10);
            saveTimer.Tick += OnTimerSave;

            model.TableCollection.TableSave += OnSaveStatus;
            model.TableCollection.CellInTablesChanged += OnTablesChanged;
        }

        private void OnTimerSave(object sender, EventArgs e)
        {
            model.TableCollection.SaveTables();
            saveTimer.Stop();
        }

        private void OnTablesChanged(object sender, EventArgs e)
        {
            StatusInfo status = StatusInfo.GetInfo(StatusEnum.UnSaved, view);
            view.Status = status;
            saveTimer.Stop();
            saveTimer.Start();
        }

        private void OnSaveStatus(object sender, EventArgs e)
        {
            StatusInfo status = StatusInfo.GetInfo(StatusEnum.Saved, view);
            view.Status = status;
            saveTimer.Stop();
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
