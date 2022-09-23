using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TablesLibrary.Interpreter.Table;
using WpfApp.Models;

namespace WpfApp.Presenters
{
    public class AutosaveSettingsPresenter
    {
        private readonly ProgramSettings settings;
        private double _selectedTimer;

        public List<double> Timers { get; set; }
        public double SelectedTimer 
        {
            get => _selectedTimer;
            set
            {
                _selectedTimer = value;
                settings.SaveTimer.Interval = TimeSpan.FromSeconds(value);
            }
        }
        public bool IsAutosaveEnabled
        {
            get => settings.IsSaveTimerEnabled;
            set => settings.IsSaveTimerEnabled = value;
        }

        public AutosaveSettingsPresenter(ProgramSettings settings)
        {
            this.settings = settings;

            Timers = new List<double>();
            Timers.Add(10);
            Timers.Add(15);
            Timers.Add(30);
            Timers.Add(60);
            Timers.Add(360);
            Timers.Add(600);

            _selectedTimer = settings.SaveTimer.Interval.TotalSeconds;
        }
    }
}
