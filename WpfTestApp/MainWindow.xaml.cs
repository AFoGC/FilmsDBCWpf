using FilmsUCWpf.Presenter;
using FilmsUCWpf.View;
using FilmsUCWpf.ViewInterfaces;
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
using TL_Tables;

namespace WpfTestApp
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public TableCollection TableCollection { get; private set; }
        public FilmsTable FilmsTable { get; private set; }
        public object IView { get; private set; }

        private Genre genre;
        private List<FilmPresenter> presenters;

        public MainWindow()
        {
            InitializeComponent();
            TableCollection = new TableCollection();
            FilmsTable = new FilmsTable();
            presenters = new List<FilmPresenter>();

            genre = new Genre(1);
            genre.Name = "genre1";

            TableCollection.AddTable(FilmsTable);
        }

        private bool simple = false;
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Film film = new Film();
            film.Name = "Ayaya";
            film.Genre = genre;
            film.RealiseYear = 2000;
            film.DateOfWatch = DateTime.Today;
            film.Mark = 5;
            film.Watched = simple;

            FilmsTable.AddElement(film);

            IBaseView view;
            if (simple)
                view = new FilmSimpleControl();
            else
                view = new FilmControl();

            simple = !simple;

            FilmPresenter presenter = new FilmPresenter(film, view, null);
            presenter.AddViewToCollection(controlsPanel.Children);
            presenters.Add(presenter);
            
            
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            FilmPresenter presenter = presenters[presenters.Count - 1];
            presenter.Model.RealiseYear++;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            FilmsTable.Remove(FilmsTable.GetLastElement);
        }
    }
}
