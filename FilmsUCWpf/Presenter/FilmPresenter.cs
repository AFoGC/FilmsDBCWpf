using FilmsUCWpf.PresenterInterfaces;
using FilmsUCWpf.View;
using FilmsUCWpf.ViewInterfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using TablesLibrary.Interpreter;
using TL_Objects;
using TL_Objects.Interfaces;
using TL_Tables;

namespace FilmsUCWpf.Presenter
{
	public class FilmPresenter : BasePresenter<Film>, IHasGenre
	{
		protected IMenu<Film> menu;

		public FilmPresenter(Film film, IView view, IMenu<Film> menu, TableCollection collection) : base(film, view, collection)
		{
			this.menu = menu;
		}

		public override bool HasCheckedProperty(bool isWatched)
		{
			return isWatched == Model.Watched;
		}

		public override bool SetFindedElement(string search)
		{
			if (this.Model.Name.ToLowerInvariant().Contains(search))
			{
				View.SetVisualFinded();
				return true;
			}

			return false;
		}

		public override void SetSelectedElement()
		{
			menu.SelectedElement = this;
			View.SetVisualSelected();
		}

		public void DeleteThis()
        {
			FilmsTable filmsTable = (FilmsTable)TableCollection.GetTable<Film>();
			CategoriesTable categoriesTable = (CategoriesTable)TableCollection.GetTable<Category>();

			if (Model.FranshiseId != 0)
            {
				Category category = categoriesTable.GetCategoryByFilm(Model);
				category.RemoveFilmFromCategory(Model);
			}

			filmsTable.Remove(Model);
		}

		public void AddToPriority()
        {
			PriorityFilmsTable priorityFilms = (PriorityFilmsTable)TableCollection.GetTable<PriorityFilm>();
            if (!priorityFilms.ContainFilm(Model))
            {
				PriorityFilm priority = new PriorityFilm();
				priority.Film = Model;
				priorityFilms.AddElement(priority);
            }
        }

		public void CopyUrl()
		{
			Helper.CopyFirstSource(Model.Sources);
		}

		public override void SetVisualDefault()
		{
			View.SetVisualDefault();
		}

		public void OpenUpdateMenu()
		{
			SeriesTable table = (SeriesTable)TableCollection.GetTable<Serie>();
			if (Model.Genre.IsSerialGenre)
			{
				menu.UpdateFormVisualizer.OpenUpdateControl(new FilmSerieUpdateControl(Model, menu, TableCollection));
			}
			else
			{
				menu.UpdateFormVisualizer.OpenUpdateControl(new FilmUpdateControl(Model, menu, TableCollection));
			}
		}

		public void OpenInfoMenu()
		{
			IView view;
			if (Model.Serie == null)
				view = new FilmControl();
			else
				view = new FilmSerieControl();

			FilmPresenter presenter = new FilmPresenter(Model, view, menu, TableCollection);

			menu.MoreInfoFormVisualizer.OpenMoreInfoForm((Control)presenter.View);
		}

		public bool HasSelectedGenre(IGenre[] selectedGenres)
		{
			foreach (IGenre genre in selectedGenres)
			{
				if (genre == Model.Genre)
				{
					return true;
				}
			}
			return false;
		}

		public void UpFranshiseListID()
        {
			CategoriesTable categories = (CategoriesTable)TableCollection.GetTable<Category>();
			Category category = categories.GetCategoryByFilm(Model);
			category.ChangeFilmPositionBy(Model, -1);
        }

		public void DownFranshiseListID()
		{
			CategoriesTable categories = (CategoriesTable)TableCollection.GetTable<Category>();
			Category category = categories.GetCategoryByFilm(Model);
			category.ChangeFilmPositionBy(Model, 1);
		}
	}
}
