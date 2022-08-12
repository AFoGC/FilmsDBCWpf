using FilmsUCWpf.Presenter.Interfaces;
using FilmsUCWpf.View.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TablesLibrary.Interpreter;
using TL_Objects;

namespace FilmsUCWpf.Presenter
{
    public class BookUpdatePresenter : IUpdatePresenter
	{
		private Book model;
		private IBookUpdateView view;
		private IMenuPresenter<Book> menu;
		private TableCollection collection;

		public BookUpdatePresenter(Book model, IBookUpdateView view, IMenuPresenter<Book> menu, TableCollection table)
		{
			this.model = model;
			this.view = view;
			this.menu = menu;
			collection = table;
			foreach (BookGenre genre in collection.GetTable<BookGenre>())
			{
				view.Genres.Add(genre);
			}

			RefreshElement();
		}

		private static Book defBook = new Book();
		public void RefreshElement()
		{
			refreshComboBox();

			view.ID = model.ID.ToString();
			view.Name = model.Name;
			view.Genre = model.BookGenre;
			view.RealiseYear = Book.FormatToString(model.PublicationYear, defBook.PublicationYear);
			view.Readed = model.Readed;
			view.Author = model.Author;
			view.FullReadDate = model.FullReadDate;
			view.CountOfReadings = Book.FormatToString(model.CountOfReadings, defBook.CountOfReadings);
			view.Bookmark = model.Bookmark;
		}

		private void refreshComboBox()
		{
			view.Marks.Clear();
			foreach (string mark in model.FormatedMark.GetComboItems())
			{
				view.Marks.Add(mark);
			}
			view.Mark = model.FormatedMark.ToString();
		}

		public void UpdateElement()
		{
			model.Name = view.Name;
			model.BookGenre = view.Genre;
			model.Readed = view.Readed;
			model.Author = view.Author;
			model.FullReadDate = view.FullReadDate;
			model.Bookmark = view.Bookmark;

			model.PublicationYear = Helper.TextToInt32(view.RealiseYear);
			model.CountOfReadings = Helper.TextToInt32(view.CountOfReadings);

			model.FormatedMark.SetMarkFromString(view.Mark);
		}

		public void OpenSources()
		{
			menu.OpenSourcesInfo(model.Sources);
		}
	}
}
