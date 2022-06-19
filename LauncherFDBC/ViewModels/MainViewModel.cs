using BL_Launcher;
using BO_Launcher;
using LauncherFDBC.Commands;
using LauncherFDBC.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace LauncherFDBC.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public MainWindowModel Model { get; private set; }
        public event PropertyChangedEventHandler PropertyChanged;

        private string updateInfo;
        public string UpdateInfo
        {
            get => updateInfo;
            set
            {
                updateInfo = value;
                OnPropertyChanged(nameof(UpdateInfo));
            }
        }

        private string updateID;
        public string UpdateID
        {
            get => updateID;
            set
            {
                updateID = value;
                OnPropertyChanged(nameof(UpdateID));
            }
        }

        public string PatchNote { get; set; }
        public ICommand UpdateProgramCommand { get; private set; }
        public ICommand UpdateLauncherCommand { get; private set; }
        public ICommand StartCommand { get; private set; }

        public MainViewModel()
        {
            Model = new MainWindowModel();
            StartCommand = new StartCommand(Model);
            UpdateProgramCommand = new ProgramUpdateCommand(this);
            UpdateLauncherCommand = new LauncherUpdateCommand(this);
            PatchNote = GetPatchNote();
        }

        public string GetPatchNote()
        {
            String export = String.Empty;
            List<ProgramBO> programs = ProgramBL.GetPatchNote();
            foreach (ProgramBO item in programs)
            {
                export += item.UpdateInfo;
                export += "\n\n\n";
            }
            return export;
        }
        
        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
