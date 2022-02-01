using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
using TablesLibrary.Interpreter;
using TablesLibrary.Interpreter.Table;
using TL_Objects;
using WpfApp.Config;

namespace WpfApp.Visual.MainWindow.GlobalElements.Menus.SettingsMenu.SettingsControl.ProfileSettings
{
	/// <summary>
	/// Логика взаимодействия для ProfilesSettingsControl.xaml
	/// </summary>
	public partial class ProfilesSettingsControl : UserControl, ISettingsControl
	{
		public ProfilesSettingsControl()
		{
			InitializeComponent();
			RefreshControl();
		}

		public void GetSettings()
		{
			
		}

		public void RefreshControl()
		{
			AddProfileTextBox.Text = "";
			ProfilesPanel.Children.Clear();
			
			foreach (Profile profile in MainInfo.Settings.Profiles)
			{
				ProfilesPanel.Children.Add(new ProfileControl(profile));
			}
			
		}

		private void AddProfileButton_Click(object sender, RoutedEventArgs e)
		{
			ProfileCollection profileCollection = MainInfo.Settings.Profiles;
			if (AddProfileTextBox.Text != "")
			{
				bool exclusive = true;
				Profile newProfile = new Profile(AddProfileTextBox.Text);

				foreach (Profile prof in profileCollection)
				{
					if (prof.Name == newProfile.Name) exclusive = false;
				}

				if (exclusive)
				{
					Directory.CreateDirectory(newProfile.ProfilePath);
					using (FileStream fs = File.Create(newProfile.MainFilePath)) { }

					TableCollection tc = MainInfo.Tables.GetDefaultTableCollectionData();
					tc.TableFilePath = newProfile.MainFilePath;

					Table<Genre> genreTable = tc.GetTable<Genre>();
					genreTable.RemoveAll(true);

					foreach (Genre genre in MainInfo.Tables.GenresTable)
					{
						genreTable.AddElement(genre);
					}

					tc.SaveTables();

					profileCollection.AddProfile(newProfile);
				}

				this.RefreshControl();
			}
		}
    }
}
