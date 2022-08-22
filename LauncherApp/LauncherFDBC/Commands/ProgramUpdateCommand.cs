using BL_Launcher;
using BO_Launcher;
using LauncherFDBC.Models;
using LauncherFDBC.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LauncherFDBC.Commands
{
    public class ProgramUpdateCommand : BaseCommand
    {
        private readonly MainWindowModel model;
        private readonly MainViewModel vm;
        public ProgramUpdateCommand(MainViewModel vm)
        {
            this.vm = vm;
            this.model = vm.Model;
        }

        public override bool CanExecute(object parameter)
        {
            if (!ProgramBL.IsDBOnline()) return false;
            if (IsProgramVersionsEqual()) return false;

            return true;
        }

        public override void Execute(object parameter)
        {
            ProgramBO programBO = ProgramBL.GetLastUpdate();

            if (!IsProgramExist())
                Directory.CreateDirectory(model.FdbcPath);

            if (File.Exists(model.FdbcProgPath))
                File.Delete(model.FdbcProgPath);

            File.WriteAllBytes(model.FdbcProgPath, programBO.ProgramFile);
            vm.UpdateID = FileVersionInfo.GetVersionInfo(model.FdbcProgPath).ProductVersion;

            downloadAdditionalFiles(programBO.ZipFile);

            OnCanExecuteChanged();
        }

        private void downloadAdditionalFiles(byte[] zipFile)
        {
            if (zipFile != null)
            {
                string zipPath = Path.Combine(model.FdbcPath, "add.zip");
                File.Delete(zipPath);
                File.WriteAllBytes(zipPath, zipFile);

                using (ZipArchive archive = ZipFile.OpenRead(zipPath))
                {
                    ExtractToDirectory(archive, model.FdbcPath, true);
                }

                File.Delete(zipPath);
            }
        }

        public void ExtractToDirectory(ZipArchive archive, string destinationDirectoryName, bool overwrite)
        {
            if (!overwrite)
            {
                archive.ExtractToDirectory(destinationDirectoryName);
                return;
            }

            DirectoryInfo di = Directory.CreateDirectory(destinationDirectoryName);
            string destinationDirectoryFullPath = di.FullName;

            foreach (ZipArchiveEntry file in archive.Entries)
            {
                string completeFileName = Path.GetFullPath(Path.Combine(destinationDirectoryFullPath, file.FullName));

                if (!completeFileName.StartsWith(destinationDirectoryFullPath, StringComparison.OrdinalIgnoreCase))
                {
                    throw new IOException("Trying to extract file outside of destination directory. See this link for more info: https://snyk.io/research/zip-slip-vulnerability");
                }

                if (file.Name == "")
                {// Assuming Empty for Directory
                    Directory.CreateDirectory(Path.GetDirectoryName(completeFileName));
                    continue;
                }
                Directory.CreateDirectory(Path.GetDirectoryName(completeFileName));
                file.ExtractToFile(completeFileName, true);
            }
        }

        public bool IsProgramExist()
        {
            return File.Exists(model.FdbcProgPath);
        }

        public bool IsProgramVersionsEqual()
        {
            if (!File.Exists(model.FdbcProgPath))
                return false;

            string version = ProgramBL.GetLastVersion();
            string localVersion = FileVersionInfo.GetVersionInfo(model.FdbcProgPath).FileVersion;

            return (version == localVersion);
        }
    }
}
