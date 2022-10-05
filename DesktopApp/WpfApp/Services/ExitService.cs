using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp.Views;

namespace WpfApp.Services
{
    public class ExitService
    {
        private ExitWindow window;
        public bool? ShowDialog()
        {
            window = new ExitWindow();
            return window.ShowDialog();
        }
        public bool Save => window.Save;
        public bool Close => window.CloseProg;
    }
}
