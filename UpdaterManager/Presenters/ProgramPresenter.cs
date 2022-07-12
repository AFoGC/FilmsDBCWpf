using BL_Launcher;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UpdaterManager.Views.Interfaces;

namespace UpdaterManager.Presenters
{
    public class ProgramPresenter
    {
        private IProgramView view;
        public ProgramPresenter(IProgramView view)
        {
            this.view = view;
        }

        public bool SendNewUpdate()
        {
            byte[] file = File.ReadAllBytes(view.ProgramPath);
            byte[] fileServer = ProgramBL.GetLastUpdate().ProgramFile;
            if (Helper.IsEqualVersions(fileServer, file))
            {
                return false;
            }
            else
            {
                ProgramBL.AddUpdate(view.UpdateInfo, file);
                return true;
            }
        }
    }
}
