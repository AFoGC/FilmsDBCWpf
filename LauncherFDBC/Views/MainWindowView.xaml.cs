using LauncherFDBC.Models;
using LauncherFDBC.Presenters;
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
	public partial class MainWindowView : Window, IMainWindowView
	{
		WindowLauncherPresenter launcherPresenter;
		WindowProgramPresenter programPresenter;
        public string UpdateInfo { set => updateInfo.Text = value; get => updateInfo.Text; }
        public string UpdateID { set => updateID.Content = value; }
        public MainWindowView()
		{
			InitializeComponent();
			MainWindowModel model = new MainWindowModel();
			launcherPresenter = new WindowLauncherPresenter(model, this);
			programPresenter = new WindowProgramPresenter(model, this);

			

			programPresenter.GetPatchesNote();
			RefreshCanBeUpdated();
			RefreshIsProgExist();
			RefreshLauncherCanBeUpdated();
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			programPresenter.GetUpdateFromDB();
			RefreshCanBeUpdated();
			RefreshIsProgExist();
		}

		private void updateLauncherButton_Click(object sender, RoutedEventArgs e)
		{
			launcherPresenter.GetLauncherUpdateFromDB();
		}

		private void startButton_Click(object sender, RoutedEventArgs e)
		{
			programPresenter.RunProgram();
		}

		private void RefreshCanBeUpdated()
		{
			if (programPresenter.CanBeUpdated())
			{
				updateButton.IsEnabled = true;
			}
			else updateButton.IsEnabled = false;
		}

		private void RefreshLauncherCanBeUpdated()
		{
			if (launcherPresenter.LauncherCanBeUpdated())
				updateLauncherButton.IsEnabled = true;
			else
				updateLauncherButton.IsEnabled = false;
		}

		private void RefreshIsProgExist()
		{
			if (programPresenter.IsProgramExist())
            {
				updateButton.Content = "Update Program";
				startButton.IsEnabled = true;
			}
            else
            {
				updateButton.Content = "Download Program";
				startButton.IsEnabled = false;
			}
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
