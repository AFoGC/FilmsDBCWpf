using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp.Services
{
    public class ImportFileDialogService : IDialogService
    {
        public string FileName { get; private set; }
        public bool OpenFileDialog()
        {
            var dialog = new Microsoft.Win32.OpenFileDialog();
            dialog.FileName = "Films";
            dialog.DefaultExt = ".fdbc";
            dialog.Filter = "Film documents (.fdbc)|*.fdbc";
            Nullable<bool> result = dialog.ShowDialog();
            if (result == true)
            {
                FileName = dialog.FileName;
                return true;
            }
            else
            {
                return false;
            }
            
        }
    }
}
