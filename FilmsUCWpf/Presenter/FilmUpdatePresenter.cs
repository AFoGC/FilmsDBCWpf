using FilmsUCWpf.Presenter.Interfaces;
using FilmsUCWpf.View.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TablesLibrary.Interpreter;
using TL_Objects;
using TL_Tables;

namespace FilmsUCWpf.Presenter
{
    public class FilmUpdatePresenter : IUpdatePresenter
	{
		private Film model;
		private IFilmUpdateView view;
		private IBaseMenu menu;
		private TableCollection collection;
		public FilmUpdatePresenter(Film model, IFilmUpdateView view, IMenu<Film> menu, TableCollection collection)
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
		}
		public void UpdateElement()
        {
			SeriesTable table = (SeriesTable)collection.GetTable<Serie>();
			if (!model.Genre.IsSerialGenre && view.Genre.IsSerialGenre)
			{
				table.FindAndConnectSerie(model);
			}

			model.Genre = view.Genre;
			model.Watched = view.Wathced;
			model.Name = view.Name;
			model.Comment = view.Comment;
			
			model.RealiseYear = Helper.TextToInt32(view.RealiseYear);
			model.CountOfViews = Helper.TextToInt32(view.CountOfViews);

			model.Mark = Helper.TextToMark(view.Mark);

			model.DateOfWatch = view.DateOfWatch;
		}

		public void OpenSources()
        {
			Helper.OpenSources(menu, model.Sources);
        }
    }
}
