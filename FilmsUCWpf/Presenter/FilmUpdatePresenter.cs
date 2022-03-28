using FilmsUCWpf.PresenterInterfaces;
using FilmsUCWpf.ViewInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TL_Objects;
using TL_Tables;

namespace FilmsUCWpf.Presenter
{
	public class FilmUpdatePresenter : IUpdatePresenter
	{
		private Film model;
		private IFilmUpdateView view;
		private IBaseMenu menu;
		private SeriesTable table;
		public FilmUpdatePresenter(Film model, IFilmUpdateView view, IMenu<Film> menu, SeriesTable table)
		{
			this.model = model;
			this.view = view;
			this.menu = menu;
			this.table = table;
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
			model.Name = view.Name;
			if (!model.Genre.IsSerialGenre && view.Genre.IsSerialGenre)
			{
				table.FindAndConnectSerie(model);
			}
			model.Genre = view.Genre;
			model.RealiseYear = Helper.TextToInt32(view.RealiseYear);
			model.Watched = view.Wathced;
			model.Mark = Helper.TextToMark(view.Mark);
			model.CountOfViews = Helper.TextToInt32(view.CountOfViews);
			model.DateOfWatch = view.DateOfWatch;
			model.Comment = view.Comment;
		}

		public void OpenSources()
        {
			Helper.OpenSources(menu, model.Sources);
        }
    }
}
