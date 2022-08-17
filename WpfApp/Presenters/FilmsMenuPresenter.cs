using FilmsUCWpf.Presenter;
using FilmsUCWpf.Presenter.Interfaces;
using FilmsUCWpf.View;
using FilmsUCWpf.View.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TablesLibrary.Interpreter;
using TablesLibrary.Interpreter.TableCell;
using TL_Objects;
using TL_Objects.CellDataClasses;
using TL_Tables;
using WpfApp.Models;
using WpfApp.Views.Interfaces;

namespace WpfApp.Presenters
{
    public class FilmsMenuPresenter : IMenuPresenter<Film>
    {
        private FilmsMenuModel model;
        private IBaseMenuView view;
        IMenuModel<Film> IMenuPresenter<Film>.Model => model;
        public FilmsMenuPresenter(FilmsMenuModel model, IBaseMenuView view)
        {
            this.view = view;
            this.model = model;
            model.Presenter = this;

            model.FilmPresenters.CollectionChanged += presentersChanged;
            model.SeriePresenters.CollectionChanged += presentersChanged;
            model.PriorityPresenters.CollectionChanged += presentersChanged;
            model.CategoryPresenters.CollectionChanged += presentersChanged;
            model.GenreButtons.CollectionChanged += GenreButtons_CollectionChanged;

            model.TableLoad();
        }

        private void GenreButtons_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    view.GenresControls.Insert(e.NewStartingIndex, e.NewItems[0]);
                    break;
                case NotifyCollectionChangedAction.Remove:
                    view.GenresControls.RemoveAt(e.OldStartingIndex);
                    break;
                case NotifyCollectionChangedAction.Reset:
                    view.GenresControls.Clear();
                    break;
                default:
                    break;
            }
        }

        private void presentersChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (sender == model.GetCurrentPresenters())
            {
                switch (e.Action)
                {
                    case NotifyCollectionChangedAction.Add:
                        view.MenuControls.Insert(e.NewStartingIndex, ((IBasePresenter)e.NewItems[0]).View);
                        break;
                    case NotifyCollectionChangedAction.Remove:
                        view.MenuControls.RemoveAt(e.OldStartingIndex);
                        if (e.OldItems[0] == model.SelectedElement)
                            model.SelectedElement = null;
                        break;
                    case NotifyCollectionChangedAction.Reset:
                        view.MenuControls.Clear();
                        break;
                    default:
                        break;
                }
            }
        }

        public void SaveTables() => model.TableCollection.SaveTables();

        public void OpenMoreInfo(Object uiElement)
        {
            view.OpenMoreInfo(uiElement);
        }

        public void OpenSourcesInfo(ObservableCollection<Source> sources)
        {
            view.OpenSourcesInfo(sources);
        }

        public void LoadFilmTable()
        {
            model.SelectedElement = null;
            if (model.ControlsCondition != FilmsMenuModel.MenuCondition.Film)
            {
                model.ControlsCondition = FilmsMenuModel.MenuCondition.Film;
                view.MenuControls.Clear();
                foreach (IBasePresenter presenter in model.FilmPresenters)
                {
                    presenter.AddViewToCollection(view.MenuControls);
                }
                UnLockGenreButtons();
            }
        }

        public void LoadSerieTable()
        {
            model.SelectedElement = null;

            if (model.ControlsCondition != FilmsMenuModel.MenuCondition.Serie)
            {
                model.ControlsCondition = FilmsMenuModel.MenuCondition.Serie;
                view.MenuControls.Clear();
                foreach (IBasePresenter presenter in model.SeriePresenters)
                {
                    presenter.AddViewToCollection(view.MenuControls);
                }
                LockNotSerialGenreButtons();
            }
        }

        public void LoadCategories()
        {

            model.SelectedElement = null;

            if (model.ControlsCondition != FilmsMenuModel.MenuCondition.Category)
            {
                model.ControlsCondition = FilmsMenuModel.MenuCondition.Category;
                view.MenuControls.Clear();
                foreach (IBasePresenter presenter in model.CategoryPresenters)
                {
                    presenter.AddViewToCollection(view.MenuControls);
                }
                UnLockGenreButtons();
            }
        }

        public void LoadPriorityTable()
        {

            model.SelectedElement = null;

            if (model.ControlsCondition != FilmsMenuModel.MenuCondition.PriorityFilm)
            {
                model.ControlsCondition = FilmsMenuModel.MenuCondition.PriorityFilm;
                view.MenuControls.Clear();
                foreach (IBasePresenter presenter in model.PriorityPresenters)
                {
                    presenter.AddViewToCollection(view.MenuControls);
                }
                UnLockGenreButtons();
            }
        }

        private Genre[] GetSelectedGenres()
        {
            List<Genre> genres = new List<Genre>();
            foreach (GenrePressButton genreButton in view.GenresControls)
            {
                if (genreButton.IsChecked)
                {
                    genres.Add((Genre)genreButton.Genre);
                }
            }
            return genres.ToArray();
        }

        public void SearchByName(string name)
        {
            foreach (IBasePresenter presenter in model.GetCurrentPresenters())
            {
                presenter.SetVisualDefault();
            }

            if (name != "")
            {
                name = name.ToLowerInvariant();
                foreach (IBasePresenter presenter in model.GetCurrentPresenters())
                {
                    presenter.SetFindedElement(name);
                }
            }
        }

        private void addPresenterToView(IBasePresenter basePresenter)
        {
            basePresenter.AddViewToCollection(view.MenuControls);
        }

        private void addPresentersToView(IEnumerable presenters)
        {
            foreach (IBasePresenter presenter in presenters)
            {
                addPresenterToView(presenter);
            }
        }

        public void Filter(bool watched, bool unwatched)
        {
            view.MenuControls.Clear();
            Genre[] genres = GetSelectedGenres();


            if (genres.Length == model.Tables.GenresTable.Count && watched && unwatched)
            {
                foreach (IBasePresenter presenter in model.GetCurrentPresenters())
                {
                    addPresenterToView(presenter);
                }
            }
            else
            {
                if (watched && unwatched)
                {
                    foreach (IHasGenre hasGenre in model.GetCurrentPresenters())
                    {
                        if (hasGenre.HasSelectedGenre(genres))
                            addPresenterToView((IBasePresenter)hasGenre);
                    }
                }
                else
                {
                    foreach (IHasGenre hasGenre in model.GetCurrentPresenters())
                    {
                        IBasePresenter presenter = (IBasePresenter)hasGenre;
                        if (hasGenre.HasSelectedGenre(genres) && presenter.HasCheckedProperty(watched))
                            addPresenterToView((IBasePresenter)hasGenre);
                    }
                }
            }
        }

        private void LockNotSerialGenreButtons()
        {
            foreach (GenrePressButton button in view.GenresControls)
            {
                Genre genre = (Genre)button.Genre;
                if (!genre.IsSerialGenre)
                {
                    button.IsChecked = false;
                    //button.PressButton.ClickLocked = true;
                }
            }
        }

        private void UnLockGenreButtons()
        {
            foreach (GenrePressButton button in view.GenresControls)
            {
                //button.PressButton.ClickLocked = false;
                button.IsChecked = true;
            }
        }

        public void AddSelectedToPriority()
        {
            if (model.SelectedElement != null)
            {
                PriorityFilm priorityFilm = new PriorityFilm();
                priorityFilm.Film = model.SelectedElement.Model;
                model.Tables.PriorityFilmsTable.AddElement(priorityFilm);
            }
        }

        public void AddCategory()
        {
            if (model.ControlsCondition == FilmsMenuModel.MenuCondition.Category)
            {
                CategoriesTable categories = model.Tables.CategoriesTable;
                Category category = new Category();
                categories.AddElement(category);
            }
        }

        public void AddFilm()
        {
            Film film = new Film();
            film.Genre = model.Tables.GenresTable[0];
            model.Tables.FilmsTable.AddElement(film);
        }

        private IEnumerable<FilmCategoryPresenter> getCategoriesOnView()
        {
            List<FilmCategoryPresenter> presenters = new List<FilmCategoryPresenter>();
            Type categoryType = typeof(FilmCategoryPresenter);

            foreach (IView view in view.MenuControls)
            {
                Type presenterType = view.Presenter.GetType();
                if (categoryType == presenterType || presenterType.IsSubclassOf(categoryType))
                {
                    presenters.Add((FilmCategoryPresenter)view.Presenter);
                }
            }

            return presenters;
        }

        private IEnumerable<FilmPresenter> getFilmsOnView()
        {
            List<FilmPresenter> presenters = new List<FilmPresenter>();
            Type categoryType = typeof(FilmPresenter);

            foreach (IView view in view.MenuControls)
            {
                Type presenterType = view.Presenter.GetType();
                if (categoryType == presenterType || presenterType.IsSubclassOf(categoryType))
                {
                    presenters.Add((FilmPresenter)view.Presenter);
                }
            }

            return presenters;
        }

        public void SortByID()
        {
            IEnumerable<FilmCategoryPresenter> categories = getCategoriesOnView();
            IEnumerable<FilmPresenter> books = getFilmsOnView();
            view.MenuControls.Clear();

            switch (model.ControlsCondition)
            {
                case FilmsMenuModel.MenuCondition.Category:
                    addPresentersToView(categories.OrderBy(a => a.Model.ID));
                    addPresentersToView(books.OrderBy(a => a.Model.ID));
                    break;
                default:
                    addPresentersToView(books.OrderBy(a => a.Model.ID));
                    break;
            }
        }

        public void SortByName()
        {
            IEnumerable<FilmCategoryPresenter> categories = getCategoriesOnView();
            IEnumerable<FilmPresenter> books = getFilmsOnView();
            view.MenuControls.Clear();

            switch (model.ControlsCondition)
            {
                case FilmsMenuModel.MenuCondition.Category:
                    addPresentersToView(categories.OrderBy(a => a.Model.Name));
                    addPresentersToView(books.OrderBy(a => a.Model.Name));
                    break;
                default:
                    addPresentersToView(books.OrderBy(a => a.Model.Name));
                    break;
            }
        }

        public void SortByMark()
        {
            IEnumerable<FilmCategoryPresenter> categories = getCategoriesOnView();
            IEnumerable<FilmPresenter> books = getFilmsOnView();
            view.MenuControls.Clear();

            switch (model.ControlsCondition)
            {
                case FilmsMenuModel.MenuCondition.Category:
                    addPresentersToView(categories.OrderBy(a => a.Model.Mark).Reverse());
                    addPresentersToView(books.OrderBy(a => a.Model.Mark).Reverse());
                    break;
                default:
                    addPresentersToView(books.OrderBy(a => a.Model.Mark).Reverse());
                    break;
            }
        }

        public void SortByGenre()
        {
            IEnumerable<FilmPresenter> books = getFilmsOnView();
            view.MenuControls.Clear();

            addPresentersToView(books.OrderBy(a => a.Model.Genre.Name));
        }

        public void SortByYear()
        {
            IEnumerable<FilmPresenter> books = getFilmsOnView();
            view.MenuControls.Clear();
            addPresentersToView(books.OrderBy(a => a.Model.RealiseYear).Reverse());
        }

        public void SortByWatched()
        {
            IEnumerable<FilmPresenter> books = getFilmsOnView();
            view.MenuControls.Clear();
            addPresentersToView(books.OrderBy(a => a.Model.Watched).Reverse());
        }

        public void SortByDate()
        {
            IEnumerable<FilmPresenter> books = getFilmsOnView();
            view.MenuControls.Clear();
            addPresentersToView(books.OrderBy(a => a.Model.DateOfWatch).Reverse());
        }

        public void SortByCoV()
        {
            IEnumerable<FilmPresenter> books = getFilmsOnView();
            view.MenuControls.Clear();
            addPresentersToView(books.OrderBy(a => a.Model.CountOfViews).Reverse());
        }

        public void SortByStartDate()
        {
            IEnumerable<FilmPresenter> books = getFilmsOnView();
            view.MenuControls.Clear();

            addPresentersToView(books.OrderBy(a => a.Model.Serie.StartWatchDate).Reverse());
        }

        public void SortByWatchedSeries()
        {
            IEnumerable<FilmPresenter> books = getFilmsOnView();
            view.MenuControls.Clear();

            addPresentersToView(books.OrderBy(a => a.Model.Serie.CountOfWatchedSeries).Reverse());
        }

        public void SortByTotalSeries()
        {
            IEnumerable<FilmPresenter> books = getFilmsOnView();
            view.MenuControls.Clear();

            addPresentersToView(books.OrderBy(a => a.Model.Serie.TotalSeries).Reverse());
        }
    }
}
