using LauncherFDBC.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LauncherFDBC.Commands
{
    public class StartCommand : BaseCommand
    {
        private readonly MainWindowModel model;
        public StartCommand(MainWindowModel model)
        {
            this.model = model;
        }

        public override bool CanExecute(object parameter)
        {
            return base.CanExecute(parameter);
        }

        public override void Execute(object parameter)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = model.FdbcProgPath
            };

            System.Windows.Application.Current.Shutdown();
            Process.Start(startInfo);
        }
    }
}
