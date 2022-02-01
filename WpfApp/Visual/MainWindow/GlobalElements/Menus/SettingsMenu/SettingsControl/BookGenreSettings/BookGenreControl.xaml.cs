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
    /// Логика взаимодействия для BookGenreControl.xaml
    /// </summary>
    public partial class BookGenreControl : UserControl
    {
        public GenreVM GenreVM { get; private set; }
        public BookGenreControl(BookGenre bookGenre)
        {
            InitializeComponent();
            GenreVM = new GenreVM(bookGenre);
            DataContext = GenreVM;
        }

        private void DeleteGenreButton_Click(object sender, RoutedEventArgs e)
        {
            if (!MainInfo.Tables.BooksTable.GenreHasBook(GenreVM.BookGenre))
            {
                WrapPanel panel = (WrapPanel)Parent;
                panel.Children.Remove(this);
                ((Grid)panel.Parent).Height -= 20;

                MainInfo.Tables.BookGenresTable.Remove(GenreVM.BookGenre);
            }
        }
    }
}
