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
	public class BooksMenuPresenter : IMenu<Book>
	{
		private BooksMenuModel model;
		private IBaseMenuView view;
		private MainWindowModel mainModel;

		public BooksMenuPresenter(BooksMenuModel model, IBaseMenuView view, MainWindowModel windowModel)
		{
			this.model = model;
			this.view = view;
			this.mainModel = windowModel;
			this.model.MoreInfoFormVisualizer = new MoreInfoFormVisualizer(view.InfoCanvas);
			this.model.UpdateFormVisualizer = new UpdateFormVisualizer(view.InfoCanvas);
			this.model.MoreInfoFormVisualizer.UpdateVisualizer = this.model.UpdateFormVisualizer;
			this.model.UpdateFormVisualizer.MoreVisualizer = this.model.MoreInfoFormVisualizer;
			mainModel.TableCollection.TableLoad += TableCollection_TableLoad;
            mainModel.Tables.BookGenresTable.CollectionChanged += BookGenresTable_CollectionChanged;
		}

        private void BookGenresTable_CollectionChanged(object sender, EventArgs e)
        {
			LoadGenres();
        }

        public BasePresenter<Book> SelectedElement { get => model.SelectedElement; set => model.SelectedElement = (BookPresenter)value; }
		public MoreInfoFormVisualizer MoreInfoFormVisualizer => model.MoreInfoFormVisualizer;
		public UpdateFormVisualizer UpdateFormVisualizer => model.UpdateFormVisualizer;
		private TableCollection TabColl => mainModel.TableCollection;

		private void TableCollection_TableLoad(object sender, EventArgs e)
		{
			LoadCategories();
			LoadGenres();
		}

		public void SaveTables()
		{
			mainModel.TableCollection.SaveTables();
		}

		private void ClearControls()
		{
			view.MenuControls.Clear();
			model.BasePresenters.Clear();
		}

		public void LoadGenres()
		{
			view.GenresControls.Clear();
			foreach (BookGenre genre in mainModel.Tables.BookGenresTable)
			{
				view.GenresControls.Add(new GenrePressButtonControl(genre));
			}
		}

		public void LoadCategories()
		{
			ClearControls();
			model.ControlsCondition = BooksMenuModel.MenuCondition.Category;
			model.SelectedElement = null;

			foreach (BookCategory category in mainModel.Tables.BookCategoriesTable)
			{
				model.BasePresenters.Add(new BookCategoryPresenter(category, new BookCategoryControl(), this, TabColl));
			}

			foreach (Book book in mainModel.Tables.BooksTable)
			{
				if (book.FranshiseId == 0)
				{
					model.BasePresenters.Add(new BookPresenter(book, new BookSimpleControl(), this, TabColl));
				}
			}

			foreach (IBasePresenter presenter in model.BasePresenters)
			{
				presenter.AddViewToCollection(view.MenuControls);
			}
		}

		public void LoadBooks()
        {
			ClearControls();
			model.ControlsCondition = BooksMenuModel.MenuCondition.Book;
			model.SelectedElement = null;

			foreach (Book book in mainModel.Tables.BooksTable)
			{
				model.BasePresenters.Add(new BookPresenter(book, new BookControl(), this, TabColl));
			}
			foreach (IBasePresenter presenter in model.BasePresenters)
			{
				presenter.AddViewToCollection(view.MenuControls);
			}
		}

		public void LoadPriorityTable()
        {
			ClearControls();
			model.ControlsCondition = BooksMenuModel.MenuCondition.PriorityBook;
			model.SelectedElement = null;

			PriorityBooksTable priorityBooks = mainModel.Tables.PriorityBooksTable;
			priorityBooks.RemoveWatchedBooks();

            foreach (PriorityBook priority in priorityBooks)
            {
				model.BasePresenters.Add(new BookPresenter(priority.Book, new BookSimpleControl(), this, TabColl));
			}

			foreach (IBasePresenter presenter in model.BasePresenters)
			{
				presenter.AddViewToCollection(view.MenuControls);
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
			BookGenre[] genres = GetSelectedGenres();


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
					foreach (IHasGenre hasGenre in model.BasePresenters)
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

		public void AddCategory()
		{
			if (model.ControlsCondition == BooksMenuModel.MenuCondition.Category)
			{
				BookCategoriesTable categories = mainModel.Tables.BookCategoriesTable;
				BookCategory category = new BookCategory();
				categories.AddElement(category);
				BookCategoryPresenter presenter = new BookCategoryPresenter(category, new BookCategoryControl(), this, TabColl);
				model.BasePresenters.Insert(categories.Count - 1, presenter);
				view.MenuControls.Insert(categories.Count - 1, presenter.View);
			}
		}

		public void AddBook()
		{
			Book book = new Book();
			book.BookGenre = mainModel.Tables.BookGenresTable[0];
			mainModel.Tables.BooksTable.AddElement(book);
			BookPresenter presenter;
			switch (model.ControlsCondition)
			{
				case BooksMenuModel.MenuCondition.Category:
					presenter = new BookPresenter(book, new BookSimpleControl(), this, TabColl);
					AddPresenter(presenter);
					break;

				case BooksMenuModel.MenuCondition.Book:
					presenter = new BookPresenter(book, new BookControl(), this, TabColl);
					AddPresenter(presenter);
					break;

				default:
					break;
			}
		}

		public void RemoveSelectedBook()
        {
			Book book = model.SelectedElement.Model;

			if (book.FranshiseId == 0)
				mainModel.Tables.BooksTable.Remove(book);
			model.BasePresenters.Remove(model.SelectedElement);
        }

		public bool AddSelected()
		{
			if (SelectedElement != null)
			{
				int i = 0;
				Type type = typeof(BasePresenter<Book>);
				foreach (IBasePresenter item in model.BasePresenters)
				{
					if (item.GetType().IsSubclassOf(type))
					{
						BasePresenter<Book> basePresenter = (BasePresenter<Book>)item;
						Book book = basePresenter.Model;
						if (book.ID > model.SelectedElement.Model.ID) break;
					}
					++i;
				}
				BookPresenter presenter = new BookPresenter(model.SelectedElement.Model, new BookSimpleControl(), this, TabColl);
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
