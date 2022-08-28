using BL_Launcher;
using BO_Launcher;
using Caliburn.Micro;
using LauncherFDBC.Commands;
using LauncherFDBC.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace LauncherFDBC.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string _updateID;
        private bool _isUpdateButtonEnabled;
        private bool _hasProgramUpdate;


        public bool HasProgramUpdate
        {
            get => _hasProgramUpdate;
            set { _hasProgramUpdate = value; OnPropertyChanged(nameof(HasProgramUpdate)); }
        }
        public bool IsProgramFileExist
        {
            get => Model.IsProgramFileExist;
            set { Model.IsProgramFileExist = value; OnPropertyChanged(nameof(IsProgramFileExist)); }
        }

        public bool IsUpdateButtonEnabled
        {
            get => _isUpdateButtonEnabled;
            set { _isUpdateButtonEnabled = value; OnPropertyChanged(nameof(IsUpdateButtonEnabled)); }
        }
        
        public string UpdateID
        {
            get => _updateID;
            set { _updateID = value; OnPropertyChanged(nameof(UpdateID)); }
        }

        public MainWindowModel Model { get; private set; }
        public BindableCollection<ProgramBO> Patches { get; set; }
        public ICommand UpdateProgramCommand { get; private set; }
        public ICommand UpdateLauncherCommand { get; private set; }
        public ICommand StartCommand { get; private set; }
        public ICommand LoadPatches { get; private set; }

        public MainViewModel()
        {
            Model = new MainWindowModel();
            UpdateProgramCommand = new ProgramUpdateCommand(this);
            UpdateLauncherCommand = new LauncherUpdateCommand(this);
            StartCommand = new StartCommand(this);
            LoadPatches = new PatchesCommand(this);
            
            UpdateID = Model.GetFileVersion();

            Patches = new BindableCollection<ProgramBO>(Model.GetPatches());
        }
        
        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
