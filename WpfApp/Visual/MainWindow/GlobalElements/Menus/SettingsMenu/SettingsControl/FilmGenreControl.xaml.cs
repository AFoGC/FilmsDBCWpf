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

namespace WpfApp.Visual.MainWindow.GlobalElements.Menus.SettingsMenu.SettingsControl
{
    /// <summary>
    /// Логика взаимодействия для FilmGenreControl.xaml
    /// </summary>
    public partial class FilmGenreControl : UserControl
    {
        private Genre FilmGenre { set; get; }
        public FilmGenreControl(Genre genre)
        {
            InitializeComponent();
            DataContext = new GenreVM(genre);
        }
    }
}
