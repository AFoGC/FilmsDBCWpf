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
		private IMenuPresenter<Film> menu;
		private TableCollection collection;
		public FilmUpdatePresenter(Film model, IFilmUpdateView view, IMenuPresenter<Film> menu, TableCollection collection)
		{
			this.model = model;
			this.view = view;
			this.menu = menu;
			this.collection = collection;
            foreach (Genre genre in collection.GetTable<Genre>())
            {
				view.Genres.Add(genre);
            }

            model.FormatedMark.PropertyChanged += FormatedMark_PropertyChanged;
            
			RefreshElement();
		}

        private void FormatedMark_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(model.FormatedMark.MarkSystem))
            {
				refreshComboBox();
            }
        }

        private static Film defFilm = new Film();
		public void RefreshElement()
		{
			refreshComboBox();

			view.ID = model.ID.ToString();
			view.Name = model.Name;
			view.Genre = model.Genre;
			view.RealiseYear = Film.FormatToString(model.RealiseYear, defFilm.RealiseYear);
			view.Wathced = model.Watched;
			view.CountOfViews = Film.FormatToString(model.CountOfViews, defFilm.CountOfViews);
			view.DateOfWatch = model.DateOfWatch;
			view.Comment = model.Comment;
		}

		private void refreshComboBox()
        {
			view.Marks.Clear();
			foreach (string mark in model.FormatedMark.GetComboItems())
			{
				view.Marks.Add(mark);
			}
			view.Mark = model.FormatedMark.ToString();
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

			model.FormatedMark.SetMarkFromString(view.Mark);

			model.DateOfWatch = view.DateOfWatch;
		}

		public void OpenSources()
        {
			menu.OpenSourcesInfo(model.Sources);
        }
    }
}
