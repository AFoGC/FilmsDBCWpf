﻿using System;
using System.Windows.Threading;

namespace WpfApp.Services
{
    public class StatusService
    {
        public event Action<StatusEnum> StatusChanged;

        private readonly TablesFileService _tablesService;

        private DispatcherTimer _statusTimer;
        private StatusEnum _currentStatus;

        public StatusEnum CurrentStatus => _currentStatus;

        public StatusService(TablesFileService tablesService)
        {
            _tablesService = tablesService;
            _currentStatus = StatusEnum.Normal;

            _statusTimer = new DispatcherTimer();
            _statusTimer.Interval = TimeSpan.FromSeconds(2);
            _statusTimer.Tick += StatusTimerTick;

            _tablesService.TablesCollection.TableSave += OnSaveStatus;
            _tablesService.TablesCollection.CellInTablesChanged += OnUnsaveStatus;
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
            if(_statusTimer.IsEnabled)
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
