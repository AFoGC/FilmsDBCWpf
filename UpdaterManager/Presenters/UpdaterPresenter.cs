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
    public class UpdaterPresenter
    {
        private readonly IUpdaterView view;
        public UpdaterPresenter(IUpdaterView view)
        {
            this.view = view;
        }

        public bool SendNewUpdate()
        {
            byte[] file = File.ReadAllBytes(view.UpdaterPath);
            byte[] fileServer = UpdaterBL.GetLastUpdate().UpdaterFile;
            if (Helper.IsEqualVersions(fileServer, file))
            {
                return false;
            }
            else
            {
                UpdaterBL.AddUpdate(file);
                return true;
            }
        }
    }
}
