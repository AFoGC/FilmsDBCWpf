using BL_Launcher;
using BO_Launcher;
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
        public MainWindowModel Model { get; private set; }
        public event PropertyChangedEventHandler PropertyChanged;

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
        private string updateButtonText;
        public string UpdateButtonText
        {
            get => updateButtonText;
            set
            {
                updateButtonText = value;
                OnPropertyChanged(nameof(UpdateButtonText));
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
            if (File.Exists(Model.FdbcProgPath))
                UpdateID = FileVersionInfo.GetVersionInfo(Model.FdbcProgPath).ProductVersion;
            RefreshButtonString();
            if (ProgramBL.IsDBOnline())
            {
                PatchNote = GetPatchNote();
            }
            UpdateProgramCommand.CanExecuteChanged += UpdateProgramCommand_CanExecuteChanged;
        }

        private void UpdateProgramCommand_CanExecuteChanged(object sender, EventArgs e)
        {
            RefreshButtonString();
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

        public bool ProgramFileExist => File.Exists(Model.FdbcProgPath);

        public void RefreshButtonString()
        {
            string str;
            if (UpdateProgramCommand.CanExecute(null))
                if (File.Exists(Model.FdbcProgPath))
                    str = "Update";
                else str = "Download";
            else str = "Download\nor Update";
            UpdateButtonText = str;
        }
        
        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
