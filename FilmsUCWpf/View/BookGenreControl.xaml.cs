using FilmsUCWpf.Presenter;
using FilmsUCWpf.View.Interfaces;
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
using TablesLibrary.Interpreter;
using TL_Objects;

namespace FilmsUCWpf.View
{
    /// <summary>
    /// Логика взаимодействия для BookGenreControl.xaml
    /// </summary>
    public partial class BookGenreControl : UserControl, IGenreView
    {
        private readonly BookGenrePresenter presenter;
        public BookGenreControl(BookGenre genre, TableCollection collection)
        {
            presenter = new BookGenrePresenter(genre, this, collection);
            InitializeComponent();
            DataContext = this.presenter;
        }

        public void RemoveFromview()
        {
            Panel panel = (Panel)Parent;
            panel.Children.Remove(this);
            ((Grid)panel.Parent).Height -= 20;
        }

        private void DeleteGenreButton_Click(object sender, RoutedEventArgs e)
        {
            presenter.DeleteGenre();
        }
    }
}
