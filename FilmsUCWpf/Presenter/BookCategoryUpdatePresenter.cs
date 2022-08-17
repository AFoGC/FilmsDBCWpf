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
	public class BookCategoryUpdatePresenter
	{
		private BookCategory model;
		private IMenuModel<Book> menu;
		private TableCollection tableCollection;

		public BookCategoryUpdatePresenter(BookCategory model, IMenuModel<Book> menu, TableCollection tableCollection)
		{
			this.model = model;
			this.menu = menu;
			this.tableCollection = tableCollection;
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
					menu.RemoveElement(menu.SelectedElement.Model);
					menu.SelectedElement = null;
				}
			}
		}

		public void RemoveSelected()
		{
			if (menu.SelectedElement != null)
			{
				Book book = menu.SelectedElement.Model;
				if (model.RemoveBookFromCategory(book))
				{
					menu.AddElement(menu.SelectedElement.Model);
					menu.SelectedElement = null;
				}
			}
		}

		public void DeleteThisCategory()
		{
			Table<BookCategory> cateories = tableCollection.GetTable<BookCategory>();
			if (model.Books.Count == 0)
			{
				cateories.Remove(model);
			}
		}
	}
}
