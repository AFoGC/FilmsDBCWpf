using FilmsUCWpf.PresenterInterfaces;
using FilmsUCWpf.View;
using FilmsUCWpf.ViewInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TablesLibrary.Interpreter;
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
            RefreshCategoryFilms();
        }

        private void Films_CollectionChanged(object sender, EventArgs e)
        {
            RefreshCategoryFilms();
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

        private void AddViewPresenter(FilmPresenter presenter)
        {
            View.Height += 15;
            View.CategoryCollection.Add(presenter.View);
        }

        public void RefreshCategoryFilms()
        {
            View.CategoryCollection.Clear();
            presenters.Clear();
            View.Height = View.DefaultHeght;

            foreach (Film film in Model.Films)
            {
                FilmPresenter filmPresenter = new FilmPresenter(film, new FilmInCategorySimpleControl(), menu, TableCollection);
                presenters.Add(filmPresenter);
                AddViewPresenter(filmPresenter);
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
            TableCollection.GetTable<Film>().AddElement(film);
            Model.Films.Add(film);
        }
    }
}
