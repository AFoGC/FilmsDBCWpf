using FilmsUCWpf.PresenterInterfaces;
using FilmsUCWpf.ViewInterfaces;
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
			RefreshElement();
		}

		public void UpdateElement()
		{
			model.Name = view.Name;
			model.HideName = view.HideName;
		}

		public void AddSelected()
        {
            if (menu.SelectedElement != null)
            {
				Film film = menu.SelectedElement.Model;
                if (film.FranshiseId == 0)
                {
					film.FranshiseId = model.ID;
					film.FranshiseListIndex = (sbyte)(model.Films.Count);
					model.Films.Add(film);
					menu.RemoveSelected();
                }
            }
        }

		public void RemoveSelected()
        {
            if (menu.SelectedElement != null)
            {
				Film film = menu.SelectedElement.Model;
				if (model.RemoveFilmFromCategory(film))
					menu.AddSelected();
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

        public void RefreshElement()
        {
			view.ID = model.ID.ToString();
			view.Name = model.Name;
			view.HideName = model.HideName;
        }
    }
}
