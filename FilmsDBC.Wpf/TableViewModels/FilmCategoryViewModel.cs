using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using TL_Objects;
using TL_Objects.Interfaces;
using TL_Tables;
using WpfApp.Commands;
using WpfApp.TableViewModels.Interfaces;

namespace WpfApp.TableViewModels
{
    public class FilmCategoryViewModel : BaseViewModel<Category>
    {
        private readonly IMenuViewModel<Film> menu;

        private bool _isCollectionVisible = true;
        private bool _isFiltered = true;

        private RelayCommand openUpdateCommand;
        private RelayCommand createBookCommand;
        private RelayCommand addSelectedCommand;
        private RelayCommand removeSelectedCommand;
        private RelayCommand deleteCategoryCommand;
        private RelayCommand collapseCommand;
        private RelayCommand contextMenuOpenedCommand;
        private RelayCommand contextMenuClosedCommand;

        public FilmCategoryViewModel(Category model, IMenuViewModel<Film> menu) : base(model)
        {
            this.menu = menu;
            FilmsVMs = new ObservableCollection<FilmInCategoryViewModel>();

            model.PropertyChanged += ModelPropertyChanged;
            model.FormatedMark.PropertyChanged += MarkPropertyChanged;
            model.CategoryElements.CollectionChanged += BooksCollectionChanged;

            FillCategoryFilms();
        }

        private void FillCategoryFilms()
        {
            foreach (Film film in Model.CategoryElements)
            {
                FilmInCategoryViewModel vm = new FilmInCategoryViewModel(film, menu);
                FilmsVMs.Add(vm);
            }
        }

        public override bool HasSelectedGenre(IGenre[] selectedGenres)
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

        public override bool Filter(IGenre[] selectedGenres, bool isReadedChecked, bool isUnReadedChecked)
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

        public override bool SetFinded(string search)
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
        
        public RelayCommand CreateBookCommand
        {
            get
            {
                return createBookCommand ??
                (createBookCommand = new RelayCommand(obj =>
                {
                    Film film = new Film();
                    film.Name = GetDefaulBookName();
                    film.Genre = TableCollection.GetTable<Genre>()[0];
                    TableCollection.GetTable<Film>().AddElement(film);
                    Model.CategoryElements.Add(film);
                }));
            }
        }

        private string GetDefaulBookName()
        {
            if (Model.HideName == string.Empty)
                return Model.Name;
            else
                return Model.HideName;
        }
        
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
                            Model.CategoryElements.Add(film);
                            menu.SelectedElement = null;
                        }
                    }
                }));
            }
        }
        
        public RelayCommand RemoveSelectedCommand
        {
            get
            {
                return removeSelectedCommand ??
                (removeSelectedCommand = new RelayCommand(obj =>
                {
                    Film film = menu.SelectedElement.Model;
                    if (Model.CategoryElements.Remove(film))
                    {
                        menu.SelectedElement = null;
                    }
                }));
            }
        }
        
        public RelayCommand DeleteCategoryCommand
        {
            get
            {
                return deleteCategoryCommand ??
                (deleteCategoryCommand = new RelayCommand(obj =>
                {
                    if (Model.CategoryElements.Count == 0)
                    {
                        CategoriesTable categories = (CategoriesTable)TableCollection.GetTable<Category>();
                        categories.Remove(Model);
                    }
                }));
            }
        }
        
        public RelayCommand CollapseCommand
        {
            get
            {
                return collapseCommand ??
                (collapseCommand = new RelayCommand(obj =>
                {
                    IsCollectionVisible = !IsCollectionVisible;
                }));
            }
        }
        
        public RelayCommand CMOpenedCommand
        {
            get
            {
                return contextMenuOpenedCommand ??
                (contextMenuOpenedCommand = new RelayCommand(obj =>
                {
                    IsSelected = true;
                }));
            }
        }
        
        public RelayCommand CMClosedCommand
        {
            get
            {
                return contextMenuClosedCommand ??
                (contextMenuClosedCommand = new RelayCommand(obj =>
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
        
        public bool IsCollectionVisible
        {
            get => _isCollectionVisible;
            set
            {
                _isCollectionVisible = value;
                OnPropertyChanged();
            }
        }
        
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
