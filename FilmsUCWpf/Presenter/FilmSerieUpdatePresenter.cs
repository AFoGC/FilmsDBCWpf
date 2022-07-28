using FilmsUCWpf.Presenter.Interfaces;
using FilmsUCWpf.View.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TablesLibrary.Interpreter;
using TL_Objects;

namespace FilmsUCWpf.Presenter
{
    public class FilmSerieUpdatePresenter : IUpdatePresenter
	{
		private Film model;
		private IFilmSerieUpdateView view;
		private IMenuPresenter<Film> menu;
		private TableCollection collection;

		public FilmSerieUpdatePresenter(Film model, IFilmSerieUpdateView view, IMenuPresenter<Film> menu, TableCollection collection)
		{
			this.model = model;
			this.view = view;
			this.menu = menu;
			this.collection = collection;
			foreach (Genre genre in collection.GetTable<Genre>())
			{
				view.Genres.Add(genre);
			}
			foreach (string mark in Helper.GetAllMarks())
			{
				view.Marks.Add(mark);
			}
			RefreshElement();
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
			model.Watched = view.Wathced;
			model.Name = view.Name;
			model.Genre = view.Genre;
			model.Comment = view.Comment;
			
			model.CountOfViews = Helper.TextToInt32(view.CountOfViews);
			model.RealiseYear = Helper.TextToInt32(view.RealiseYear);
			model.Serie.CountOfWatchedSeries = Helper.TextToInt32(view.CountOfWatchedSeries);
			model.Serie.TotalSeries = Helper.TextToInt32(view.TotalSeries);

			model.Mark = Helper.TextToMark(view.Mark);

			model.DateOfWatch = view.DateOfWatch;
			model.Serie.StartWatchDate = view.StartWatchDate;

			if(!model.Genre.IsSerialGenre)
				model.Serie.Film = null;
		}

		public void OpenSources()
		{
			menu.OpenSourcesInfo(model.Sources);
		}
	}
}
