using FilmsUCWpf.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TL_Objects;

namespace FilmsUCWpf.Presenters
{
	public class FilmPresenter : BasePresenter<Film>
	{
		protected IMenu<Film> menu;
		public FilmPresenter(Film film, IView<Film> view, IMenu<Film> menu) : this(film, view)
		{
			this.menu = menu;
		}

		private FilmPresenter(Film film, IView<Film> view) : base(film, view)
		{

		}

		public void SetSelectedElement()
		{
			if (menu.ControlInBuffer != null)
			{
				menu.ControlInBuffer = this;
				View.SetVisualSelected();
			}
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

		public void OpenUpdateMenu()
        {
			//menu.UpdateFormVisualizer.OpenUpdateControl(new FilmUpdateControl());
        }
	}
}
