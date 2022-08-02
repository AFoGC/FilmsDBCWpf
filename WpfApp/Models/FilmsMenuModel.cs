using CustomButtons;
using FilmsUCWpf.Presenter;
using FilmsUCWpf.Presenter.Interfaces;
using FilmsUCWpf.View;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using TablesLibrary.Interpreter;
using TL_Objects;

namespace WpfApp.Models
{
    public class FilmsMenuModel : IMenuModel<Film>
    {
        public enum MenuCondition
        {
            Category = 1,
            Film = 2,
            Serie = 3,
            PriorityFilm = 4
        }

        public IMenuPresenter<Film> Presenter { get; set; }
        public MenuCondition ControlsCondition { get; set; }
        public TLTables Tables => mainModel.Tables;
        public TableCollection TableCollection => mainModel.TableCollection;
        BasePresenter<Film> IMenuModel<Film>.SelectedElement { get => SelectedElement; set => SelectedElement = (FilmPresenter)value; }
        private FilmPresenter selectedElement = null;
        public FilmPresenter SelectedElement
        {
            get { return selectedElement; }
            set
            {
                if (selectedElement != null) selectedElement.SetVisualDefault();
                selectedElement = value;
            }
        }

        public ObservableCollection<IBasePresenter> CategoryPresenters { get; private set; }
        public ObservableCollection<FilmPresenter> FilmPresenters { get; private set; }
        public ObservableCollection<FilmPresenter> SeriePresenters { get; private set; }
        public ObservableCollection<FilmPriorityPresenter> PriorityPresenters { get; private set; }
        public ObservableCollection<GenrePressButtonControl> GenreButtons { get; private set; }

        public IEnumerable GetCurrentPresenters()
        {
            switch (ControlsCondition)
            {
                case MenuCondition.Category:
                    return CategoryPresenters;
                case MenuCondition.Film:
                    return FilmPresenters;
                case MenuCondition.Serie:
                    return SeriePresenters;
                case MenuCondition.PriorityFilm:
                    return PriorityPresenters;
                default:
                    return null;
            }
        }

        private readonly MainWindowModel mainModel;
        public FilmsMenuModel(MainWindowModel mainWindowModel)
        {
            mainModel = mainWindowModel;

            CategoryPresenters = new ObservableCollection<IBasePresenter>();
            FilmPresenters = new ObservableCollection<FilmPresenter>();
            SeriePresenters = new ObservableCollection<FilmPresenter>();
            PriorityPresenters = new ObservableCollection<FilmPriorityPresenter>();
            GenreButtons = new ObservableCollection<GenrePressButtonControl>();

            mainModel.TableCollection.TableLoad += TableCollection_TableLoad;

            mainModel.Tables.FilmsTable.CollectionChanged += FilmsTable_CollectionChanged;
            mainModel.Tables.SeriesTable.CollectionChanged += SeriesTable_CollectionChanged;
            mainModel.Tables.PriorityFilmsTable.CollectionChanged += PriorityFilmsTable_CollectionChanged;
            mainModel.Tables.CategoriesTable.CollectionChanged += CategoriesTable_CollectionChanged;
            mainModel.Tables.GenresTable.CollectionChanged += GenresTable_CollectionChanged;

            ControlsCondition = MenuCondition.Category;
        }

        private void GenresTable_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            Genre genre;
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    genre = (Genre)e.NewItems[0];
                    GenreButtons.Add(new GenrePressButtonControl(genre));
                    break;
                case NotifyCollectionChangedAction.Remove:
                    genre = (Genre)e.OldItems[0];
                    GenreButtons.Remove(GenreButtons.Where(x => x.Genre == genre).FirstOrDefault());
                    break;
                case NotifyCollectionChangedAction.Reset:
                    GenreButtons.Clear();
                    break;
                default:
                    break;
            }
        }

        private void CategoriesTable_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            Category category;
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    category = (Category)e.NewItems[0];
                    CategoryPresenters.Insert(Tables.CategoriesTable.Count - 1, new FilmCategoryPresenter(category, new FilmCategoryControl(), Presenter, TableCollection));
                    break;
                case NotifyCollectionChangedAction.Remove:
                    CategoryPresenters.Remove(CategoryPresenters.Where(x =>
                    {
                        category = (Category)e.OldItems[0];
                        if (x.GetType() == typeof(FilmCategoryPresenter))
                            return ((FilmCategoryPresenter)x).Model == category;
                        else
                            return false;

                    }).FirstOrDefault());
                    break;
                case NotifyCollectionChangedAction.Reset:
                    CategoryPresenters.Clear();
                    break;
                default:
                    break;
            }
        }

        private void PriorityFilmsTable_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            PriorityFilm priorityFilm;
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    priorityFilm = (PriorityFilm)e.NewItems[0];
                    PriorityPresenters.Add(new FilmPriorityPresenter(priorityFilm, new FilmPriorityControl(), Presenter, TableCollection));
                    break;
                case NotifyCollectionChangedAction.Remove:
                    priorityFilm = (PriorityFilm)e.OldItems[0];
                    PriorityPresenters.Remove(PriorityPresenters.Where(x => x.PriorityModel == priorityFilm).FirstOrDefault());
                    break;
                case NotifyCollectionChangedAction.Reset:
                    PriorityPresenters.Clear();
                    break;
                default:
                    break;
            }
        }

        private void SeriesTable_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            Serie serie;
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    serie = (Serie)e.NewItems[0];
                    SeriePresenters.Add(new FilmPresenter(serie.Film, new FilmSerieControl(), Presenter, TableCollection));
                    break;
                case NotifyCollectionChangedAction.Remove:
                    serie = (Serie)e.OldItems[0];
                    SeriePresenters.Remove(SeriePresenters.Where(x => x.Model.Serie == null).FirstOrDefault());
                    break;
                case NotifyCollectionChangedAction.Reset:
                    SeriePresenters.Clear();
                    break;
                default:
                    break;
            }
        }

        private void FilmsTable_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            Film film;
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    film = (Film)e.NewItems[0];
                    FilmPresenters.Add(new FilmPresenter(film, new FilmControl(), Presenter, TableCollection));
                    CategoryPresenters.Add(new FilmPresenter(film, new FilmSimpleControl(), Presenter, TableCollection));
                    break;
                case NotifyCollectionChangedAction.Remove:
                    film = (Film)e.OldItems[0];
                    FilmPresenters.Remove(FilmPresenters.Where(x => x.Model == film).FirstOrDefault());
                    PriorityPresenters.Remove(PriorityPresenters.Where(x => x.Model == film).FirstOrDefault());
                    SeriePresenters.Remove(SeriePresenters.Where(x => x.Model == film).FirstOrDefault());
                    CategoryPresenters.Remove(CategoryPresenters.Where(x =>
                    {
                        if (x.GetType() == typeof(FilmPresenter))
                            return ((FilmPresenter)x).Model == film;
                        else
                            return false;

                    }).FirstOrDefault());
                    break;
                case NotifyCollectionChangedAction.Reset:
                    FilmPresenters.Clear();
                    CategoryPresenters.Clear();
                    break;
                default:
                    break;
            }
        }

        private void TableCollection_TableLoad(object sender, EventArgs e)
        {
            TableLoad();
        }

        public void TableLoad()
        {
            PriorityPresenters.Clear();
            CategoryPresenters.Clear();
            SeriePresenters.Clear();
            FilmPresenters.Clear();
            GenreButtons.Clear();

            foreach (Genre genre in Tables.GenresTable)
            {
                GenreButtons.Add(new GenrePressButtonControl(genre));
            }

            foreach (Category category in Tables.CategoriesTable)
            {
                CategoryPresenters.Add(new FilmCategoryPresenter(category, new FilmCategoryControl(), Presenter, TableCollection));
            }

            foreach (Film film in Tables.FilmsTable)
            {
                FilmPresenters.Add(new FilmPresenter(film, new FilmControl(), Presenter, TableCollection));
                if (film.FranshiseId == 0)
                {
                    CategoryPresenters.Add(new FilmPresenter(film, new FilmSimpleControl(), Presenter, TableCollection));
                }
            }

            foreach (Serie serie in Tables.SeriesTable)
            {
                SeriePresenters.Add(new FilmPresenter(serie.Film, new FilmSerieControl(), Presenter, TableCollection));
            }

            foreach (PriorityFilm priorityFilm in Tables.PriorityFilmsTable)
            {
                PriorityPresenters.Add(new FilmPriorityPresenter(priorityFilm, new FilmPriorityControl(), Presenter, TableCollection));
            }
        }

        public IEnumerable<FilmCategoryPresenter> GetFilmCategoryPresenters()
        {
            List<FilmCategoryPresenter> export = new List<FilmCategoryPresenter>();
            foreach (var item in CategoryPresenters)
            {
                if (item.GetType() == typeof(FilmCategoryPresenter))
                {
                    export.Add((FilmCategoryPresenter)item);
                }
            }
            return export;
        }

        public IEnumerable<FilmPresenter> GetFilmSimplePresenters()
        {
            List<FilmPresenter> export = new List<FilmPresenter>();
            foreach (var item in CategoryPresenters)
            {
                if (item.GetType() == typeof(FilmPresenter))
                {
                    export.Add((FilmPresenter)item);
                }
            }
            return export;
        }

        public bool AddElement(Film film)
        {
            int i = 0;
            Type type = typeof(FilmPresenter);
            foreach (IBasePresenter item in CategoryPresenters)
            {
                if (item.GetType() == type)
                {
                    Film filmInPresenter = (Film)item.ModelCell;
                    if (filmInPresenter.ID > film.ID) break;
                }
                ++i;
            }
            FilmPresenter filmPresenter = new FilmPresenter(film, new FilmSimpleControl(), Presenter, TableCollection);
            CategoryPresenters.Insert(i, filmPresenter);
            return true;
        }

        public bool RemoveElement(Film film)
        {
            Type type = typeof(FilmPresenter);
            FilmPresenter presenterInCategory;
            foreach (IBasePresenter presenter in CategoryPresenters)
            {
                if (presenter.GetType() == type)
                {
                    presenterInCategory = (FilmPresenter)presenter;
                    if (presenterInCategory.Model == film)
                    {
                        CategoryPresenters.Remove(presenterInCategory);
                        break;
                    }
                }
            }
            return false;
        }
    }
}
