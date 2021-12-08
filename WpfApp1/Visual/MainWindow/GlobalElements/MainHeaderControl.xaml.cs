using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp.Visual.MainWindow.GlobalElements
{
    /// <summary>
    /// Логика взаимодействия для MainHeaderControl.xaml
    /// </summary>
    public partial class MainHeaderControl : UserControl
    {
        private Window window;
        public MainHeaderControl()
        {
            InitializeComponent();
            window = App.Current.MainWindow;
        }

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);
        private void mainHeader_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ReleaseCapture();
            SendMessage(Process.GetCurrentProcess().MainWindowHandle, 0x112, 0xf012, 0);
        }

        private void btn_close_Click(object sender, RoutedEventArgs e)
        {
            window.Close();
        }

        
    }
}
