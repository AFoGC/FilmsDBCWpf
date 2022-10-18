using FilmsUCWpf.ViewModel;
using FilmsUCWpf.ViewModel.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using TablesLibrary.Interpreter.TableCell;
using TL_Objects;
using TL_Objects.CellDataClasses;
using TL_Objects.Interfaces;
using TL_Tables;
using WpfApp.Commands;
using WpfApp.Models;

namespace WpfApp.ViewModels
{
    public class FilmsViewModel : BaseViewModel, IMenuViewModel<Film>
    {
        public FilmsModel Model { get; private set; }

        public ObservableCollection<GenreButtonViewModel> GenresTable { get; private set; }
        public ObservableCollection<FilmCategoryViewModel> CategoriesMenu { get; private set; }
        public ObservableCollection<FilmViewModel> SimpleFilmsMenu { get; private set; }
        public ObservableCollection<FilmViewModel> FilmsMenu { get; private set; }
        public ObservableCollection<FilmSerieViewModel> SeriesMenu { get; private set; }
        public ObservableCollection<FilmViewModel> PriorityFilmsMenu { get; private set; }

        private Visibility _categoryVisibility = Visibility.Collapsed;
        public Visibility CategoryVisibility
        {
            get => _categoryVisibility;
            set
            {
                _categoryVisibility = value;
                OnPropertyChanged();
            }
        }

        private Visibility _filmsVisibility = Visibility.Collapsed;
        public Visibility FilmsVisibility
        {
            get => _filmsVisibility;
            set
            {
                _filmsVisibility = value;
                OnPropertyChanged();
            }
        }

        private Visibility _seriesVisibility = Visibility.Collapsed;
        public Visibility SeriesVisibility
        {
            get => _seriesVisibility;
            set
            {
                _seriesVisibility = value;
                OnPropertyChanged();
            }
        }

        private Visibility _priorityVisibility = Visibility.Collapsed;
        public Visibility PriorityVisibility
        {
            get => _priorityVisibility;
            set
            {
                _priorityVisibility = value;
                OnPropertyChanged();
            }
        }

        private bool _isReadedChecked = true;
        public bool IsReadedChecked
        {
            get => _isReadedChecked;
            set
            {
                if (IsUnReadedChecked != false || value != false)
                {
                    _isReadedChecked = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _isUnReadedChecked = true;
        public bool IsUnReadedChecked
        {
            get => _isUnReadedChecked;
            set
            {
                if (IsReadedChecked != false || value != false)
                {
                    _isUnReadedChecked = value;
                    OnPropertyChanged();
                }
            }
        }

        private BaseViewModel<Film> _selectedElement;
        public BaseViewModel<Film> SelectedElement
        {
            get => _selectedElement;
            set
            {
                if (_selectedElement != null) _selectedElement.IsSelected = false;
                _selectedElement = value;
                _selectedElement.IsSelected = true;
            }
        }

        private String _searchText = String.Empty;
        public String SearchText
        {
            get => _searchText;
            set
            {
                _searchText = value;
                OnPropertyChanged();
            }
        }

        private Command showCategoriesCommand;
        public Command ShowCategoriesCommand
        {
            get
            {
                return showCategoriesCommand ??
                (showCategoriesCommand = new Command(obj =>
                {
                    CategoryVisibility = Visibility.Visible;
                    FilmsVisibility = Visibility.Collapsed;
                    SeriesVisibility = Visibility.Collapsed;
                    PriorityVisibility = Visibility.Collapsed;
                }));
            }
        }

        private Command showFilmsCommand;
        public Command ShowFilmsCommand
        {
            get
            {
                return showFilmsCommand ??
                (showFilmsCommand = new Command(obj =>
                {
                    CategoryVisibility = Visibility.Collapsed;
                    FilmsVisibility = Visibility.Visible;
                    SeriesVisibility = Visibility.Collapsed;
                    PriorityVisibility = Visibility.Collapsed;
                }));
            }
        }

        private Command showSeriesCommand;
        public Command ShowSeriesCommand
        {
            get
            {
                return showSeriesCommand ??
                (showSeriesCommand = new Command(obj =>
                {
                    CategoryVisibility = Visibility.Collapsed;
                    FilmsVisibility = Visibility.Collapsed;
                    SeriesVisibility = Visibility.Visible;
                    PriorityVisibility = Visibility.Collapsed;
                }));
            }
        }

        private Command showPriorityCommand;
        public Command ShowPriorityCommand
        {
            get
            {
                return showPriorityCommand ??
                (showPriorityCommand = new Command(obj =>
                {
                    CategoryVisibility = Visibility.Collapsed;
                    FilmsVisibility = Visibility.Collapsed;
                    SeriesVisibility = Visibility.Collapsed;
                    PriorityVisibility = Visibility.Visible;
                }));
            }
        }

        private Command addCategoryCommand;
        public Command AddCategoryCommand
        {
            get
            {
                return addCategoryCommand ??
                (addCategoryCommand = new Command(obj => Model.AddCategory()));
            }
        }

        private Command addBookCommand;
        public Command AddBookCommand
        {
            get
            {
                return addBookCommand ??
                (addBookCommand = new Command(obj => Model.AddFilm()));
            }
        }

        private Command saveTablesCommand;
        public Command SaveTablesCommand
        {
            get
            {
                return saveTablesCommand ??
                (saveTablesCommand = new Command(obj => Model.SaveTables()));
            }
        }

        private Command filterCommand;
        public Command FilterCommand
        {
            get
            {
                return filterCommand ??
                (filterCommand = new Command(obj =>
                {
                    IGenre[] genres = getSelectedGenres();
                    FilterTable(CategoriesMenu, genres);
                    FilterTable(SimpleFilmsMenu, genres);
                    FilterTable(FilmsMenu, genres);
                    FilterTable(SeriesMenu, genres);
                    FilterTable(PriorityFilmsMenu, genres);

                }));
            }
        }

        private Command searchCommand;
        public Command SearchCommand
        {
            get
            {
                return searchCommand ??
                (searchCommand = new Command(obj =>
                {
                    SearcherTable(CategoriesMenu);
                    SearcherTable(SimpleFilmsMenu);
                    SearcherTable(FilmsMenu);
                    SearcherTable(SeriesMenu);
                    SearcherTable(PriorityFilmsMenu);
                }));
            }
        }

        private CollectionViewSource categoryCVS;
        private CollectionViewSource simpleFilmsCVS;
        private CollectionViewSource filmsCVS;
        private CollectionViewSource seriesCVS;
        private CollectionViewSource priorityFilmsCVS;
        public CollectionViewSource CategoryCVS 
        { 
            get => categoryCVS; 
            set
            {
                categoryCVS = value;
                OnPropertyChanged();
            }
        }
        public CollectionViewSource SimpleFilmsCVS 
        { 
            get => simpleFilmsCVS; 
            set
            {
                simpleFilmsCVS = value;
                OnPropertyChanged();
            }
        }
        public CollectionViewSource FilmsCVS 
        { 
            get => filmsCVS; 
            set
            {
                filmsCVS = value;
                OnPropertyChanged();
            }
        }
        public CollectionViewSource SeriesCVS 
        { 
            get => seriesCVS; 
            set
            {
                seriesCVS = value;
                OnPropertyChanged();
            }
        }
        public CollectionViewSource PriorityFilmsCVS 
        { 
            get => priorityFilmsCVS; 
            set
            {
                priorityFilmsCVS = value;
                OnPropertyChanged();
            }
        }

        private Command sortTable;
        public Command SortTable =>
        sortTable ?? (sortTable = new Command(obj =>
        {
            string str = obj as string;
            ListSortDirection direction = getSortDirection(str);
            CollectionViewSource[] sources = getVisibleCVS();

            foreach (CollectionViewSource item in sources)
            {
                CVSChangeSort(item, str, direction);
                //item.View.;
            }

            Costyl();
        }));

        private void Costyl()
        {
            if (CategoryVisibility == Visibility.Visible)
            {
                CategoryCVS = CategoryCVS;
            }
            if (FilmsVisibility == Visibility.Visible)
            {
                FilmsCVS = FilmsCVS;
            }
            if (SeriesVisibility == Visibility.Visible)
            {
                SeriesCVS = SeriesCVS;
            }
            if (PriorityVisibility == Visibility.Visible)
            {
                PriorityFilmsCVS = PriorityFilmsCVS;
            }
        }

        private ListSortDirection getSortDirection(string paramenter)
        {
            switch (paramenter)
            {
                case "Model.Mark":
                    return ListSortDirection.Descending;
                case "Model.RealiseYear":
                    return ListSortDirection.Descending;
                case "Model.CountOfViews":
                    return ListSortDirection.Descending;
                case "Model.DateOfWatch":
                    return ListSortDirection.Descending;
                case "Model.StartWatchDate":
                    return ListSortDirection.Descending;
                case "Model.CountOfWatchedSeries":
                    return ListSortDirection.Descending;
                case "Model.TotalSeries":
                    return ListSortDirection.Descending;

                default:
                    return ListSortDirection.Ascending;
            }
        }

        private CollectionViewSource[] getVisibleCVS()
        {
            List<CollectionViewSource> sources = new List<CollectionViewSource>();

            if (CategoryVisibility == Visibility.Visible)
            {
                sources.Add(CategoryCVS);
                sources.Add(SimpleFilmsCVS);
            }
            if (FilmsVisibility == Visibility.Visible)
            {
                sources.Add(FilmsCVS);
            }
            if (SeriesVisibility == Visibility.Visible)
            {
                sources.Add(SeriesCVS);
            }
            if (PriorityVisibility == Visibility.Visible)
            {
                sources.Add(PriorityFilmsCVS);
            }

            return sources.ToArray();
        }

        private void CVSChangeSort(CollectionViewSource cvs, string property, ListSortDirection direction)
        {
            cvs.SortDescriptions.Clear();
            cvs.SortDescriptions.Add(new SortDescription(property, direction));
        }

        private void FilterTable(IEnumerable table, IGenre[] genres)
        {
            foreach (IFilter vm in table)
            {
                vm.Filter(genres, IsReadedChecked, IsUnReadedChecked);
            }
        }

        private void SearcherTable(IEnumerable table)
        {
            string search = SearchText.ToLower();
            foreach (IFinded vm in table)
            {
                vm.SetFinded(search);
            }
        }

        private IGenre[] getSelectedGenres()
        {
            List<IGenre> genres = new List<IGenre>();
            foreach (var genre in GenresTable)
            {
                if (genre.IsChecked)
                {
                    genres.Add(genre.Model);
                }
            }
            return genres.ToArray();
        }

        public FilmsViewModel()
        {
            Model = new FilmsModel();

            GenresTable = new ObservableCollection<GenreButtonViewModel>();
            CategoriesMenu = new ObservableCollection<FilmCategoryViewModel>();
            SimpleFilmsMenu = new ObservableCollection<FilmViewModel>();
            FilmsMenu = new ObservableCollection<FilmViewModel>();
            SeriesMenu = new ObservableCollection<FilmSerieViewModel>();
            PriorityFilmsMenu = new ObservableCollection<FilmViewModel>();

            Model.TableCollection.TableLoad += TableLoad;

            Model.GenresTable.CollectionChanged += GenresChanged;
            Model.FilmsTable.CollectionChanged += FilmsChanged;
            Model.CategoriesTable.CollectionChanged += CategoriesChanged;
            Model.SeriesTable.CollectionChanged += SeriesChanged;
            Model.PriorityFilmsTable.CollectionChanged += PriorityChanged;

            CategoryVisibility = Visibility.Visible;
            TableLoad(this, null);

            SimpleFilmsCVS = new CollectionViewSource();
            CategoryCVS = new CollectionViewSource();
            FilmsCVS = new CollectionViewSource();
            SeriesCVS = new CollectionViewSource();
            PriorityFilmsCVS = new CollectionViewSource();

            SimpleFilmsCVS.Source = SimpleFilmsMenu;
            CategoryCVS.Source = CategoriesMenu;
            FilmsCVS.Source = FilmsMenu;
            SeriesCVS.Source = SeriesMenu;
            PriorityFilmsCVS.Source = PriorityFilmsMenu;

            CVSChangeSort(SeriesCVS, "Model.ID", ListSortDirection.Ascending);

            SourcesCVS = new CollectionViewSource();
        }


        
        private void SeriesChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            Serie serie;
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    serie = (Serie)e.NewItems[0];
                    SeriesMenu.Add(new FilmSerieViewModel(serie, this));
                    break;
                case NotifyCollectionChangedAction.Remove:
                    serie = (Serie)e.OldItems[0];
                    SeriesMenu.Remove(SeriesMenu.Where(x => x.Model == serie.Film).FirstOrDefault());
                    break;
                case NotifyCollectionChangedAction.Reset:
                    SeriesMenu.Clear();
                    break;
                default:
                    break;
            }
        }

        private void PriorityChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            PriorityFilm priorityFilm;
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    priorityFilm = (PriorityFilm)e.NewItems[0];
                    PriorityFilmsMenu.Add(new FilmViewModel(priorityFilm.Film, this));
                    break;
                case NotifyCollectionChangedAction.Remove:
                    priorityFilm = (PriorityFilm)e.OldItems[0];
                    PriorityFilmsMenu.Remove(PriorityFilmsMenu.Where(x => x.Model == priorityFilm.Film).FirstOrDefault());
                    break;
                case NotifyCollectionChangedAction.Reset:
                    PriorityFilmsMenu.Clear();
                    break;
                default:
                    break;
            }
        }

        private void CategoriesChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            Category category;
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    category = (Category)e.NewItems[0];
                    CategoriesMenu.Add(new FilmCategoryViewModel(category, this));
                    category.Films.CollectionChanged += CategoryChanged;
                    break;
                case NotifyCollectionChangedAction.Remove:
                    category = (Category)e.OldItems[0];
                    CategoriesMenu.Remove(CategoriesMenu.Where(x => x.Model == category).FirstOrDefault());
                    category.Films.CollectionChanged -= CategoryChanged;
                    break;
                case NotifyCollectionChangedAction.Reset:
                    CategoriesMenu.Clear();
                    break;
                default:
                    break;
            }
        }

        private void FilmsChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            Film film;
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    film = (Film)e.NewItems[0];
                    film.PropertyChanged += FilmPropertyChanged;

                    FilmsMenu.Add(new FilmViewModel(film, this));
                    SimpleFilmsMenu.Add(new FilmViewModel(film, this));
                    break;
                case NotifyCollectionChangedAction.Remove:
                    film = (Film)e.OldItems[0];
                    film.PropertyChanged -= FilmPropertyChanged;

                    FilmsMenu.Remove(FilmsMenu.Where(x => x.Model == film).FirstOrDefault());
                    SimpleFilmsMenu.Remove(SimpleFilmsMenu.Where(x => x.Model == film).FirstOrDefault());
                    SeriesMenu.Remove(SeriesMenu.Where(x => x.Model == film).FirstOrDefault());
                    PriorityFilmsMenu.Remove(PriorityFilmsMenu.Where(x => x.Model == film).FirstOrDefault());
                    break;
                case NotifyCollectionChangedAction.Reset:
                    FilmsMenu.Clear();
                    SimpleFilmsMenu.Clear();
                    break;
                default:
                    break;
            }
        }

        private void FilmPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            Film film = sender as Film;
            if (e.PropertyName == nameof(film.Genre))
            {
                FilmSerieViewModel serieVM = SeriesMenu.Where(x => x.Model == film).FirstOrDefault();
                if (film.Genre.IsSerialGenre)
                {
                    if (serieVM == null)
                    {
                        Serie serie = Model.SeriesTable.FindAndConnectSerie(film);
                        SeriesMenu.Add(new FilmSerieViewModel(serie, this));
                    }
                }
                else
                {
                    if (serieVM != null)
                    {
                        SeriesMenu.Remove(serieVM);
                    }
                }
            }
        }

        private void GenresChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            Genre genre;
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    genre = (Genre)e.NewItems[0];
                    GenresTable.Add(new GenreButtonViewModel(genre));
                    break;
                case NotifyCollectionChangedAction.Remove:
                    genre = (Genre)e.OldItems[0];
                    GenresTable.Remove(GenresTable.Where(x => x.Model == genre).FirstOrDefault());
                    break;
                case NotifyCollectionChangedAction.Reset:
                    GenresTable.Clear();
                    break;
                default:
                    break;
            }
        }

        private void TableLoad(object sender, EventArgs e)
        {
            CategoriesMenu.Clear();
            SimpleFilmsMenu.Clear();
            FilmsMenu.Clear();
            SeriesMenu.Clear();
            PriorityFilmsMenu.Clear();
            GenresTable.Clear();

            foreach (Genre genre in Model.GenresTable)
            {
                GenresTable.Add(new GenreButtonViewModel(genre));
            }

            foreach (Category category in Model.CategoriesTable)
            {
                CategoriesMenu.Add(new FilmCategoryViewModel(category, this));
                category.Films.CollectionChanged += CategoryChanged;
            }

            foreach (Film film in Model.FilmsTable)
            {
                film.PropertyChanged += FilmPropertyChanged;

                FilmsMenu.Add(new FilmViewModel(film, this));
                if (film.FranshiseId == 0)
                {
                    SimpleFilmsMenu.Add(new FilmViewModel(film, this));
                }
            }

            foreach (Serie serie in Model.SeriesTable)
            {
                SeriesMenu.Add(new FilmSerieViewModel(serie, this));
            }

            foreach (PriorityFilm book in Model.PriorityFilmsTable)
            {
                PriorityFilmsMenu.Add(new FilmViewModel(book.Film, this));
            }
        }

        private void CategoryChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            Film film;
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    film = (Film)e.NewItems[0];
                    SimpleFilmsMenu.Remove(SimpleFilmsMenu.Where(x => x.Model == film).FirstOrDefault());
                    break;
                case NotifyCollectionChangedAction.Remove:
                    film = (Film)e.OldItems[0];
                    if (Model.FilmsTable.Contains(film))
                    {
                        SimpleFilmsMenu.Add(new FilmViewModel(film, this));
                    }
                    break;
                default:
                    break;
            }
        }

        private FilmInfoMenuCondition infoMenuCondition;
        public FilmInfoMenuCondition InfoMenuCondition
        {
            get => infoMenuCondition;
            set
            {
                infoMenuCondition = value;
                if (infoMenuCondition == FilmInfoMenuCondition.Closed)
                {
                    InfoMenuDataContext = null;
                    SourcesCVS.Source = null;
                }

                OnPropertyChanged();
            }
        }

        private Object _infoMenuDataContext;
        public Object InfoMenuDataContext
        {
            get => _infoMenuDataContext;
            set
            {
                _infoMenuDataContext = value;
                OnPropertyChanged();
            }
        }

        public void OpenInfoMenu(Cell model)
        {
            SourcesCVS.Source = null;
            InfoMenuCondition = FilmInfoMenuCondition.Closed;
            if (model.GetType() == typeof(Film))
            {
                Film film = model as Film;
                if (film.Serie == null)
                {
                    InfoMenuDataContext = new FilmViewModel(film, this);
                    InfoMenuCondition = FilmInfoMenuCondition.FilmInfo;
                }
                else
                {
                    InfoMenuDataContext = new FilmSerieViewModel(film.Serie, this);
                    InfoMenuCondition = FilmInfoMenuCondition.SerieInfo;
                }
                
            }
        }

        public CollectionViewSource SourcesCVS { get; private set; }
        public void OpenSourcesMenu(ObservableCollection<Source> sources)
        {
            SourcesCVS.Source = sources;
        }

        public void OpenUpdateMenu(Cell model)
        {
            SourcesCVS.Source = null;
            InfoMenuCondition = FilmInfoMenuCondition.Closed;
            if (model.GetType() == typeof(Film))
            {
                Film film = model as Film;
                if (film.Serie == null)
                {
                    InfoMenuDataContext = new FilmViewModel(film, this);
                    InfoMenuCondition = FilmInfoMenuCondition.FilmUpdate;
                }
                else
                {
                    InfoMenuDataContext = new FilmSerieViewModel(film.Serie, this);
                    InfoMenuCondition = FilmInfoMenuCondition.SerieUpdate;
                }
            }
            if (model.GetType() == typeof(Category))
            {
                Category category = model as Category;
                InfoMenuDataContext = new FilmCategoryViewModel(category, this);
                InfoMenuCondition = FilmInfoMenuCondition.CategoryUpdate;
            }
        }

        private Command removeSourceCommand;
        public Command RemoveSourceCommand =>
        removeSourceCommand ?? (removeSourceCommand = new Command(obj =>
        {
            if (SourcesCVS.Source != null)
            {
                Source source = obj as Source;
                ObservableCollection<Source> sources = SourcesCVS.Source as ObservableCollection<Source>;
                sources.Remove(source);
            }
        }));

        private Command addSourceCommand;
        public Command AddSourceCommand =>
        addSourceCommand ?? (addSourceCommand = new Command(obj =>
        {
            if (SourcesCVS.Source != null)
            {
                ObservableCollection<Source> sources = SourcesCVS.Source as ObservableCollection<Source>;
                sources.Add(new Source());
            }
        }));

        private Command moveUpSourceCommand;
        public Command MoveUpSourceCommand =>
        moveUpSourceCommand ?? (moveUpSourceCommand = new Command(obj =>
        {
            if (SourcesCVS.Source != null)
            {
                Source source = obj as Source;
                ObservableCollection<Source> sources = SourcesCVS.Source as ObservableCollection<Source>;
                sources.Move(sources.IndexOf(source), 0);
            }
        }));

        private Command closeInfoCommand;
        public Command CloseInfoCommand =>
        closeInfoCommand ?? (closeInfoCommand = new Command(obj =>
        {
            InfoMenuCondition = FilmInfoMenuCondition.Closed;
        }));
    }

    public enum FilmInfoMenuCondition
    {
        Closed,
        FilmInfo,
        FilmUpdate,
        SerieInfo,
        SerieUpdate,
        CategoryUpdate
    }
}
