using System.Windows;
using WpfApp.Services.Interfaces;

namespace WpfApp.Services
{
    public class ShowMessageService : IMessageService
    {
        public void Show(string message)
        {
            MessageBox.Show(message);
        }
    }
}
