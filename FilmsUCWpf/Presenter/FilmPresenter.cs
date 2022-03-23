﻿using FilmsUCWpf.PresenterInterfaces;
using FilmsUCWpf.View;
using FilmsUCWpf.ViewInterfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using TL_Objects;
using TL_Objects.Interfaces;

namespace FilmsUCWpf.Presenter
{
    public class FilmPresenter : BasePresenter<Film>, IHasGenre
    {

        protected IMenu<Film> menu;

        public FilmPresenter(Film film, IBaseView view, IMenu<Film> menu) : base(film, view)
        {
            this.menu = menu;
            film.Genre.PropertyChanged += Genre_PropertyChanged;
        }

        private void Genre_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            OnPropertyChanged("Genre");
        }

        public override bool HasCheckedProperty(bool isWatched)
        {
            return isWatched == Model.Watched;
        }

        public override bool SetFindedElement(string search)
        {
            if (this.Model.Name.ToLowerInvariant().Contains(search))
            {
                View.SetVisualFinded();
                return true;
            }

            return false;
        }

        public override void SetSelectedElement()
        {
            if (menu.ControlInBuffer != null)
            {
                menu.ControlInBuffer = this;
                View.SetVisualSelected();
            }
        }

        public void CopyUrl()
        {
            Helper.CopyFirstSource(Model.Sources);
        }

        public override void SetVisualDefault()
        {
            View.SetVisualDefault();
        }

        public void OpenUpdateMenu()
        {
            //menu.UpdateFormVisualizer.OpenUpdateControl(new FilmUpdateControl());
        }

        public void OpenInfoMenu()
        {
            IBaseView view;
            if (Model.Serie == null)
                view = new FilmControl();
            else
                view = new FilmSerieControl();

            FilmPresenter presenter = new FilmPresenter(Model, view, menu);

            menu.MoreInfoFormVisualizer.OpenMoreInfoForm((Control)presenter.View);
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

        private static Film defFilm = new Film();
        private static Serie defSerie = new Serie();
        public String ID { get => Model.ID.ToString(); set { } }
        public String Name { get => Model.Name; set { } }
        public String Genre { get => Model.Genre.ToString(); set { } }
        public String RealiseYear { get => Film.FormatToString(Model.RealiseYear, defFilm.RealiseYear); set { } }
        public Boolean Watched { get => Model.Watched; set { } }
        public String DateOfWatch { get => Film.FormatToString(Model.DateOfWatch, defFilm.DateOfWatch); set { } }
        public String Mark { get => Helper.MarkToText(Film.FormatToString(Model.Mark, defFilm.Mark)); set { } }
        public String CountOfViews { get => Film.FormatToString(Model.CountOfViews, defFilm.CountOfViews); set { } }
        public String Comment { get => Model.Comment; set { } }
        public String Sources { get => Helper.SourcesStateString(Model.Sources); set { } }

        public String StartWatchDate { get => Serie.FormatToString(Model.Serie.StartWatchDate, defSerie.StartWatchDate); set { } }
        public String CountOfWatchedSeries { get => Serie.FormatToString(Model.Serie.CountOfWatchedSeries, defSerie.CountOfWatchedSeries); set { } }
        public String TotalSeries { get => Serie.FormatToString(Model.Serie.TotalSeries, defSerie.TotalSeries); set { } }
    }
}
