using FilmsUCWpf.PresenterInterfaces;
using FilmsUCWpf.ViewInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TL_Objects;

namespace FilmsUCWpf.Presenter
{
	public class FilmSerieUpdatePresenter : IUpdatePresenter
	{
		private Film model;
		private IFilmSerieUpdateView view;
		private IBaseMenu menu;

		public FilmSerieUpdatePresenter(Film model, IFilmSerieUpdateView view, IMenu<Film> menu)
		{
			this.model = model;
			this.view = view;
			this.menu = menu;
		}

		private static Film defFilm = new Film();
		private static Serie defSerie = new Serie();
		public void RefreshElement()
		{
			view.ID = model.ID.ToString();
			view.Name = model.Name;
			view.Genre = model.Genre;
			view.RealiseYear = Film.FormatToString(model.RealiseYear, defFilm.RealiseYear);
			view.Wathced = model.Watched;
			view.Mark = Helper.MarkToText(Film.FormatToString(model.Mark, defFilm.Mark));
			view.CountOfViews = Film.FormatToString(model.CountOfViews, defFilm.CountOfViews);
			view.DateOfWatch = model.DateOfWatch;
			view.Comment = model.Comment;

			view.StartWatchDate = model.Serie.StartWatchDate;
			view.CountOfWatchedSeries = Serie.FormatToString(model.Serie.CountOfWatchedSeries, defSerie.CountOfWatchedSeries);
			view.TotalSeries = Serie.FormatToString(model.Serie.TotalSeries, defSerie.TotalSeries);
		}

		public void UpdateElement()
		{
			model.Name = view.Name;
			model.Genre = view.Genre;
			model.RealiseYear = Helper.TextToInt32(view.RealiseYear);
			model.Watched = view.Wathced;
			model.Mark = Helper.TextToMark(view.Mark);
			model.CountOfViews = Helper.TextToInt32(view.CountOfViews);
			model.DateOfWatch = view.DateOfWatch;
			model.Comment = view.Comment;

			model.Serie.StartWatchDate = view.StartWatchDate;
			model.Serie.CountOfWatchedSeries = Helper.TextToInt32(view.CountOfWatchedSeries);
			model.Serie.TotalSeries = Helper.TextToInt32(view.TotalSeries);
		}

		public void OpenSources()
		{
			Helper.OpenSources(menu, model.Sources);
		}
	}
}
