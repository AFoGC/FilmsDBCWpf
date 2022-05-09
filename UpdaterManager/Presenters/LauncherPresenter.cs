using BL_Launcher;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UpdaterManager.Views;

namespace UpdaterManager.Presenters
{
    public class LauncherPresenter
    {
        private ILauncherView view;
        public LauncherPresenter(ILauncherView view)
        {
            this.view = view;
        }

        public bool SendNewUpdate()
        {
            byte[] file = File.ReadAllBytes(view.LauncherPath);
            byte[] fileServer = LauncherBL.GetLastUpdate().LauncherFile;
            if (Helper.IsEqualVersions(fileServer, file))
            {
                return false;
            }
            else
            {
                LauncherBL.AddUpdate(file);
                return true;
            }
        }
    }
}
