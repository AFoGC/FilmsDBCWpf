using FilmsUCWpf.Presenter.Interfaces;
using FilmsUCWpf.View.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TablesLibrary.Interpreter;
using TL_Objects;

namespace FilmsUCWpf.Presenter
{
    public class FilmSerieUpdatePresenter
	{
		private Film model;
		private IMenuPresenter<Film> menu;

		public FilmSerieUpdatePresenter(Film model, IMenuPresenter<Film> menu)
		{
			this.model = model;
			this.menu = menu;
		}

		public void OpenSources()
		{
			menu.OpenSourcesInfo(model.Sources);
		}
	}
}
