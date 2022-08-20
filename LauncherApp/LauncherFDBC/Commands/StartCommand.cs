using LauncherFDBC.Models;
using LauncherFDBC.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LauncherFDBC.Commands
{
    public class StartCommand : BaseCommand
    {
        private readonly MainViewModel vm;
        public StartCommand(MainViewModel vm)
        {
            this.vm = vm;
            vm.UpdateProgramCommand.CanExecuteChanged += UpdateProgramCommand_CanExecuteChanged;
        }

        private void UpdateProgramCommand_CanExecuteChanged(object sender, EventArgs e)
        {
            OnCanExecuteChanged();
        }

        public override bool CanExecute(object parameter)
        {
            return File.Exists(vm.Model.FdbcProgPath);
        }

        public override void Execute(object parameter)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = vm.Model.FdbcProgPath
            };

            System.Windows.Application.Current.Shutdown();
            Process.Start(startInfo);
        }
    }
}
