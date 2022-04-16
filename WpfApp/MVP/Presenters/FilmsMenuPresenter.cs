using CustomButtons;
using FilmsUCWpf.Presenter;
using FilmsUCWpf.PresenterInterfaces;
using FilmsUCWpf.View;
using InfoMenusWpf.MoreInfo;
using InfoMenusWpf.UpdateInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TablesLibrary.Interpreter;
using TL_Objects;
using TL_Tables;
using WpfApp.MVP.Models;
using WpfApp.MVP.ViewsInterface;

namespace WpfApp.MVP.Presenters
{
	public class FilmsMenuPresenter : IMenu<Film>
	{
		private FilmsMenuModel model;
		private IBaseMenuView view;
		private MainWindowModel mainModel;
		public FilmsMenuPresenter(FilmsMenuModel model, IBaseMenuView view, MainWindowModel windowModel)
		{
			this.model = model;
			this.view = view;
			this.mainModel = windowModel;
			this.model.MoreInfoFormVisualizer = new MoreInfoFormVisualizer(view.InfoCanvas);
			this.model.UpdateFormVisualizer = new UpdateFormVisualizer(view.InfoCanvas);
			this.model.MoreInfoFormVisualizer.UpdateVisualizer = this.model.UpdateFormVisualizer;
			this.model.UpdateFormVisualizer.MoreVisualizer = this.model.MoreInfoFormVisualizer;
			mainModel.TableCollection.TableLoad += TableCollection_TableLoad;
		}

		private void TableCollection_TableLoad(object sender, EventArgs e)
		{
			LoadCategories();
			LoadGenres();
		}

		public MoreInfoFormVisualizer MoreInfoFormVisualizer => model.MoreInfoFormVisualizer;
		public UpdateFormVisualizer UpdateFormVisualizer => model.UpdateFormVisualizer;
		public BasePresenter<Film> SelectedElement { get => model.SelectedElement; set => model.SelectedElement = (FilmPresenter)value; }
		private TableCollection TabColl => mainModel.TableCollection;

		public void LoadGenres()
		{
			view.GenresControls.Clear();
			foreach (Genre genre in mainModel.Tables.GenresTable)
			{
				view.GenresControls.Add(new GenrePressButtonControl(genre));
			}
		}

		private void ClearControls()
		{
			view.MenuControls.Clear();
			model.BasePresenters.Clear();
		}

		public void SaveTables()
		{
			mainModel.TableCollection.SaveTables();
		}

		public void LoadFilmTable()
		{
			ClearControls();
			model.ControlsCondition = FilmsMenuModel.MenuCondition.Film;
			model.SelectedElement = null;

			foreach (Film film in mainModel.Tables.FilmsTable)
			{
				model.BasePresenters.Add(new FilmPresenter(film, new FilmControl(), this, TabColl));
			}
			foreach (IBasePresenter presenter in model.BasePresenters)
			{
				presenter.AddViewToCollection(view.MenuControls);
			}
			UnLockGenreButtons();
		}

		public void LoadSerieTable()
		{
			ClearControls();
			model.ControlsCondition = FilmsMenuModel.MenuCondition.Serie;
			model.SelectedElement = null;

			foreach (Film film in mainModel.Tables.FilmsTable)
			{
				if (film.Genre.IsSerialGenre)
				{
					model.BasePresenters.Add(new FilmPresenter(film, new FilmSerieControl(), this, TabColl));
				}
			}
			foreach (IBasePresenter presenter in model.BasePresenters)
			{
				presenter.AddViewToCollection(view.MenuControls);
			}

			LockNotSerialGenreButtons();
		}

		public void LoadCategories()
		{
			ClearControls();
			model.ControlsCondition = FilmsMenuModel.MenuCondition.Category;
			model.SelectedElement = null;

			foreach (Category category in mainModel.Tables.CategoriesTable)
			{
				model.BasePresenters.Add(new FilmCategoryPresenter(category, new FilmCategoryControl(), this, TabColl));
			}

			foreach (Film film in mainModel.Tables.FilmsTable)
			{
				if (film.FranshiseId == 0)
				{
					model.BasePresenters.Add(new FilmPresenter(film, new FilmSimpleControl(), this, TabColl));
				}
			}

			foreach (IBasePresenter presenter in model.BasePresenters)
			{
				presenter.AddViewToCollection(view.MenuControls);
			}

			UnLockGenreButtons();
		}

		public void LoadPriorityTable()
		{
			ClearControls();
			model.ControlsCondition = FilmsMenuModel.MenuCondition.PriorityFilm;
			model.SelectedElement = null;

			PriorityFilmsTable priorityFilms = mainModel.Tables.PriorityFilmsTable;
			priorityFilms.RemoveWatchedFilms();

			foreach (PriorityFilm priorityFilm in priorityFilms)
			{
				model.BasePresenters.Add(new FilmPresenter(priorityFilm.Film, new FilmSimpleControl(), this, TabColl));
			}

			foreach (IBasePresenter presenter in model.BasePresenters)
			{
				presenter.AddViewToCollection(view.MenuControls);
			}

			UnLockGenreButtons();
		}

		private Genre[] GetSelectedGenres()
		{
			List<Genre> genres = new List<Genre>();
			foreach (GenrePressButtonControl genreButton in view.GenresControls)
			{
				if (genreButton.PressButton.Included)
				{
					genres.Add((Genre)genreButton.Genre);
				}
			}
			return genres.ToArray();
		}

		public void SearchByName(String name)
		{
			foreach (IBasePresenter presenter in model.BasePresenters)
			{
				presenter.SetVisualDefault();
			}

			if (name != "")
			{
				name = name.ToLowerInvariant();
				foreach (IBasePresenter presenter in model.BasePresenters)
				{
					presenter.SetFindedElement(name);
				}
			}
		}

		private void AddPresenter(IBasePresenter basePresenter)
		{
			model.BasePresenters.Add(basePresenter);
			AddPresenterToView(basePresenter);
		}

		private void AddPresenterToView(IBasePresenter basePresenter)
		{
			basePresenter.AddViewToCollection(view.MenuControls);
		}

		public void Filter(bool watched, bool unwatched)
		{
			view.MenuControls.Clear();
			Genre[] genres = GetSelectedGenres();


			if (genres.Length == mainModel.Tables.GenresTable.Count && watched && unwatched)
			{
				foreach (IBasePresenter presenter in model.BasePresenters)
				{
					AddPresenterToView(presenter);
				}
			}
			else
			{
				if (watched && unwatched)
				{
					foreach(IHasGenre hasGenre in model.BasePresenters)
					{
						if (hasGenre.HasSelectedGenre(genres))
							AddPresenterToView((IBasePresenter)hasGenre);
					}
				}
				else
				{
					foreach (IHasGenre hasGenre in model.BasePresenters)
					{
						IBasePresenter presenter = (IBasePresenter)hasGenre;
						if (hasGenre.HasSelectedGenre(genres) && presenter.HasCheckedProperty(watched))
							AddPresenterToView((IBasePresenter)hasGenre);
					}
				}
			}
		}

		private void LockNotSerialGenreButtons()
		{
			foreach (GenrePressButtonControl button in view.GenresControls)
			{
				Genre genre = (Genre)button.Genre;
				if (!genre.IsSerialGenre)
				{
					button.PressButton.Included = false;
					button.PressButton.ClickLocked = true;
				}
			}
		}

		private void UnLockGenreButtons()
		{
			foreach (GenrePressButtonControl button in view.GenresControls)
			{
				button.PressButton.ClickLocked = false;
				button.PressButton.Included = true;
			}
		}

		public void AddSelectedToPriority()
		{
			if (model.SelectedElement != null)
			{
				PriorityFilm priorityFilm = new PriorityFilm();
				priorityFilm.Film = model.SelectedElement.Model;
				mainModel.Tables.PriorityFilmsTable.AddElement(priorityFilm);
			}
		}

		public void AddCategory()
		{
			if (model.ControlsCondition == FilmsMenuModel.MenuCondition.Category)
			{
				CategoriesTable categories = mainModel.Tables.CategoriesTable;
				Category category = new Category();
				categories.AddElement(category);
				FilmCategoryPresenter presenter = new FilmCategoryPresenter(category, new FilmCategoryControl(), this, TabColl);
				model.BasePresenters.Insert(categories.Count - 1, presenter);
				view.MenuControls.Insert(categories.Count - 1, presenter.View);
			}
		}

		public void AddFilm()
		{
			Film film = new Film();
			film.Genre = mainModel.Tables.GenresTable[0];
			mainModel.Tables.FilmsTable.AddElement(film);
			FilmPresenter presenter;
			switch (model.ControlsCondition)
			{
				case FilmsMenuModel.MenuCondition.Category:
					presenter = new FilmPresenter(film, new FilmSimpleControl(), this, TabColl);
					AddPresenter(presenter);
					break;

				case FilmsMenuModel.MenuCondition.Film:
					presenter = new FilmPresenter(film, new FilmControl(), this, TabColl);
					AddPresenter(presenter);
					break;

				case FilmsMenuModel.MenuCondition.Serie:
					film.Genre = mainModel.Tables.GenresTable.GetFirstSeriealGenre();
					Serie serie = new Serie();
					serie.Film = film;
					mainModel.Tables.SeriesTable.AddElement(serie);
					presenter = new FilmPresenter(film, new FilmSerieControl(), this, TabColl);
					AddPresenter(presenter);
					break;

				default:
					break;
			}
		}

		public void RemoveSelectedFilm()
		{
			Film film = model.SelectedElement.Model;
			

			switch (model.ControlsCondition)
			{
				case FilmsMenuModel.MenuCondition.PriorityFilm:
					PriorityFilmsTable priFilmTab = mainModel.Tables.PriorityFilmsTable;
					priFilmTab.Remove(priFilmTab.GetElementByFilm(film));
					RemoveSelected();
					break;
				default:
					if (film.FranshiseId == 0)
						mainModel.Tables.FilmsTable.Remove(film);
					model.BasePresenters.Remove(model.SelectedElement);
					break;
			}
		}

		public bool AddSelected()
		{
			if (SelectedElement != null)
			{
				int i = 0;
				Type type = typeof(BasePresenter<Film>);
				foreach (IBasePresenter item in model.BasePresenters)
				{
                    if (item.GetType().IsSubclassOf(type))
                    {
						BasePresenter<Film> basePresenter = (BasePresenter<Film>)item;
						Film film = basePresenter.Model;
						if (film.ID > model.SelectedElement.Model.ID) break;
					}
					++i;
				}
				FilmPresenter presenter = new FilmPresenter(model.SelectedElement.Model, new FilmSimpleControl(), this, TabColl);
				model.BasePresenters.Insert(i, presenter);
				view.MenuControls.Insert(i, presenter.View);
				model.SelectedElement = null;
				return true;
			}
			else return false;
		}

		public bool RemoveSelected()
		{
			if (SelectedElement != null)
			{
				view.MenuControls.Remove(SelectedElement.View);
				bool exp = model.BasePresenters.Remove(SelectedElement);
				SelectedElement = null;
				return exp;
			}
			else return false;
		}
	}
}
