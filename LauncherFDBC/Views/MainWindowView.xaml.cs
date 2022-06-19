using LauncherFDBC.Models;
using LauncherFDBC.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LauncherFDBC.Views
{
	/// <summary>
	/// Логика взаимодействия для MainWindow.xaml
	/// </summary>
	public partial class MainWindowView : Window
	{
        public MainWindowView()
		{
			InitializeComponent();
			DataContext = new MainViewModel();
		}

		private void btn_minimize_Click(object sender, RoutedEventArgs e)
		{
			Application.Current.MainWindow.WindowState = WindowState.Minimized;
		}

		private void btn_close_Click(object sender, RoutedEventArgs e)
		{
			App.Current.MainWindow.Close();
		}


		[DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
		private extern static void ReleaseCapture();
		[DllImport("user32.DLL", EntryPoint = "SendMessage")]
		private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);
		private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
		{
			ReleaseCapture();
			SendMessage(Process.GetCurrentProcess().MainWindowHandle, 0x112, 0xf012, 0);
		}
	}
}
