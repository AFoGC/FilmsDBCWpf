using FilmsUCWpf.Presenter.Interfaces;
using FilmsUCWpf.View.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TablesLibrary.Interpreter;
using TablesLibrary.Interpreter.Table;
using TL_Objects;

namespace FilmsUCWpf.Presenter
{
    public class FilmCategoryUpdatePresenter : IUpdatePresenter
	{
		private Category model;
		private IFilmCategoryUpdateView view;
		private IMenu<Film> menu;
		private TableCollection tableCollection;

		public FilmCategoryUpdatePresenter(Category model, IFilmCategoryUpdateView view, IMenu<Film> menu, TableCollection tableCollection)
		{
			this.model = model;
			this.view = view;
			this.menu = menu;
			this.tableCollection = tableCollection;
			foreach (string mark in Helper.GetAllMarks())
			{
				view.Marks.Add(mark);
			}
			RefreshElement();
		}

		public void AddSelected()
        {
            if (menu.SelectedElement != null)
            {
				Film film = menu.SelectedElement.Model;
                if (film.FranshiseId == 0)
                {
					film.FranshiseId = model.ID;
					model.Films.Add(film);
					menu.RemoveElement(menu.SelectedElement.Model);
					menu.SelectedElement = null;
				}
            }
        }

		public void RemoveSelected()
        {
            if (menu.SelectedElement != null)
            {
				Film film = menu.SelectedElement.Model;
				if (model.RemoveFilmFromCategory(film))
				{
					menu.AddElement(menu.SelectedElement.Model);
					menu.SelectedElement = null;
				}
			}
        }

		public void DeleteThisCategory()
        {
			Table<Category> cateories = tableCollection.GetTable<Category>();
            if (model.Films.Count == 0)
            {
				cateories.Remove(model);
			}
        }

		private static Category defCat = new Category();
		public void RefreshElement()
        {
			view.ID = model.ID.ToString();
			view.Name = model.Name;
			view.HideName = model.HideName;
			view.Mark = Helper.MarkToText(Film.FormatToString(model.Mark, defCat.Mark));
		}

		public void UpdateElement()
		{
			model.Name = view.Name;
			model.HideName = view.HideName;
			model.Mark = Helper.TextToMark(view.Mark);
		}
	}
}
