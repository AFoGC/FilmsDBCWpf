using WpfApp.Services.Interfaces;
using WpfApp.Windows;

namespace WpfApp.Services
{
    public class ExitService : IExitService
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
