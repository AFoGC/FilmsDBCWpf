using FilmsUCWpf.Command;
using FilmsUCWpf.ViewModel.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TL_Objects;
using TL_Objects.Interfaces;
using TL_Tables;

namespace FilmsUCWpf.ViewModel
{
    public class FilmCategoryViewModel : BaseViewModel<Category>, IHasGenre, IFilter, IFinded
    {
        private readonly IMenuViewModel<Film> menu;
        public FilmCategoryViewModel(Category model, IMenuViewModel<Film> menu) : base(model)
        {
            this.menu = menu;
            FilmsVMs = new ObservableCollection<FilmInCategoryViewModel>();

            model.PropertyChanged += ModelPropertyChanged;
            model.FormatedMark.PropertyChanged += MarkPropertyChanged;
            model.Films.CollectionChanged += BooksCollectionChanged;

            fillCategoryFilms();
        }

        private void fillCategoryFilms()
        {
            foreach (Film film in Model.Films)
            {
                FilmInCategoryViewModel vm = new FilmInCategoryViewModel(film, menu);
                FilmsVMs.Add(vm);
            }
        }

        public bool HasSelectedGenre(IGenre[] selectedGenres)
        {
            foreach (FilmInCategoryViewModel vm in FilmsVMs)
            {
                if (vm.HasSelectedGenre(selectedGenres))
                {
                    return true;
                }
            }

            return false;
        }

        public bool Filter(IGenre[] selectedGenres, bool isReadedChecked, bool isUnReadedChecked)
        {
            bool passedFilter = false;

            if (HasSelectedGenre(selectedGenres))
            {
                foreach (var vm in FilmsVMs)
                {
                    if (vm.Filter(selectedGenres, isReadedChecked, isUnReadedChecked))
                    {
                        passedFilter = true;
                    }
                }
            }

            IsFiltered = passedFilter;
            return passedFilter;
        }

        public bool SetFinded(string search)
        {
            IsFinded = Model.Name.ToLower().Contains(search);

            foreach (FilmInCategoryViewModel vm in FilmsVMs)
            {
                if (vm.SetFinded(search))
                {
                    IsFinded = true;
                }
            }

            return IsFinded;
        }

        private RelayCommand openUpdateCommand;
        public RelayCommand OpenUpdateCommand
        {
            get
            {
                return openUpdateCommand ??
                (openUpdateCommand = new RelayCommand(obj =>
                {
                    menu.OpenUpdateMenu(Model);
                }));
            }
        }

        private RelayCommand createBookCommand;
        public RelayCommand CreateBookCommand
        {
            get
            {
                return createBookCommand ??
                (createBookCommand = new RelayCommand(obj =>
                {
                    Film film = new Film();
                    film.Name = getDefaulBookName();
                    film.Genre = TableCollection.GetTable<Genre>()[0];
                    TableCollection.GetTable<Film>().AddElement(film);
                    Model.Films.Add(film);
                }));
            }
        }

        private string getDefaulBookName()
        {
            if (Model.HideName == string.Empty)
                return Model.Name;
            else
                return Model.HideName;
        }

        private RelayCommand addSelectedCommand;
        public RelayCommand AddSelectedCommand
        {
            get
            {
                return addSelectedCommand ??
                (addSelectedCommand = new RelayCommand(obj =>
                {
                    if (menu.SelectedElement != null)
                    {
                        Film film = menu.SelectedElement.Model;
                        if (film.FranshiseId == 0)
                        {
                            Model.Films.Add(film);
                            menu.SelectedElement = null;
                        }
                    }
                }));
            }
        }

        private RelayCommand removeSelectedCommand;
        public RelayCommand RemoveSelectedCommand
        {
            get
            {
                return removeSelectedCommand ??
                (removeSelectedCommand = new RelayCommand(obj =>
                {
                    Film film = menu.SelectedElement.Model;
                    if (Model.Films.Remove(film))
                    {
                        menu.SelectedElement = null;
                    }
                }));
            }
        }

        private RelayCommand deleteCategoryCommand;
        public RelayCommand DeleteCategoryCommand
        {
            get
            {
                return deleteCategoryCommand ??
                (deleteCategoryCommand = new RelayCommand(obj =>
                {
                    if (Model.Films.Count == 0)
                    {
                        CategoriesTable categories = (CategoriesTable)TableCollection.GetTable<Category>();
                        categories.Remove(Model);
                    }
                }));
            }
        }

        private RelayCommand collapseCommand;
        public RelayCommand CollapseCommand
        {
            get
            {
                return collapseCommand ??
                (collapseCommand = new RelayCommand(obj =>
                {
                    if (CollectionVisiblility == Visibility.Visible)
                        CollectionVisiblility = Visibility.Collapsed;
                    else
                        CollectionVisiblility = Visibility.Visible;
                }));
            }
        }

        private RelayCommand _CMOpenedCommand;
        public RelayCommand CMOpenedCommand
        {
            get
            {
                return _CMOpenedCommand ??
                (_CMOpenedCommand = new RelayCommand(obj =>
                {
                    IsSelected = true;
                }));
            }
        }

        private RelayCommand _CMClosedCommand;
        public RelayCommand CMClosedCommand
        {
            get
            {
                return _CMClosedCommand ??
                (_CMClosedCommand = new RelayCommand(obj =>
                {
                    IsSelected = false;
                }));
            }
        }

        private void BooksCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            Film film;
            FilmInCategoryViewModel vm;
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    film = (Film)e.NewItems[0];
                    vm = new FilmInCategoryViewModel(film, menu);
                    FilmsVMs.Add(vm);
                    break;
                case NotifyCollectionChangedAction.Remove:
                    film = (Film)e.OldItems[0];
                    vm = FilmsVMs.Where(x => x.Model == film).FirstOrDefault();
                    FilmsVMs.Remove(vm);
                    break;
                case NotifyCollectionChangedAction.Reset:
                    FilmsVMs.Clear();
                    break;
                case NotifyCollectionChangedAction.Move:
                    FilmsVMs.Move(e.OldStartingIndex, e.NewStartingIndex);
                    break;
                default:
                    break;
            }
        }

        private void MarkPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            OnPropertyChanged(nameof(Mark));
            if (e.PropertyName == nameof(Model.FormatedMark.MarkSystem))
            {
                OnPropertyChanged(nameof(Marks));
            }
        }

        private void ModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            OnPropertyChanged(e);
        }

        private Visibility _collectionVisibility = Visibility.Visible;
        public Visibility CollectionVisiblility
        {
            get => _collectionVisibility;
            set { _collectionVisibility = value; OnPropertyChanged(); }
        }

        private bool _isFiltered = true;
        public bool IsFiltered
        {
            get => _isFiltered;
            set
            {
                _isFiltered = value;
                OnPropertyChanged();
            }
        }

        public String ID
        {
            get => Model.ID.ToString();
            set { }
        }
        public String Name
        {
            get => Model.Name;
            set => Model.Name = value;
        }
        public String HideName
        {
            get => Model.HideName;
            set => Model.HideName = value;
        }
        public String Mark
        {
            get => Model.FormatedMark.ToString();
            set => Model.FormatedMark.SetMarkFromString(value);
        }
        public List<String> Marks => Model.FormatedMark.GetComboItems();

        public ObservableCollection<FilmInCategoryViewModel> FilmsVMs { get; private set; }
    }
}
