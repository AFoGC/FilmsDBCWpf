using BL_Launcher;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UpdaterManager.Models;
using UpdaterManager.Views;

namespace UpdaterManager.Presenters
{
    public class MainWindowPresenter
    {
        private readonly MainWindowModel model;
        private readonly IMainWindowView view;
        public MainWindowPresenter(IMainWindowView view, MainWindowModel model)
        {
            this.model = model;
            this.view = view;
        }

        public void SendNewUpdate()
        {
            byte[] file = File.ReadAllBytes(view.FilePath);
            ProgramBL.AddUpdate(view.UpdateInfo, file);
        }
    }
}
