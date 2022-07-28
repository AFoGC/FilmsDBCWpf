using FilmsUCWpf.Presenter.Interfaces;
using FilmsUCWpf.View;
using FilmsUCWpf.View.Interfaces;
using System;
using System.Collections.Generic;
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
    public class BookPresenter : BasePresenter<Book>, IHasGenre
	{
		protected IMenuPresenter<Book> menu;
		public BookPresenter(Book book, IView view, IMenuPresenter<Book> menu, TableCollection collection) : base(book, view, collection)
		{
			this.menu = menu;
		}

		public override bool HasCheckedProperty(bool isReaded)
		{
			return isReaded == Model.Readed;
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
			menu.Model.SelectedElement = this;
			View.SetVisualSelected();
		}

		public override void SetVisualDefault()
		{
			View.SetVisualDefault();
		}

		public bool HasSelectedGenre(IGenre[] selectedGenres)
		{
			foreach (IGenre genre in selectedGenres)
			{
				if (genre == Model.BookGenre)
				{
					return true;
				}
			}
			return false;
		}

		public void DeleteThis()
		{
			BooksTable booksTable = (BooksTable)TableCollection.GetTable<Book>();
			BookCategoriesTable categoriesTable = (BookCategoriesTable)TableCollection.GetTable<BookCategory>();

			if (Model.FranshiseId != 0)
			{
				BookCategory category = categoriesTable.GetCategoryByBook(Model);
				category.RemoveBookFromCategory(Model);
			}

			booksTable.Remove(Model);
		}

		public void AddToPriority()
        {
			PriorityBooksTable priorityFilms = (PriorityBooksTable)TableCollection.GetTable<PriorityBook>();
			if (!priorityFilms.ContainBook(Model))
			{
				PriorityBook priority = new PriorityBook();
				priority.Book = Model;
				priorityFilms.AddElement(priority);
			}
		}

		public void CopyUrl()
		{
			Helper.CopyFirstSource(Model.Sources);
		}

		public void OpenUpdateMenu()
		{
			menu.OpenUpdateInfo(new BookUpdateControl(Model, menu, TableCollection));
		}

		public void OpenInfoMenu()
		{
			IView view = new BookControl();
			BookPresenter presenter = new BookPresenter(Model, view, menu, TableCollection);
			menu.OpenMoreInfo(presenter.View);
		}

		public void UpFranshiseListID()
		{
			BookCategoriesTable categories = (BookCategoriesTable)TableCollection.GetTable<BookCategory>();
			BookCategory category = categories.GetCategoryByBook(Model);
			category.ChangeBookPositionBy(Model, -1);
		}

		public void DownFranshiseListID()
		{
			BookCategoriesTable categories = (BookCategoriesTable)TableCollection.GetTable<BookCategory>();
			BookCategory category = categories.GetCategoryByBook(Model);
			category.ChangeBookPositionBy(Model, 1);
		}

		public void RemoveThisFromCategory()
		{
			BookCategoriesTable categories = (BookCategoriesTable)TableCollection.GetTable<BookCategory>();
			BookCategory category = categories.GetCategoryByBook(Model);
			if (category != null)
			{
				category.RemoveBookFromCategory(Model);
				menu.Model.AddElement(this.Model);
			}
		}
	}
}
