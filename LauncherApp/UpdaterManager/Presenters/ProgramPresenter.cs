using BL_Launcher;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
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
            byte[] zipFile = null;

            if (view.ZipPath != String.Empty)
            {
                string[] files = Directory.GetFiles(view.ZipPath);
                string zipFilePath = Path.Combine(Path.GetDirectoryName(view.ZipPath), "files.zip");
                if (File.Exists(zipFilePath))
                    File.Delete(zipFilePath);
                ZipFile.CreateFromDirectory(view.ZipPath, zipFilePath);


                if (view.ZipPath != String.Empty)
                {
                    zipFile = File.ReadAllBytes(zipFilePath);
                }
            }
            


            byte[] file = File.ReadAllBytes(view.ProgramPath);
            byte[] fileServer = ProgramBL.GetLastUpdate().ProgramFile;
            if (Helper.IsEqualVersions(fileServer, file))
            {
                return false;
            }
            else
            {
                ProgramBL.AddUpdate(view.UpdateInfo, file, zipFile);
                return true;
            }
        }
    }
}
