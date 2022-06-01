using FilmsUCWpf.PresenterInterfaces;
using FilmsUCWpf.View;
using FilmsUCWpf.ViewInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TablesLibrary.Interpreter;
using TL_Objects;
using TL_Objects.Interfaces;

namespace FilmsUCWpf.Presenter
{
	public class BookCategoryPresenter : BasePresenter<BookCategory>, IHasGenre
	{
		protected IMenu<Book> menu;
		private List<BookPresenter> presenters;

		new ICategoryView View { get => (ICategoryView)base.View; }

		public BookCategoryPresenter(BookCategory category, ICategoryView view, IMenu<Book> menu, TableCollection collection) : base(category, view, collection)
		{
			this.menu = menu;
			presenters = new List<BookPresenter>();
			category.Books.CollectionChanged += Books_CollectionChanged;
			RefreshCategoryBooks();
		}

		private void Books_CollectionChanged(object sender, EventArgs e)
		{
			RefreshCategoryBooks();
		}

		public bool HasSelectedGenre(IGenre[] selectedGenres)
		{
			foreach (BookPresenter presenter in presenters)
			{
				if (presenter.HasSelectedGenre(selectedGenres))
				{
					return true;
				}
			}

			return false;
		}

		private void AddViewPresenter(BookPresenter presenter)
		{
			View.Height += 15;
			View.CategoryCollection.Add(presenter.View);
		}

		public void RefreshCategoryBooks()
		{
			View.CategoryCollection.Clear();
			presenters.Clear();
			View.Height = View.DefaultHeght;

			foreach (Book book in Model.Books)
			{
				BookPresenter bookPresenter = new BookPresenter(book, new BookInCategorySimpleControl(), menu, TableCollection);
				presenters.Add(bookPresenter);
				AddViewPresenter(bookPresenter);
			}
		}

		public bool RemoveBookFromCategory(BookPresenter presenter)
		{
			if (Model.RemoveBookFromCategory(presenter.Model))
			{
				View.CategoryCollection.Remove(presenter.View);
				presenters.Remove(presenter);
				View.Height -= 15;

				foreach (Book book in Model.Books)
				{
					if (presenter.Model.FranshiseListIndex < book.FranshiseListIndex)
					{
						--book.FranshiseListIndex;
					}
				}
				return true;
			}
			return false;
		}

		public override bool HasCheckedProperty(bool isReaded)
		{
			foreach (IBasePresenter presenter in presenters)
			{
				if (presenter.HasCheckedProperty(isReaded))
				{
					return true;
				}
			}

			return false;
		}

		public override bool SetFindedElement(string search)
		{
			bool export = false;
			if (Model.Name.ToLowerInvariant().Contains(search))
			{
				View.SetVisualFinded();
			}

			foreach (BookPresenter presenter in presenters)
			{
				presenter.SetFindedElement(search);
			}

			return export;
		}

		public override void SetSelectedElement()
		{
			View.SetVisualSelected();
		}

		public override void SetVisualDefault()
		{
			View.SetVisualDefault();
			foreach (BookPresenter presenter in presenters)
			{
				presenter.SetVisualDefault();
			}
		}

		public void OpenUpdateMenu()
		{
			menu.UpdateFormVisualizer.OpenUpdateControl(new BookCategoryUpdateControl(Model, menu, TableCollection));
		}

		public void CreateBookInCategory()
        {
			Book book = new Book();
			book.BookGenre = TableCollection.GetTable<BookGenre>()[0];
			TableCollection.GetTable<Book>().AddElement(book);
			Model.Books.Add(book);
        }
	}
}
