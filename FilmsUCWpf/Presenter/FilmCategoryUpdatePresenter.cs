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
	public class FilmCategoryUpdatePresenter : IUpdatePresenter
	{
		private Category model;
		private IFilmCategoryUpdateView view;
		private IMenu<Film> menu;

		public FilmCategoryUpdatePresenter(Category model, IFilmCategoryUpdateView view, IMenu<Film> menu)
		{
			this.model = model;
			this.view = view;
			this.menu = menu;
		}

		public void UpdateElement()
		{
			model.Name = view.Name;
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

        public void RefreshElement()
        {
			view.ID = model.ID.ToString();
			view.Name = model.Name;
        }
    }
}
