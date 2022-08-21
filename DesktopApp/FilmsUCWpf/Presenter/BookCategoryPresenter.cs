﻿using FilmsUCWpf.Presenter.Interfaces;
using FilmsUCWpf.View;
using FilmsUCWpf.View.Interfaces;
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
		protected IMenuPresenter<Book> menu;
		private List<BookPresenter> presenters;

		new ICategoryView View { get => (ICategoryView)base.View; }

		public BookCategoryPresenter(BookCategory category, ICategoryView view, IMenuPresenter<Book> menu, TableCollection collection) : base(category, view, collection)
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

			foreach (Book book in Model.Books)
			{
				BookPresenter bookPresenter = new BookPresenter(book, new BookInCategorySimpleControl(), menu, TableCollection);
				presenters.Add(bookPresenter);
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
					presenter = new BookPresenter(book, new BookInCategorySimpleControl(), menu, TableCollection);
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
				case NotifyCollectionChangedAction.Move:
					IView view = null;
					foreach (IView item in View.CategoryCollection)
					{
						if (item.Presenter.ModelCell == e.OldItems[0])
						{
							view = item;
							break;
						}
					}
					View.CategoryCollection.Remove(view);
					View.CategoryCollection.Insert(e.NewStartingIndex, view);
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
			menu.OpenMoreInfo(new BookCategoryUpdateControl(Model, menu.Model, TableCollection));
		}

		public void CreateBookInCategory()
        {
			Book book = new Book();
			book.Name = getDefaulBookName();
			book.BookGenre = TableCollection.GetTable<BookGenre>()[0];
			TableCollection.GetTable<Book>().AddElement(book);
			Model.Books.Add(book);
			menu.Model.RemoveElement(book);
		}

		private string getDefaulBookName()
        {
            if (Model.HideName == string.Empty)
				return Model.Name;
            else
				return Model.HideName;
        }

		public void AddSelected()
		{
			if (menu.Model.SelectedElement != null)
			{
				Book book = menu.Model.SelectedElement.Model;
				if (book.FranshiseId == 0)
				{
					Model.Books.Add(book);
					menu.Model.RemoveElement(menu.Model.SelectedElement.Model);
					menu.Model.SelectedElement = null;
				}
			}
		}

		public void RemoveSelected()
		{
			if (menu.Model.SelectedElement != null)
			{
				Book book = menu.Model.SelectedElement.Model;
				if (Model.RemoveBookFromCategory(book))
                {
					menu.Model.AddElement(menu.Model.SelectedElement.Model);
					menu.Model.SelectedElement = null;
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