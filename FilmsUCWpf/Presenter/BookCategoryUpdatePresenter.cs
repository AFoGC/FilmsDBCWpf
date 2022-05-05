using FilmsUCWpf.PresenterInterfaces;
using FilmsUCWpf.ViewInterfaces;
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
	public class BookCategoryUpdatePresenter : IUpdatePresenter
	{
		private BookCategory model;
		private IBookCategoryUpdateView view;
		private IMenu<Book> menu;
		private TableCollection tableCollection;

		public BookCategoryUpdatePresenter(BookCategory model, IBookCategoryUpdateView view, IMenu<Book> menu, TableCollection tableCollection)
		{
			this.model = model;
			this.view = view;
			this.menu = menu;
			this.tableCollection = tableCollection;
			RefreshElement();
		}

		public void AddSelected()
		{
			if (menu.SelectedElement != null)
			{
				Book book = menu.SelectedElement.Model;
				if (book.FranshiseId == 0)
				{
					book.FranshiseId = model.ID;
					book.FranshiseListIndex = (sbyte)(model.Books.Count);
					model.Books.Add(book);
					menu.RemoveSelected();
				}
			}
		}

		public void RemoveSelected()
		{
			if (menu.SelectedElement != null)
			{
				Book book = menu.SelectedElement.Model;
				if (model.RemoveBookFromCategory(book))
					menu.AddSelected();
			}
		}

		public void RefreshElement()
		{
			view.ID = model.ID.ToString();
			view.Name = model.Name;
			view.HideName = model.HideName;
		}

		public void DeleteThisCategory()
		{
			Table<BookCategory> cateories = tableCollection.GetTable<BookCategory>();
			if (model.Books.Count == 0)
			{
				cateories.Remove(model);
			}
		}

		public void UpdateElement()
		{
			model.Name = view.Name;
			model.HideName = view.HideName;
		}
	}
}
