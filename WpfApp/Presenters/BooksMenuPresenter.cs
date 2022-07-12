using CustomButtons;
using FilmsUCWpf.Presenter;
using FilmsUCWpf.Presenter.Interfaces;
using FilmsUCWpf.View;
using FilmsUCWpf.View.Interfaces;
using InfoMenusWpf.MoreInfo;
using InfoMenusWpf.UpdateInfo;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using TablesLibrary.Interpreter;
using TL_Objects;
using TL_Tables;
using WpfApp.Models;
using WpfApp.Views.Interfaces;

namespace WpfApp.Presenters
{
    public class BooksMenuPresenter
	{
		private BooksMenuModel model;
		private IBaseMenuView view;

		public BooksMenuPresenter(BooksMenuModel model, IBaseMenuView view)
		{
			this.view = view;
			this.model = model;
			this.model.MoreInfoFormVisualizer = new MoreInfoFormVisualizer(view.InfoCanvas);
			this.model.UpdateFormVisualizer = new UpdateFormVisualizer(view.InfoCanvas);
			this.model.MoreInfoFormVisualizer.UpdateVisualizer = this.model.UpdateFormVisualizer;
			this.model.UpdateFormVisualizer.MoreVisualizer = this.model.MoreInfoFormVisualizer;

			model.BookPresenters.CollectionChanged += presentersChanged;
			model.CategoryPresenters.CollectionChanged += presentersChanged;
			model.PriorityPresenters.CollectionChanged += presentersChanged;
			model.GenreButtons.CollectionChanged += GenreButtons_CollectionChanged;

			model.TableLoad();
		}

		private void GenreButtons_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			switch (e.Action)
			{
				case NotifyCollectionChangedAction.Add:
					view.GenresControls.Insert(e.NewStartingIndex, e.NewItems[0]);
					break;
				case NotifyCollectionChangedAction.Remove:
					view.GenresControls.RemoveAt(e.OldStartingIndex);
					break;
				case NotifyCollectionChangedAction.Reset:
					view.GenresControls.Clear();
					break;
				default:
					break;
			}
		}

		private void presentersChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			if (sender == model.GetCurrentPresenters())
			{
				switch (e.Action)
				{
					case NotifyCollectionChangedAction.Add:
						view.MenuControls.Insert(e.NewStartingIndex, ((IBasePresenter)e.NewItems[0]).View);
						break;
					case NotifyCollectionChangedAction.Remove:
						view.MenuControls.RemoveAt(e.OldStartingIndex);
						if (e.OldItems[0] == model.SelectedElement)
							model.SelectedElement = null;
						break;
					case NotifyCollectionChangedAction.Reset:
						view.MenuControls.Clear();
						break;
					default:
						break;
				}
			}
		}

		public void SaveTables() => model.TableCollection.SaveTables();

		public void LoadCategories()
		{
			model.SelectedElement = null;

			if (model.ControlsCondition != BooksMenuModel.MenuCondition.Category)
			{
				model.ControlsCondition = BooksMenuModel.MenuCondition.Category;
				view.MenuControls.Clear();
				foreach (IBasePresenter presenter in model.CategoryPresenters)
				{
					presenter.AddViewToCollection(view.MenuControls);
				}
			}
		}

		public void LoadBooks()
		{
			model.SelectedElement = null;
			if (model.ControlsCondition != BooksMenuModel.MenuCondition.Book)
			{
				model.ControlsCondition = BooksMenuModel.MenuCondition.Book;
				view.MenuControls.Clear();
				foreach (IBasePresenter presenter in model.BookPresenters)
				{
					presenter.AddViewToCollection(view.MenuControls);
				}
			}
		}

		public void LoadPriorityTable()
		{
			model.SelectedElement = null;
			if (model.ControlsCondition != BooksMenuModel.MenuCondition.PriorityBook)
			{
				model.ControlsCondition = BooksMenuModel.MenuCondition.PriorityBook;
				view.MenuControls.Clear();
				foreach (IBasePresenter presenter in model.PriorityPresenters)
				{
					presenter.AddViewToCollection(view.MenuControls);
				}
			}
		}

		public BookGenre[] GetSelectedGenres()
		{
			List<BookGenre> genres = new List<BookGenre>();
			foreach (GenrePressButtonControl genreButton in view.GenresControls)
			{
				if (genreButton.PressButton.Included)
				{
					genres.Add((BookGenre)genreButton.Genre);
				}
			}
			return genres.ToArray();
		}

		public void SearchByName(string name)
		{
			foreach (IBasePresenter presenter in model.GetCurrentPresenters())
			{
				presenter.SetVisualDefault();
			}

			if (name != "")
			{
				name = name.ToLowerInvariant();
				foreach (IBasePresenter presenter in model.GetCurrentPresenters())
				{
					presenter.SetFindedElement(name);
				}
			}
		}

		private void addPresenterToView(IBasePresenter basePresenter)
		{
			basePresenter.AddViewToCollection(view.MenuControls);
		}

		private void addPresentersToView(IEnumerable presenters)
        {
            foreach (IBasePresenter presenter in presenters)
            {
				addPresenterToView(presenter);
            }
        }

		public void Filter(bool watched, bool unwatched)
		{
			view.MenuControls.Clear();
			BookGenre[] genres = GetSelectedGenres();


			if (genres.Length == model.Tables.GenresTable.Count && watched && unwatched)
			{
				foreach (IBasePresenter presenter in model.GetCurrentPresenters())
				{
					addPresenterToView(presenter);
				}
			}
			else
			{
				if (watched && unwatched)
				{
					foreach (IHasGenre hasGenre in model.GetCurrentPresenters())
					{
						if (hasGenre.HasSelectedGenre(genres))
							addPresenterToView((IBasePresenter)hasGenre);
					}
				}
				else
				{
					foreach (IHasGenre hasGenre in model.GetCurrentPresenters())
					{
						IBasePresenter presenter = (IBasePresenter)hasGenre;
						if (hasGenre.HasSelectedGenre(genres) && presenter.HasCheckedProperty(watched))
							addPresenterToView((IBasePresenter)hasGenre);
					}
				}
			}
		}

		public void AddCategory()
		{
			if (model.ControlsCondition == BooksMenuModel.MenuCondition.Category)
			{
				BookCategoriesTable categories = model.Tables.BookCategoriesTable;
				BookCategory category = new BookCategory();
				categories.AddElement(category);
			}
		}

		public void AddBook()
		{
			Book book = new Book();
			book.BookGenre = model.Tables.BookGenresTable[0];
			model.Tables.BooksTable.AddElement(book);
		}

		public void UpdateVisualizerIfOpen()
		{
			model.UpdateFormVisualizer.UpdateControl.Update();
		}

		private IEnumerable<BookCategoryPresenter> getCategoriesOnView()
        {
			List<BookCategoryPresenter> presenters = new List<BookCategoryPresenter>();
			Type categoryType = typeof(BookCategoryPresenter);

            foreach (IView view in view.MenuControls)
            {
				Type presenterType = view.Presenter.GetType();
				if (categoryType == presenterType || presenterType.IsSubclassOf(categoryType))
                {
					presenters.Add((BookCategoryPresenter)view.Presenter);
                }
            }

			return presenters;
        }

		private IEnumerable<BookPresenter> getBooksOnView()
		{
			List<BookPresenter> presenters = new List<BookPresenter>();
			Type categoryType = typeof(BookPresenter);

			foreach (IView view in view.MenuControls)
			{
				Type presenterType = view.Presenter.GetType();
				if (categoryType == presenterType || presenterType.IsSubclassOf(categoryType))
				{
					presenters.Add((BookPresenter)view.Presenter);
				}
			}

			return presenters;
		}

		public void SortByID()
        {
			IEnumerable<BookCategoryPresenter> categories = getCategoriesOnView();
			IEnumerable<BookPresenter> books = getBooksOnView();
			view.MenuControls.Clear();

			switch (model.ControlsCondition)
            {
				case BooksMenuModel.MenuCondition.Category:
					addPresentersToView(categories.OrderBy(a => a.Model.ID));
					addPresentersToView(books.OrderBy(a => a.Model.ID));
					break;
				default:
					addPresentersToView(books.OrderBy(a => a.Model.ID));
					break;
            }
        }

		public void SortByName()
        {
			IEnumerable<BookCategoryPresenter> categories = getCategoriesOnView();
			IEnumerable<BookPresenter> books = getBooksOnView();
			view.MenuControls.Clear();

			switch (model.ControlsCondition)
			{
				case BooksMenuModel.MenuCondition.Category:
					addPresentersToView(categories.OrderBy(a => a.Model.Name));
					addPresentersToView(books.OrderBy(a => a.Model.Name));
					break;
				default:
					addPresentersToView(books.OrderBy(a => a.Model.Name));
					break;
			}
		}

		public void SortByMark()
		{
			IEnumerable<BookCategoryPresenter> categories = getCategoriesOnView();
			IEnumerable<BookPresenter> books = getBooksOnView();
			view.MenuControls.Clear();

			switch (model.ControlsCondition)
			{
				case BooksMenuModel.MenuCondition.Category:
					addPresentersToView(categories.OrderBy(a => a.Model.Mark).Reverse());
					addPresentersToView(books.OrderBy(a => a.Model.Mark).Reverse());
					break;
				default:
					addPresentersToView(books.OrderBy(a => a.Model.Mark).Reverse());
					break;
			}
		}

		public void SortByAuthor()
		{
			IEnumerable<BookPresenter> books = getBooksOnView();
			view.MenuControls.Clear();
			addPresentersToView(books.OrderBy(a => a.Model.Author));
		}

		public void SortByGenre()
        {
			IEnumerable<BookPresenter> books = getBooksOnView();
			view.MenuControls.Clear();
			addPresentersToView(books.OrderBy(a => a.Model.BookGenre.Name));
		}

		public void SortByYear()
		{
			IEnumerable<BookPresenter> books = getBooksOnView();
			view.MenuControls.Clear();
			addPresentersToView(books.OrderBy(a => a.Model.PublicationYear).Reverse());
		}

		public void SortByReaded()
		{
			IEnumerable<BookPresenter> books = getBooksOnView();
			view.MenuControls.Clear();
			addPresentersToView(books.OrderBy(a => a.Model.Readed).Reverse());
		}

		public void SortByDate()
		{
			IEnumerable<BookPresenter> books = getBooksOnView();
			view.MenuControls.Clear();
			addPresentersToView(books.OrderBy(a => a.Model.FullReadDate).Reverse());
		}

		public void SortByBookmark()
		{
			IEnumerable<BookPresenter> books = getBooksOnView();
			view.MenuControls.Clear();
			addPresentersToView(books.OrderBy(a => a.Model.Bookmark).Reverse());
		}

		public void SortByCoR()
		{
			IEnumerable<BookPresenter> books = getBooksOnView();
			view.MenuControls.Clear();
			addPresentersToView(books.OrderBy(a => a.Model.CountOfReadings).Reverse());
		}
	}
}
