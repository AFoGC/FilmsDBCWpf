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
		}

		public void UpdateElement()
		{
			model.Name = view.Name;
		}
	}
}
