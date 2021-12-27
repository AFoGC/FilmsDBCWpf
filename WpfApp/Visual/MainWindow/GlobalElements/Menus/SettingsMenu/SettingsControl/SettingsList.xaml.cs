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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using TablesLibrary.Interpreter;
using TL_Objects;
using WpfApp.Config;

namespace WpfApp.Visual.MainWindow.GlobalElements.Menus.SettingsMenu.SettingsControl
{
    /// <summary>
    /// Логика взаимодействия для SettingsList.xaml
    /// </summary>
    public partial class SettingsList : UserControl, ISettingsControl
    {
        private ProfileCollection profileCollection = null;
        private Profile usedProfile = null;

        public SettingsList()
        {
            InitializeComponent();
            Grid.Height = 20;
        }

        private void Expand_Click(object sender, RoutedEventArgs e)
        {
            DoubleAnimation doubleAnimation = new DoubleAnimation();
            doubleAnimation.From = Grid.Height;
            if  (Grid.Height == 80)
            {
                doubleAnimation.To = 20;
            }
            else
            {
                doubleAnimation.To = 80;
            }
            doubleAnimation.Duration = TimeSpan.FromSeconds(0.3);
            Grid.BeginAnimation(Grid.HeightProperty, doubleAnimation);
            
            
            
        }

        public void GetSettings()
        {
            MainInfo.Settings.UsedProfile = usedProfile;
            MainInfo.TableCollection.TableFilePath = usedProfile.MainFilePath;
        }

        public void RefreshControl()
        {
            profileCollection = MainInfo.Settings.Profiles;
            usedProfile = MainInfo.Settings.UsedProfile;

            ProfileTextBox.Text = usedProfile.Name;

            ProfileComboBox.Items.Clear();
            RenameProfileComboBox.Items.Clear();
            RemoveProfileComboBox.Items.Clear();

            foreach (Profile prof in profileCollection.Profiles)
            {
                ProfileComboBox.Items.Add(prof);
                RenameProfileComboBox.Items.Add(prof);
                RemoveProfileComboBox.Items.Add(prof);

                if (prof.Name == usedProfile.Name)
                {
                    ProfileComboBox.SelectedItem = prof;
                }
            }
            RemoveProfileComboBox.Text = "";
        }


        //В CRUD методах нужно ещё добавить проверок
        private void ChangeProfileButton_Click(object sender, RoutedEventArgs e)
        {
            usedProfile = (Profile)ProfileComboBox.SelectedItem;
            ProfileTextBox.Text = usedProfile.Name;
        }

        private void RenameProfileButton_Click(object sender, RoutedEventArgs e)
        {
            if (RenameProfileTextBox.Text != "")
            {
                Profile profileFrom = (Profile)RenameProfileComboBox.SelectedItem;
                profileFrom.RenameProfile(RenameProfileTextBox.Text);

                this.RefreshControl();

                RenameProfileComboBox.SelectedItem = profileFrom;
            }
        }

        private void RemoveProfileButton_Click(object sender, RoutedEventArgs e)
        {
            if (RemoveProfileComboBox.Text != "")
            {
                Profile prof = (Profile)RemoveProfileComboBox.SelectedItem;
                Directory.Delete(prof.ProfilePath, true);

                profileCollection.RemoveProfile(prof);

                this.RefreshControl();
            }
        }

        private void AddProfileButton_Click(object sender, RoutedEventArgs e)
        {
            if (AddProfileTextBox.Text != "")
            {
                bool exclusive = true;
                Profile newProfile = new Profile(AddProfileTextBox.Text);

                foreach (Profile prof in profileCollection.Profiles)
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
                        genreTable.AddWithoutReindexation(genre);
                    }

                    tc.SaveTables();

                    profileCollection.AddProfile(newProfile);
                }

                this.RefreshControl();
            }
        }
    }
}
