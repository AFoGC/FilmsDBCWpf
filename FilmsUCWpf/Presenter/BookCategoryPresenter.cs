using FilmsUCWpf.PresenterInterfaces;
using FilmsUCWpf.View;
using FilmsUCWpf.ViewInterfaces;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TablesLibrary.Interpreter;
using TablesLibrary.Interpreter.Table;
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
			refreshCategoryBooks();
		}

		public void refreshCategoryBooks()
		{
			View.CategoryCollection.Clear();
			presenters.Clear();
			View.Height = View.DefaultHeght;

			foreach (Book book in Model.Books)
			{
				BookPresenter bookPresenter = new BookPresenter(book, new BookInCategorySimpleControl(), menu, TableCollection);
				presenters.Add(bookPresenter);
				View.Height += bookPresenter.View.Height;
				View.CategoryCollection.Add(bookPresenter.View);
			}
		}

		private void Books_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			Book book;
			BookPresenter presenter;
			switch (e.Action)
			{
				case NotifyCollectionChangedAction.Add:
					book = (Book)e.NewItems[0];
					presenter = new BookPresenter(book, new BookSimpleControl(), menu, TableCollection);
					presenters.Add(presenter);
					View.CategoryCollection.Add(presenter.View);
					break;
				case NotifyCollectionChangedAction.Remove:
					book = (Book)e.OldItems[0];
					presenter = presenters.Where(x => x.Model == book).FirstOrDefault();
					presenters.Remove(presenter);
					View.CategoryCollection.Remove(presenter?.View);
					break;
				case NotifyCollectionChangedAction.Reset:
					presenters.Clear();
					View.CategoryCollection.Clear();
					break;
				default:
					break;
			}
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

		public void AddSelected()
		{
			if (menu.SelectedElement != null)
			{
				Book book = menu.SelectedElement.Model;
				if (book.FranshiseId == 0)
				{
					book.FranshiseId = Model.ID;
					book.FranshiseListIndex = (sbyte)(Model.Books.Count);
					Model.Books.Add(book);
					menu.RemovePresenter(menu.SelectedElement);
					menu.SelectedElement = null;
				}
			}
		}

		public void RemoveSelected()
		{
			if (menu.SelectedElement != null)
			{
				Book book = menu.SelectedElement.Model;
				if (Model.RemoveBookFromCategory(book))
                {
					menu.AddPresenter(menu.SelectedElement);
					menu.SelectedElement = null;
				}
			}
		}

		public void DeleteThisCategory()
		{
			Table<BookCategory> cateories = TableCollection.GetTable<BookCategory>();
			if (Model.Books.Count == 0)
			{
				cateories.Remove(Model);
			}
		}
	}
}
