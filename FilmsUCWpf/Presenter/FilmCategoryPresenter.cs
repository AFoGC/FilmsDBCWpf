using FilmsUCWpf.Presenter.Interfaces;
using FilmsUCWpf.View;
using FilmsUCWpf.View.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TablesLibrary.Interpreter;
using TablesLibrary.Interpreter.Table;
using TL_Objects;
using TL_Objects.Interfaces;

namespace FilmsUCWpf.Presenter
{
    public class FilmCategoryPresenter : BasePresenter<Category>, IHasGenre
    {

        protected IMenu<Film> menu;
        private List<FilmPresenter> presenters;

        new ICategoryView View { get => (ICategoryView)base.View; }

        public FilmCategoryPresenter(Category category, ICategoryView view, IMenu<Film> menu, TableCollection collection) : base(category, view, collection)
        {
            this.menu = menu;
            presenters = new List<FilmPresenter>();
            category.Films.CollectionChanged += Films_CollectionChanged;
            refreshCategoryFilms();
        }

        private void refreshCategoryFilms()
        {
            View.CategoryCollection.Clear();
            presenters.Clear();
            View.Height = View.DefaultHeght;

            foreach (Film film in Model.Films)
            {
                FilmPresenter filmPresenter = new FilmPresenter(film, new FilmInCategorySimpleControl(), menu, TableCollection);
                presenters.Add(filmPresenter);
                View.Height += filmPresenter.View.Height;
                View.CategoryCollection.Add(filmPresenter.View);
            }
        }

        private void Films_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            Film film;
            FilmPresenter presenter;
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    film = (Film)e.NewItems[0];
                    presenter = new FilmPresenter(film, new FilmInCategorySimpleControl(), menu, TableCollection);
                    presenters.Add(presenter);
                    View.CategoryCollection.Add(presenter.View);
                    break;
                case NotifyCollectionChangedAction.Remove:
                    film = (Film)e.OldItems[0];
                    presenter = presenters.Where(x => x.Model == film).FirstOrDefault();
                    presenters.Remove(presenter);
                    View.CategoryCollection.Remove(presenter?.View);
                    break;
                case NotifyCollectionChangedAction.Reset:
                    presenters.Clear();
                    View.CategoryCollection.Clear();
                    break;
                case NotifyCollectionChangedAction.Move:
                    IView view = null;
                    foreach (IView item in View.CategoryCollection)
                    {
                        if (item.Presenter.ModelCell == e.OldItems[0])
                        {
                            view = item;
                            break;
                        }
                    }
                    View.CategoryCollection.Remove(view);
                    View.CategoryCollection.Insert(e.NewStartingIndex, view);
                    break;
                default:
                    break;
            }
        }

        public override bool HasCheckedProperty(bool isWached)
        {
            foreach (IBasePresenter presenter in presenters)
            {
                if (presenter.HasCheckedProperty(isWached))
                {
                    return true;
                }
            }

            return false;
        }

        public override bool SetFindedElement(string search)
        {
            bool export = false;
            if (Model.Name.ToLowerInvariant().Contains(search))
            {
                View.SetVisualFinded();
            }

            foreach (FilmPresenter presenter in presenters)
            {
                presenter.SetFindedElement(search);
            }

            return export;
        }

        public override void SetSelectedElement()
        {
            View.SetVisualSelected();
        }

        public override void SetVisualDefault()
        {
            View.SetVisualDefault();
            foreach (FilmPresenter presenter in presenters)
            {
                presenter.SetVisualDefault();
            }
        }

        public bool HasSelectedGenre(IGenre[] selectedGenres)
        {
            foreach (FilmPresenter presenter in presenters)
            {
                if (presenter.HasSelectedGenre(selectedGenres))
                {
                    return true;
                }
            }

            return false;
        }

        public void OpenUpdateMenu()
        {
            menu.UpdateFormVisualizer.OpenUpdateControl(new FilmCategoryUpdateControl(Model, menu, TableCollection));
        }

        public void CreateFilmInCategory()
        {
            Film film = new Film();
            film.Genre = TableCollection.GetTable<Genre>()[0];
            Model.Films.Add(film);
            TableCollection.GetTable<Film>().AddElement(film);
        }

        public void AddSelected()
        {
            if (menu.SelectedElement != null)
            {
                Film film = menu.SelectedElement.Model;
                if (film.FranshiseId == 0)
                {
                    
                    Model.Films.Add(film);
                    menu.RemovePresenter(menu.SelectedElement);
                    menu.SelectedElement = null;
                }
            }
        }

        public void RemoveSelected()
        {
            if (menu.SelectedElement != null)
            {
                Film film = menu.SelectedElement.Model;
                if (Model.RemoveFilmFromCategory(film))
                {
                    menu.AddPresenter(menu.SelectedElement);
                    menu.SelectedElement = null;
                }
            }
        }

        public void DeleteThisCategory()
        {
            Table<Category> cateories = TableCollection.GetTable<Category>();
            if (Model.Films.Count == 0)
            {
                cateories.Remove(Model);
            }
        }
    }
}
