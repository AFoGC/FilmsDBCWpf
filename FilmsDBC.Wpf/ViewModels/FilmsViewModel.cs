﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using TablesLibrary.Interpreter.TableCell;
using TL_Objects;
using TL_Objects.CellDataClasses;
using TL_Objects.Interfaces;
using WpfApp.Commands;
using WpfApp.Models;
using WpfApp.TableViewModels;
using WpfApp.TableViewModels.Interfaces;

namespace WpfApp.ViewModels
{
    public class FilmsViewModel : BaseViewModel, IMenuViewModel<Film>
    {
        private readonly FilmsModel _model;

        private BaseViewModel<Film> _selectedElement;

        private Visibility _categoryVisibility = Visibility.Collapsed;
        private Visibility _filmsVisibility = Visibility.Collapsed;
        private Visibility _seriesVisibility = Visibility.Collapsed;
        private Visibility _priorityVisibility = Visibility.Collapsed;

        private bool _isReadedChecked = true;
        private bool _isUnReadedChecked = true;
        private string _searchText = string.Empty;

        private RelayCommand showCategoriesCommand;
        private RelayCommand showFilmsCommand;
        private RelayCommand showSeriesCommand;
        private RelayCommand showPriorityCommand;
        private RelayCommand addCategoryCommand;
        private RelayCommand addBookCommand;
        private RelayCommand saveTablesCommand;
        private RelayCommand filterCommand;
        private RelayCommand sortTable;
        private RelayCommand removeSourceCommand;
        private RelayCommand addSourceCommand;
        private RelayCommand moveUpSourceCommand;
        private RelayCommand closeInfoCommand;

        private FilmInfoMenuCondition _infoMenuCondition;
        private Object _infoMenuDataContext;

        public ObservableCollection<GenreButtonViewModel> GenresTable { get; private set; }
        public ObservableCollection<FilmCategoryViewModel> CategoriesMenu { get; private set; }
        public ObservableCollection<FilmViewModel> SimpleFilmsMenu { get; private set; }
        public ObservableCollection<FilmViewModel> FilmsMenu { get; private set; }
        public ObservableCollection<FilmSerieViewModel> SeriesMenu { get; private set; }
        public ObservableCollection<FilmViewModel> PriorityFilmsMenu { get; private set; }
        public CollectionViewSource SourcesCVS { get; private set; }
        public CollectionViewSource CategoryCVS { get; private set; }
        public CollectionViewSource SimpleFilmsCVS { get; private set; }
        public CollectionViewSource FilmsCVS { get; private set; }
        public CollectionViewSource SeriesCVS { get; private set; }
        public CollectionViewSource PriorityFilmsCVS { get; private set; }

        public FilmsViewModel(FilmsModel model)
        {
            _model = model;

            GenresTable = new ObservableCollection<GenreButtonViewModel>();
            CategoriesMenu = new ObservableCollection<FilmCategoryViewModel>();
            SimpleFilmsMenu = new ObservableCollection<FilmViewModel>();
            FilmsMenu = new ObservableCollection<FilmViewModel>();
            SeriesMenu = new ObservableCollection<FilmSerieViewModel>();
            PriorityFilmsMenu = new ObservableCollection<FilmViewModel>();

            _model.TablesLoaded += TableLoad;

            _model.GenresTable.CollectionChanged += GenresChanged;
            _model.FilmsTable.CollectionChanged += FilmsChanged;
            _model.CategoriesTable.CollectionChanged += CategoriesChanged;
            _model.SeriesTable.CollectionChanged += SeriesChanged;
            _model.PriorityFilmsTable.CollectionChanged += PriorityChanged;

            CategoryVisibility = Visibility.Visible;
            TableLoad();

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

            CVSChangeSort(CategoryCVS, "Model.ID", ListSortDirection.Ascending);
            CVSChangeSort(SimpleFilmsCVS, "Model.ID", ListSortDirection.Ascending);
            CVSChangeSort(FilmsCVS, "Model.ID", ListSortDirection.Ascending);
            CVSChangeSort(SeriesCVS, "Model.ID", ListSortDirection.Ascending);
            CVSChangeSort(PriorityFilmsCVS, "Model.ID", ListSortDirection.Ascending);

            SourcesCVS = new CollectionViewSource();
        }

        public Visibility CategoryVisibility
        {
            get => _categoryVisibility;
            set
            {
                _categoryVisibility = value;
                OnPropertyChanged();
            }
        }

        public Visibility FilmsVisibility
        {
            get => _filmsVisibility;
            set
            {
                _filmsVisibility = value;
                OnPropertyChanged();
            }
        }

        public Visibility SeriesVisibility
        {
            get => _seriesVisibility;
            set
            {
                _seriesVisibility = value;
                OnPropertyChanged();
            }
        }

        public Visibility PriorityVisibility
        {
            get => _priorityVisibility;
            set
            {
                _priorityVisibility = value;
                OnPropertyChanged();
            }
        }

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

        public BaseViewModel<Film> SelectedElement
        {
            get => _selectedElement;
            set
            {
                if (_selectedElement != null)
                    _selectedElement.IsSelected = false;

                _selectedElement = value;

                if (_selectedElement != null)
                    _selectedElement.IsSelected = true;
            }
        }

        public String SearchText
        {
            get => _searchText;
            set
            {
                _searchText = value;
                SearchInTable(CategoriesMenu);
                SearchInTable(SimpleFilmsMenu);
                SearchInTable(FilmsMenu);
                SearchInTable(SeriesMenu);
                SearchInTable(PriorityFilmsMenu);
                OnPropertyChanged();
            }
        }

        private void SearchInTable(IEnumerable table)
        {
            string search = SearchText.ToLower();

            foreach (IFinded vm in table)
                vm.SetFinded(search);
        }

        public RelayCommand ShowCategoriesCommand
        {
            get
            {
                return showCategoriesCommand ??
                (showCategoriesCommand = new RelayCommand(obj =>
                {
                    CategoryVisibility = Visibility.Visible;
                    FilmsVisibility = Visibility.Collapsed;
                    SeriesVisibility = Visibility.Collapsed;
                    PriorityVisibility = Visibility.Collapsed;
                }));
            }
        }

        public RelayCommand ShowFilmsCommand
        {
            get
            {
                return showFilmsCommand ??
                (showFilmsCommand = new RelayCommand(obj =>
                {
                    CategoryVisibility = Visibility.Collapsed;
                    FilmsVisibility = Visibility.Visible;
                    SeriesVisibility = Visibility.Collapsed;
                    PriorityVisibility = Visibility.Collapsed;
                }));
            }
        }

        public RelayCommand ShowSeriesCommand
        {
            get
            {
                return showSeriesCommand ?? 
                (showSeriesCommand = new RelayCommand(obj =>
                {
                    CategoryVisibility = Visibility.Collapsed;
                    FilmsVisibility = Visibility.Collapsed;
                    SeriesVisibility = Visibility.Visible;
                    PriorityVisibility = Visibility.Collapsed;
                }));
            }
        }
            

        public RelayCommand ShowPriorityCommand
        {
            get
            {
                return showPriorityCommand ?? 
                (showPriorityCommand = new RelayCommand(obj =>
                {
                    CategoryVisibility = Visibility.Collapsed;
                    FilmsVisibility = Visibility.Collapsed;
                    SeriesVisibility = Visibility.Collapsed;
                    PriorityVisibility = Visibility.Visible;
                }));
            }
        }
            

        public RelayCommand AddCategoryCommand =>
            addCategoryCommand ?? (addCategoryCommand = new RelayCommand(obj => 
                _model.AddCategory()));

        public RelayCommand AddBookCommand =>
            addBookCommand ?? (addBookCommand = new RelayCommand(obj => 
                _model.AddFilm()));

        public RelayCommand SaveTablesCommand =>
            saveTablesCommand ?? (saveTablesCommand = new RelayCommand(obj => 
                _model.SaveTables()));

        public RelayCommand FilterCommand
        {
            get
            {
                return filterCommand ?? (filterCommand = new RelayCommand(obj =>
                {
                    IGenre[] genres = GetSelectedGenres();
                    FilterTable(CategoriesMenu, genres);
                    FilterTable(SimpleFilmsMenu, genres);
                    FilterTable(FilmsMenu, genres);
                    FilterTable(SeriesMenu, genres);
                    FilterTable(PriorityFilmsMenu, genres);
                }));
            }
        }
            

        public RelayCommand SortTable
        {
            get
            {
                return sortTable ?? (sortTable = new RelayCommand(obj =>
                {
                    string str = obj as string;
                    ListSortDirection direction = GetSortDirection(str);
                    CollectionViewSource[] sources = GetVisibleCVS();

                    foreach (CollectionViewSource item in sources)
                        CVSChangeSort(item, str, direction);
                }));
            }
        }
            

        private ListSortDirection GetSortDirection(string paramenter)
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

        private CollectionViewSource[] GetVisibleCVS()
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
                vm.Filter(genres, IsReadedChecked, IsUnReadedChecked);
        }

        private IGenre[] GetSelectedGenres()
        {
            List<IGenre> genres = new List<IGenre>();

            foreach (var genre in GenresTable)
                if (genre.IsChecked)
                    genres.Add(genre.Model);

            return genres.ToArray();
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
                        Serie serie = _model.SeriesTable.FindAndConnectSerie(film);
                    }
                }
                else
                {
                    if (serieVM != null)
                    {
                        _model.SeriesTable.Remove(serieVM.Serie);
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

        private void TableLoad()
        {
            CategoriesMenu.Clear();
            SimpleFilmsMenu.Clear();
            FilmsMenu.Clear();
            SeriesMenu.Clear();
            PriorityFilmsMenu.Clear();
            GenresTable.Clear();

            foreach (Genre genre in _model.GenresTable)
            {
                GenresTable.Add(new GenreButtonViewModel(genre));
            }

            foreach (Category category in _model.CategoriesTable)
            {
                CategoriesMenu.Add(new FilmCategoryViewModel(category, this));
                category.Films.CollectionChanged += CategoryChanged;
            }

            foreach (Film film in _model.FilmsTable)
            {
                film.PropertyChanged += FilmPropertyChanged;

                FilmsMenu.Add(new FilmViewModel(film, this));
                if (film.FranshiseId == 0)
                {
                    SimpleFilmsMenu.Add(new FilmViewModel(film, this));
                }
            }

            foreach (Serie serie in _model.SeriesTable)
            {
                SeriesMenu.Add(new FilmSerieViewModel(serie, this));
            }

            foreach (PriorityFilm book in _model.PriorityFilmsTable)
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
                    if (_model.FilmsTable.Contains(film))
                        SimpleFilmsMenu.Add(new FilmViewModel(film, this));
                    break;
                default:
                    break;
            }
        }

        public FilmInfoMenuCondition InfoMenuCondition
        {
            get => _infoMenuCondition;
            set
            {
                _infoMenuCondition = value;
                if (_infoMenuCondition == FilmInfoMenuCondition.Closed)
                {
                    InfoMenuDataContext = null;
                    SourcesCVS.Source = null;
                }

                OnPropertyChanged();
            }
        }

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

        public RelayCommand RemoveSourceCommand =>
        removeSourceCommand ?? (removeSourceCommand = new RelayCommand(obj =>
        {
            if (SourcesCVS.Source != null)
            {
                Source source = obj as Source;
                ObservableCollection<Source> sources = SourcesCVS.Source as ObservableCollection<Source>;
                sources.Remove(source);
            }
        }));

        public RelayCommand AddSourceCommand =>
        addSourceCommand ?? (addSourceCommand = new RelayCommand(obj =>
        {
            if (SourcesCVS.Source != null)
            {
                ObservableCollection<Source> sources = SourcesCVS.Source as ObservableCollection<Source>;
                sources.Add(new Source());
            }
        }));

        public RelayCommand MoveUpSourceCommand =>
        moveUpSourceCommand ?? (moveUpSourceCommand = new RelayCommand(obj =>
        {
            if (SourcesCVS.Source != null)
            {
                Source source = obj as Source;
                ObservableCollection<Source> sources = SourcesCVS.Source as ObservableCollection<Source>;
                sources.Move(sources.IndexOf(source), 0);
            }
        }));

        public RelayCommand CloseInfoCommand =>
        closeInfoCommand ?? (closeInfoCommand = new RelayCommand(obj =>
        {
            InfoMenuCondition = FilmInfoMenuCondition.Closed;
        }));
    }
}