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

namespace WpfApp.Visual.MainWindow.GlobalElements.Menus.SettingsMenu.SettingsControl.BookGenreSettings
{
    /// <summary>
    /// Логика взаимодействия для BookGenreSettingsControl.xaml
    /// </summary>
    public partial class BookGenreSettingsControl : UserControl, ISettingsControl
    {
        public BookGenreSettingsControl()
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
            throw new NotImplementedException();
        }

        public void RefreshControl()
        {
            grid.Height = 60;
            GenresPanel.Children.Clear();
            foreach (BookGenre genre in MainInfo.Tables.BookGenresTable)
            {
                GenresPanel.Children.Add(new BookGenreControl(genre));
                grid.Height += 20;
            }
        }

        private void AddGenreButton_Click(object sender, RoutedEventArgs e)
        {
            BookGenre genre = new BookGenre();
            MainInfo.Tables.BookGenresTable.AddElement(genre);
            genre.Name = $"Genre{genre.ID}";
            GenresPanel.Children.Add(new BookGenreControl(genre));
            grid.Height += 20;
        }
    }
}
