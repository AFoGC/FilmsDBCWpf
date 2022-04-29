using System;
using System.Collections;
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
using WpfApp.MVP.Presenters;
using WpfApp.MVP.ViewsInterface;

namespace WpfApp.MVP.Views
{
    /// <summary>
    /// Логика взаимодействия для GenreConteinerView.xaml
    /// </summary>
    public partial class GenreConteinerView : UserControl, IGenreSettingsContainerView
    {
        private readonly IGenreContainerPresenter presenter;

        public GenreConteinerView(TableCollection tableCollection, GenrePresenterEnum genre)
        {
            InitializeComponent();
            DefaultHeight = Height;
            if (genre == GenrePresenterEnum.BookGenre)
                presenter = new BookGenreContainerPresenter(this, tableCollection);
            else
                presenter = new FilmGenreContainerPresenter(this, tableCollection);
            
        }

        public IList GenreControls => GenresPanel.Children;

        public double DefaultHeight { get; private set; }

        double IGenreSettingsContainerView.Height { get => grid.Height; set => grid.Height = value; }

        private void AddGenreButton_Click(object sender, RoutedEventArgs e)
        {
            presenter.AddGenre();
        }
    }
}
