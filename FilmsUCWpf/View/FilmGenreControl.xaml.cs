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
    /// Логика взаимодействия для FilmGenreControl.xaml
    /// </summary>
    public partial class FilmGenreControl : UserControl, IGenreView
    {
        FilmGenrePresenter presenter;
        public FilmGenreControl(Genre genre, TableCollection tableCollection)
        {
            presenter = new FilmGenrePresenter(genre, this, tableCollection);
            InitializeComponent();
            DataContext = this.presenter;
        }

        public void RemoveFromview()
        {
            WrapPanel panel = (WrapPanel)Parent;
            panel.Children.Remove(this);
            ((Grid)panel.Parent).Height -= 20;
        }

        private void DeleteGenreButton_Click(object sender, RoutedEventArgs e)
        {
            presenter.DeleteGenre();
        }
    }
}
