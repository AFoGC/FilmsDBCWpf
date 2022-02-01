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
using TL_Objects;

namespace WpfApp.Visual.MainWindow.GlobalElements.Menus.SettingsMenu.SettingsControl.GenreSettings
{
    /// <summary>
    /// Логика взаимодействия для FilmGenresSettingsControl.xaml
    /// </summary>
    public partial class FilmGenresSettingsControl : UserControl, ISettingsControl
    {
        public FilmGenresSettingsControl()
        {
            InitializeComponent();
            MainInfo.TableCollection.TableLoad += TableCollection_TableLoad;
        }

        private void TableCollection_TableLoad(object sender, EventArgs e)
        {
            RefreshControl();
        }

        public void GetSettings()
        {
            //throw new NotImplementedException();
        }

        public void RefreshControl()
        {
            GenresPanel.Children.Clear();
            foreach (Genre genre in MainInfo.Tables.GenresTable)
            {
                GenresPanel.Children.Add(new FilmGenreControl(genre));
            }
        }

        private void Help_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("help.html");
        }
    }
}
