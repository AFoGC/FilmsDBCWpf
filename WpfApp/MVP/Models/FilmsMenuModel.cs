using CustomButtons;
using FilmsUCWpf.Presenter;
using FilmsUCWpf.PresenterInterfaces;
using FilmsUCWpf.View;
using InfoMenusWpf.MoreInfo;
using InfoMenusWpf.UpdateInfo;
using ProfilesConfig;
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

namespace WpfApp.MVP.Models
{
    public class FilmsMenuModel : IMenu<Film>
    {
        public enum MenuCondition
        {
            Category = 1,
            Film = 2,
            Serie = 3,
            PriorityFilm = 4
        }

        public MenuCondition ControlsCondition { get; set; }
        public MoreInfoFormVisualizer MoreInfoFormVisualizer { get; set; }
        public UpdateFormVisualizer UpdateFormVisualizer { get; set; }
        public TLTables Tables => mainModel.Tables;
        public TableCollection TableCollection => mainModel.TableCollection;
        BasePresenter<Film> IMenu<Film>.SelectedElement { get => SelectedElement; set => SelectedElement = (FilmPresenter)value; }
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
                    CategoryPresenters.Insert(Tables.BookCategoriesTable.Count - 1, new FilmCategoryPresenter(category, new FilmCategoryControl(), this, TableCollection));
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
                    PriorityPresenters.Add(new FilmPriorityPresenter(priorityFilm, new FilmPriorityControl(), this, TableCollection));
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
                    SeriePresenters.Add(new FilmPresenter(serie.Film, new FilmSerieControl(), this, TableCollection));
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
                    FilmPresenters.Add(new FilmPresenter(film, new FilmControl(), this, TableCollection));
                    CategoryPresenters.Add(new FilmPresenter(film, new FilmSimpleControl(), this, TableCollection));
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
                CategoryPresenters.Add(new FilmCategoryPresenter(category, new FilmCategoryControl(), this, TableCollection));
            }

            foreach (Film film in Tables.FilmsTable)
            {
                FilmPresenters.Add(new FilmPresenter(film, new FilmControl(), this, TableCollection));
                if (film.FranshiseId == 0)
                {
                    CategoryPresenters.Add(new FilmPresenter(film, new FilmSimpleControl(), this, TableCollection));
                }
            }

            foreach (Serie serie in Tables.SeriesTable)
            {
                SeriePresenters.Add(new FilmPresenter(serie.Film, new FilmSerieControl(), this, TableCollection));
            }

            foreach (PriorityFilm priorityFilm in Tables.PriorityFilmsTable)
            {
                PriorityPresenters.Add(new FilmPriorityPresenter(priorityFilm, new FilmPriorityControl(), this, TableCollection));
            }
        }

        public bool AddSelected()
        {
            if (SelectedElement != null)
            {
                int i = 0;
                Type type = typeof(BasePresenter<Film>);
                foreach (IBasePresenter item in CategoryPresenters)
                {
                    if (item.GetType().IsSubclassOf(type))
                    {
                        BasePresenter<Film> basePresenter = (BasePresenter<Film>)item;
                        Film film = basePresenter.Model;
                        if (film.ID > SelectedElement.Model.ID) break;
                    }
                    ++i;
                }
                FilmPresenter presenter = new FilmPresenter(SelectedElement.Model, new FilmSimpleControl(), this, TableCollection);
                CategoryPresenters.Insert(i, presenter);
                SelectedElement = null;
                return true;
            }
            else return false;
        }

        public bool RemoveSelected()
        {
            if (SelectedElement != null)
            {
                bool exp = CategoryPresenters.Remove(SelectedElement);
                SelectedElement = null;
                return exp;
            }
            else return false;
        }
    }
}
