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
using WpfApp.Config;

namespace WpfApp.Visual.MainWindow.GlobalElements.Menus.SettingsMenu.SettingsControl
{
	/// <summary>
	/// Логика взаимодействия для ProfileControl.xaml
	/// </summary>
	public partial class ProfileControl : UserControl
	{
		public Profile Profile { get; private set; }
		public ProfileControl(Profile profile)
		{
			InitializeComponent();
			Profile = profile;
			this.ProfileNameTextBox.Text = profile.Name;
			this.setSelected(MainInfo.Settings.UsedProfile);
		}

		public void setSelected(Profile usedProfile)
        {
            if (usedProfile == Profile)
            {
				this.ProfileNameTextBox.Background = new SolidColorBrush(Color.FromRgb(53,53,83));
				this.ProfileNameTextBox.BorderBrush = new SolidColorBrush(Color.FromRgb(122, 122, 182));
			}
            else
            {
				this.ProfileNameTextBox.Background = new SolidColorBrush(Color.FromRgb(53, 53, 53));
				this.ProfileNameTextBox.BorderBrush = new SolidColorBrush(Color.FromRgb(122, 122, 122));
			}
        }

		private void ChangeProfileButton_Click(object sender, RoutedEventArgs e)
		{
			MainInfo.Settings.UsedProfile = Profile;
			MainInfo.Settings.SaveSettings();
			WrapPanel panel = (WrapPanel)this.Parent;
			Profile usedProfile = MainInfo.Settings.UsedProfile;
            foreach (ProfileControl profile in panel.Children)
            {
				profile.setSelected(usedProfile);
            }
		}

		private void DeleteProfileButton_Click(object sender, RoutedEventArgs e)
		{
			MainInfo.Settings.Profiles.RemoveProfile(Profile);
			WrapPanel panel =  (WrapPanel)this.Parent;
			panel.Children.Remove(this);
		}

		private void RenameProfileButton_Click(object sender, RoutedEventArgs e)
		{
			//Profile.RenameProfile();
		}
	}
}
