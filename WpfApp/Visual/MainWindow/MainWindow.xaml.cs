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
using System.Windows.Shapes;
using BL_Films;
using BO_Films;
using WpfApp.Visual.HelpWindows.ExitWindow;
using WpfApp.Visual.MainWindow.GlobalElements.Menus.BooksMenu;
using WpfApp.Visual.MainWindow.GlobalElements.Menus.FilmsMenu;
using WpfApp.Visual.MainWindow.GlobalElements.Menus.SettingsMenu;

namespace WpfApp.Visual.MainWindow
{
	/// <summary>
	/// Логика взаимодействия для MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
			MainInfo.TableCollection.TableSave += boolSaved;

			InfoUnsaved = false;
			if (MainInfo.Settings.StartUser.LoggedIn)
			{
				MainInfo.LoggedInUser = BL_Films.UserBL.LogIn(MainInfo.Settings.StartUser.Email, MainInfo.Settings.StartUser.Password);
			}
		}

		public FilmsMenuControl FilmsMenu
		{
			get { return films_menu; }
		}

		public BooksMenuControl BooksMenu
		{
			get { return books_menu; }
		}
		public SettingsMenuControl SettingsMenu
        {
			get { return settings_menu; }
        }
		public Grid Grid { get { return grid; } }
		public Grid Menus { get { return menus; } }

		public Boolean InfoUnsaved { get; set; }
		private void boolSaved(object sender, EventArgs e)
		{
			InfoUnsaved = false;
		}

		private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if (InfoUnsaved)
			{
				ExitWindow exitForm = new ExitWindow();
				exitForm.ShowDialog();

				if (exitForm.Save == true)
				{
					MainInfo.TableCollection.SaveTables();
				}

				e.Cancel = !exitForm.CloseProg;
			}
		}

		private void Window_Closed(object sender, EventArgs e)
		{
			if (MainInfo.IsLoggedIn)
			{
				MainInfo.Settings.Profiles.SendProfilesToDB(MainInfo.LoggedInUser);
			}
		}

		private void Window_KeyDown(object sender, KeyEventArgs e)
        {
			if (e.Key == Key.S && (Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl)))
			{
				MainInfo.TableCollection.SaveTables();
			}
		}
    }
}
