using MobileApp.Models;
using MobileApp.Views;
using System;
using System.Collections.Generic;
using System.Text;
using TL_Objects;

namespace MobileApp.Presenters
{
	public class FilmsMenuPresenter
	{
		private readonly IFilmsMenuView view;
		private readonly FilmsMenuModel model;
		private readonly MainModel mainModel;

		public FilmsMenuPresenter(IFilmsMenuView view, FilmsMenuModel model, MainModel mainModel)
		{
			this.view = view;
			this.model = model;
			this.mainModel = mainModel;
			LoadFilmTable();
			mainModel.TableCollection.TableLoad += TableCollection_TableLoad;
		}

		private void TableCollection_TableLoad(object sender, EventArgs e)
		{
			
		}

		public void LoadFilmTable()
        {
			view.MenuControls.Clear();
            foreach (Film film in mainModel.Tables.FilmsTable)
            {
				view.MenuControls.Add(new FilmSimpleView(film));
			}
        }

		public void AddFilm()
        {
			Film film = new Film();
			film.Genre = mainModel.Tables.GenresTable[0];
			mainModel.Tables.FilmsTable.AddElement(film);
			view.MenuControls.Add(new FilmSimpleView(film));
			mainModel.TableCollection.SaveTables();
		}
	}
}
