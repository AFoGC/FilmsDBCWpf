using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

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
