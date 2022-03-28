using FilmsUCWpf.PresenterInterfaces;
using FilmsUCWpf.ViewInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TL_Objects;

namespace FilmsUCWpf.Presenter
{
	public class BookCategoryUpdatePresenter : IUpdatePresenter
	{
		private BookCategory model;
		private IBookCategoryUpdateView view;
		private IMenu<Book> menu;

		public BookCategoryUpdatePresenter(BookCategory model, IBookCategoryUpdateView view, IMenu<Book> menu)
		{
			this.model = model;
			this.view = view;
			this.menu = menu;
		}

		public void AddSelected()
		{
			if (menu.ControlInBuffer != null)
			{
				Book book = menu.ControlInBuffer.Model;
				if (book.FranshiseId == 0)
				{
					book.FranshiseId = model.ID;
					model.Books.Add(book);
					menu.RemoveSelected();
				}
			}
		}

		public void RemoveSelected()
		{
			if (menu.ControlInBuffer != null)
			{
				Book book = menu.ControlInBuffer.Model;
				if (model.RemoveBookFromCategory(book))
					menu.AddSelected();
			}
		}

		public void RefreshElement()
		{
			view.ID = model.ID.ToString();
			view.Name = model.Name;
		}

		public void UpdateElement()
		{
			model.Name = view.Name;
		}
	}
}
