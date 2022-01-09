using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfApp.Visual.HelpWindows.LogOutWindow
{
    /// <summary>
    /// Логика взаимодействия для LogOutWindow.xaml
    /// </summary>
    public partial class LogOutWindow : Window
    {
        public LogOutWindow()
        {
            InitializeComponent();
        }


        public bool LogOut { get; private set; }
        private void btn_StayLogIn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btn_LogOut_Click(object sender, RoutedEventArgs e)
        {
            LogOut = true;
            this.Close();
        }

        private void btn_close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ReleaseCapture();
            SendMessage(new WindowInteropHelper(this).Handle, 0x112, 0xf012, 0);
        }
    }
}
