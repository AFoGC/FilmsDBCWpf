using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using WpfApp.Models;
using WpfApp.ViewModels.Interfaces;

namespace WpfApp.ViewModels
{
    public class MainViewModel : BaseViewModel, IMainViewModel
    {
        public MainWindowModel Model { get; private set; }
        private DispatcherTimer SaveTimer => Model.Settings.SaveTimer;

        private StatusInfo _status;
        public StatusInfo Status
        {
            get => _status;
            set { _status = value; }
        }

        public MainViewModel()
        {
            Model = new MainWindowModel();

            SaveTimer.Tick += OnTimerSave;
            Model.TableCollection.TableSave += OnSaveStatus;
            Model.TableCollection.CellInTablesChanged += OnTablesChanged;
        }

        private void OnTimerSave(object sender, EventArgs e)
        {
            Model.TableCollection.SaveTables();
            SaveTimer.Stop();
        }

        private void OnTablesChanged(object sender, EventArgs e)
        {
            StatusInfo status = StatusInfo.GetInfo(StatusEnum.UnSaved, this);
            Status = status;
            if (Model.Settings.IsSaveTimerEnabled)
            {
                SaveTimer.Stop();
                SaveTimer.Start();
            }
        }

        private void OnSaveStatus(object sender, EventArgs e)
        {
            StatusInfo status = StatusInfo.GetInfo(StatusEnum.Saved, view);
            Status = status;
            SaveTimer.Stop();
        }
    }
}
