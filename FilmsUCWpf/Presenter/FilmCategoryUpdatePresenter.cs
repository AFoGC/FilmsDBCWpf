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
		private IMenuModel<Film> menu;
		private TableCollection tableCollection;

		public FilmCategoryUpdatePresenter(Category model, IFilmCategoryUpdateView view, IMenuModel<Film> menu, TableCollection tableCollection)
		{
			this.model = model;
			this.view = view;
			this.menu = menu;
			this.tableCollection = tableCollection;

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
			refreshComboBox();

			view.ID = model.ID.ToString();
			view.Name = model.Name;
			view.HideName = model.HideName;
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
			model.Name = view.Name;
			model.HideName = view.HideName;
			model.FormatedMark.SetMarkFromString(view.Mark);
		}
	}
}
