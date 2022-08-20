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
    public class FilmCategoryUpdatePresenter
	{
		private Category model;
		private IMenuModel<Film> menu;
		private TableCollection tableCollection;

		public FilmCategoryUpdatePresenter(Category model, IMenuModel<Film> menu, TableCollection tableCollection)
		{
			this.model = model;
			this.menu = menu;
			this.tableCollection = tableCollection;
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
	}
}
