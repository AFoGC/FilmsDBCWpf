﻿using FilmsUCWpf.PresenterInterfaces;
using FilmsUCWpf.View;
using FilmsUCWpf.ViewInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TL_Objects;
using TL_Objects.Interfaces;

namespace FilmsUCWpf.Presenter
{
    public class FilmCategoryPresenter : BasePresenter<Category>, IHasGenre
    {

        protected IMenu<Film> menu;
        private List<FilmPresenter> presenters;

        new ICategoryView View { get => (ICategoryView)base.View; }

        public FilmCategoryPresenter(Category category, ICategoryView view, IMenu<Film> menu) : base(category, view)
        {
            this.menu = menu;
            presenters = new List<FilmPresenter>();
            category.Films.CollectionChanged += Films_CollectionChanged;
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

        private void AddPresenter(Film film)
        {
            View.Height += 15;
            FilmSimpleControl view = new FilmSimpleControl();
            FilmPresenter presenter = new FilmPresenter(film, view, menu);
            presenters.Add(presenter);

            View.CategoryCollection.Add(view);
        }

        private void AddViewPresenter(FilmPresenter presenter)
        {
            View.Height += 15;
            View.CategoryCollection.Add(presenter.View);
        }

        public bool RemoveFilmFromCategory(FilmPresenter presenter)
        {
            if (Model.RemoveFilmFromCategory(presenter.Model))
            {
                View.CategoryCollection.Remove(presenter.View);
                presenters.Remove(presenter);
                View.Height -= 15;

                foreach (Film film in Model.Films)
                {
                    if (presenter.Model.FranshiseListIndex < film.FranshiseListIndex)
                    {
                        --film.FranshiseListIndex;
                    }
                }
                return true;
            }
            return false;
        }

        private void RefreshCategoryFilms()
        {
            View.CategoryCollection.Clear();
            presenters.Clear();
            View.Height = 20;

            foreach (Film film in Model.Films)
            {
                presenters.Add(new FilmPresenter(film, new FilmSimpleControl(), menu));
            }

            for (int i = 0; i < presenters.Count; i++)
            {
                FilmPresenter presenter = presenters[i];
                if (presenter.Model.FranshiseListIndex != i)
                {
                    presenters.Remove(presenter);
                    presenters.Insert(presenter.Model.FranshiseListIndex, presenter);
                    i = 0;
                }
            }

            foreach (FilmPresenter presenter in presenters)
            {
                AddViewPresenter(presenter);
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

        //private static Category category = new Category();
        public String ID { get => Model.ID.ToString(); set { } }
        public String Name { get => Model.Name; set { } }
    }
}
