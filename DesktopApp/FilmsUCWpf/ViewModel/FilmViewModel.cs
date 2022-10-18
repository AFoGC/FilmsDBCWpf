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
using System.Windows.Documents;
using TL_Objects;
using TL_Objects.Interfaces;
using TL_Tables;

namespace FilmsUCWpf.ViewModel
{
    public class FilmViewModel : BaseViewModel<Film>, IHasGenre, IFilter, IFinded
    {
        private readonly GenresTable genresTable;
        private readonly SeriesTable seriesTable;
        private readonly IMenuViewModel<Film> menu;

        public FilmViewModel(Film model, IMenuViewModel<Film> menu) : base(model)
        {
            model.PropertyChanged += ModelPropertyChanged;
            this.menu = menu;
            genresTable = (GenresTable)model.Genre.ParentTable;
            seriesTable = (SeriesTable)Model.ParentTable.TableCollection.GetTable<Serie>();
        }

        private RelayCommand selectCommand;
        public RelayCommand SelectCommand
        {
            get
            {
                return selectCommand ??
                (selectCommand = new RelayCommand(obj =>
                {
                    menu.SelectedElement = this;
                }));
            }
        }

        private RelayCommand copyUrlCommand;
        public RelayCommand CopyUrlCommand
        {
            get
            {
                return copyUrlCommand ??
                (copyUrlCommand = new RelayCommand(obj =>
                {
                    Helper.CopyFirstSource(Model.Sources);
                }));
            }
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

        private RelayCommand openInfoCommand;
        public RelayCommand OpenInfoCommand
        {
            get
            {
                return openInfoCommand ??
                (openInfoCommand = new RelayCommand(obj =>
                {
                    menu.OpenInfoMenu(Model);
                }));
            }
        }

        private RelayCommand openSourceCommand;
        public RelayCommand OpenSourceCommand
        {
            get
            {
                return openSourceCommand ??
                (openSourceCommand = new RelayCommand(obj =>
                {
                    menu.OpenSourcesMenu(Model.Sources);
                }));
            }
        }

        public RelayCommand addToPriorityCommand;
        public RelayCommand AddToPriorityCommand
        {
            get
            {
                return addToPriorityCommand ??
                (addToPriorityCommand = new RelayCommand(obj =>
                {
                    PriorityFilmsTable priorityBooks = (PriorityFilmsTable)TableCollection.GetTable<PriorityFilm>();
                    if (!priorityBooks.ContainFilm(Model))
                    {
                        PriorityFilm priority = new PriorityFilm();
                        priority.Film = Model;
                        priorityBooks.AddElement(priority);
                    }
                }));
            }
        }

        public RelayCommand removeFromPriorityCommand;
        public RelayCommand RemoveFromPriorityCommand
        {
            get
            {
                return removeFromPriorityCommand ??
                (removeFromPriorityCommand = new RelayCommand(obj =>
                {
                    PriorityFilmsTable priorityFilms = (PriorityFilmsTable)TableCollection.GetTable<PriorityFilm>();
                    IEnumerable<PriorityFilm> enumerable = priorityFilms as IEnumerable<PriorityFilm>;
                    priorityFilms?.Remove(enumerable.Where(x => x.Film == Model).FirstOrDefault());
                }));
            }
        }

        private RelayCommand upInCategoryIDCommand;
        public RelayCommand UpInCategoryIDCommand
        {
            get
            {
                return upInCategoryIDCommand ??
                (upInCategoryIDCommand = new RelayCommand(obj =>
                {
                    CategoriesTable categories = (CategoriesTable)TableCollection.GetTable<Category>();
                    Category category = categories.GetCategoryByFilm(Model);
                    category.ChangeFilmPositionBy(Model, -1);
                }));
            }
        }

        private RelayCommand downInCategoryIDCommand;
        public RelayCommand DownInCategoryIDCommand
        {
            get
            {
                return downInCategoryIDCommand ??
                (downInCategoryIDCommand = new RelayCommand(obj =>
                {
                    CategoriesTable categories = (CategoriesTable)TableCollection.GetTable<Category>();
                    Category category = categories.GetCategoryByFilm(Model);
                    category.ChangeFilmPositionBy(Model, 1);
                }));
            }
        }

        private RelayCommand removeFromCategoryCommand;
        public RelayCommand RemoveFromCategoryCommand
        {
            get
            {
                return removeFromCategoryCommand ??
                (removeFromCategoryCommand = new RelayCommand(obj =>
                {
                    CategoriesTable categories = (CategoriesTable)TableCollection.GetTable<Category>();
                    Category category = categories.GetCategoryByFilm(Model);
                    if (category != null)
                    {
                        category.Films.Remove(Model);
                    }
                }));
            }
        }

        private RelayCommand deleteFilmCommand;
        public RelayCommand DeleteFilmCommand
        {
            get
            {
                return deleteFilmCommand ??
                (deleteFilmCommand = new RelayCommand(obj =>
                {
                    FilmsTable filmsTable = (FilmsTable)TableCollection.GetTable<Film>();
                    filmsTable.Remove(Model);
                }));
            }
        }

        private RelayCommand baseAutoFill;
        public RelayCommand BaseAutoFill
        {
            get
            {
                return baseAutoFill ??
                (baseAutoFill = new RelayCommand(obj =>
                {
                    if (Model.Watched == false)
                    {
                        if (Model.CountOfViews == 0)
                        {
                            Model.CountOfViews = 1;
                        }
                    }
                }));
            }
        }

        private RelayCommand fullAutoFill;
        public RelayCommand FullAutoFill
        {
            get
            {
                return fullAutoFill ??
                (fullAutoFill = new RelayCommand(obj =>
                {
                    BaseAutoFill.Execute(obj);
                    if (Model.Watched == false)
                    {
                        if (Model.DateOfWatch == defaultDate)
                        {
                            Model.DateOfWatch = DateTime.Today;
                        }

                        Model.Watched = true;
                    }
                }));
            }
        }

        private RelayCommand openComment;
        public RelayCommand OpenComment
        {
            get
            {
                return openComment ??
                (openComment = new RelayCommand(obj => 
                {
                    if (CommentVisibility == Visibility.Visible)
                    {
                        CommentVisibility = Visibility.Collapsed;
                    }
                    else
                    {
                        CommentVisibility = Visibility.Visible;
                    }
                }));
            }
        }

        public bool SetFinded(string search)
        {
            IsFinded = Model.Name.ToLowerInvariant().Contains(search);
            return IsFinded;
        }

        public bool HasSelectedGenre(IGenre[] selectedGenres)
        {
            foreach (IGenre genre in selectedGenres)
            {
                if (genre == Model.Genre)
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
                passedFilter = Model.Watched == isReadedChecked || Model.Watched != isUnReadedChecked;
            }

            IsFiltered = passedFilter;
            return passedFilter;
        }

        private void ModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Model.Genre))
            {
                OnPropertyChanged(nameof(GenreText));
                OnPropertyChanged(nameof(SelectedGenre));
                return;
            }
            if (e.PropertyName == nameof(Model.DateOfWatch))
            {
                OnPropertyChanged(nameof(DateOfWatchTxt));
                return;
            }

            OnPropertyChanged(e);
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
        public virtual String Name
        {
            get => Model.Name;
            set => Model.Name = value;
        }
        public String GenreText
        {
            get => Model.Genre.ToString();
            set { }
        }
        public Genre SelectedGenre
        {
            get => Model.Genre;
            set
            {
                /*
                if (!Model.Genre.IsSerialGenre && value.IsSerialGenre)
                {
                    seriesTable.FindAndConnectSerie(Model);
                }

                if (Model.Genre.IsSerialGenre && !value.IsSerialGenre)
                {
                    Model.Serie.Film = null;
                }
                */

                Model.Genre = value;
            }
        }

        public INotifyCollectionChanged GenresCollection => genresTable;

        private Visibility commentVisibility = Visibility.Collapsed;
        public Visibility CommentVisibility
        {
            get => commentVisibility;
            set
            {
                commentVisibility = value;
                OnPropertyChanged();
            }
        }
        public String RealiseYear
        {
            get => formatZero(Model.RealiseYear);
            set => Model.RealiseYear = formatEmpty(value);
        }
        public Boolean Watched
        {
            get => Model.Watched;
            set => Model.Watched = value;
        }
        public String DateOfWatchTxt
        {
            get => FormateDate(Model.DateOfWatch);
            set { }
        }
        public DateTime DateOfWatch
        {
            get => Model.DateOfWatch;
            set => Model.DateOfWatch = value;
        }

        public String Mark
        {
            get => Model.FormatedMark.ToString();
            set => Model.FormatedMark.SetMarkFromString(value);
        }
        public List<String> Marks => Model.FormatedMark.GetComboItems();
        public String CountOfViews
        {
            get => formatZero(Model.CountOfViews);
            set => Model.CountOfViews = formatEmpty(value);
        }
        public String Comment
        {
            get => Model.Comment;
            set => Model.Comment = value;
        }
        public String Sources
        {
            get => Helper.SourcesStateString(Model.Sources);
            set { }
        }
    }
}
