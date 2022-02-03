using BL_Films;
using System;
using System.Collections.Generic;
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
using WpfApp.Visual.MainWindow.GlobalElements.Menus.Registration_Window;
using WpfApp.Visual.MainWindow.GlobalElements.Menus.SettingsMenu.SettingsControl;
using WpfApp.Visual.MainWindow.GlobalElements.Menus.SettingsMenu.SettingsControl.BookGenreSettings;
using WpfApp.Visual.MainWindow.GlobalElements.Menus.SettingsMenu.SettingsControl.GenreSettings;
using WpfApp.Visual.MainWindow.GlobalElements.Menus.SettingsMenu.SettingsControl.ProfileSettings;

namespace WpfApp.Visual.MainWindow.GlobalElements.Menus.SettingsMenu
{
    /// <summary>
    /// Логика взаимодействия для SettingsMenuControl.xaml
    /// </summary>
    public partial class SettingsMenuControl : UserControl
    {
        private ProfilesSettingsControl profilesSettings;
        private FilmGenresSettingsControl filmGenreControl;
        private BookGenreSettingsControl bookGenreSettings;
        public SettingsMenuControl()
        {
            InitializeComponent();

            profilesSettings = new ProfilesSettingsControl();
            filmGenreControl = new FilmGenresSettingsControl();
            bookGenreSettings = new BookGenreSettingsControl();

            
            SettingsListPanel.Children.Add(profilesSettings);
            SettingsListPanel.Children.Add(filmGenreControl);
            SettingsListPanel.Children.Add(bookGenreSettings);
            RefreshControl();
        }

        public void RefreshControl()
        {
            profilesSettings.RefreshControl();
        }

        private void Embrace_Click(object sender, RoutedEventArgs e)
        {
            foreach (ISettingsControl setting in SettingsListPanel.Children)
            {
                setting.GetSettings();
            }


            MainInfo.TableCollection.LoadTables();
        }

        private void GetFromDB_Click(object sender, RoutedEventArgs e)
        {
            MainInfo.Settings.Profiles.GetProfilesFromDB(MainInfo.LoggedInUser);
            MainInfo.TableCollection.LoadTables();
        }

        private void SendToDB_Click(object sender, RoutedEventArgs e)
        {
            MainInfo.Settings.Profiles.SendProfilesToDB(MainInfo.LoggedInUser);
        }

        private void Help_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("Довідка до програми.pdf");
        }
    }
}
