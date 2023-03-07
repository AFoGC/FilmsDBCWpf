using System;

namespace WpfApp.Services
{
    public class StatusService
    {
        public event Action<StatusEnum> StatusChanged;

        private readonly TablesService _tablesService;

        private System.Timers.Timer _statusTimer;
        private StatusEnum _currentStatus;

        public StatusEnum CurrentStatus => _currentStatus;

        public StatusService(TablesService tablesService)
        {
            _tablesService = tablesService;
            _currentStatus = StatusEnum.Normal;

            _statusTimer = new System.Timers.Timer();
            _statusTimer.Interval = 2000;
            _statusTimer.Elapsed += StatusTimerTick;

            _tablesService.TablesCollection.TableSave += OnSaveStatus;
            _tablesService.TablesCollection.CellInTablesChanged += OnUnsaveStatus;
            _tablesService.TablesCollection.TableLoad += OnTableLoad;
        }

        private void OnTableLoad(object sender, EventArgs e)
        {
            SetStatus(StatusEnum.Normal);
        }

        private void OnSaveStatus(object sender, EventArgs e)
        {
            SetStatus(StatusEnum.Saved);
        }

        private void OnUnsaveStatus(object sender, EventArgs e)
        {
            SetStatus(StatusEnum.UnSaved);
        }

        public void SetStatus(StatusEnum status)
        {
            if(_statusTimer.Enabled)
                _statusTimer.Stop();

            if (IsStatusWithTimeDelay(status))
                _statusTimer.Start();

            _currentStatus = status;
            StatusChanged?.Invoke(status);
        }

        private bool IsStatusWithTimeDelay(StatusEnum status)
        {
            if (status == StatusEnum.Saved)
                return true;

            return false;
        }

        private void StatusTimerTick(object sender, EventArgs e)
        {
            _statusTimer.Stop();
            SetStatus(StatusEnum.Normal);
        }
    }
}
