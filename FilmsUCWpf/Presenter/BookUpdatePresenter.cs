using FilmsUCWpf.PresenterInterfaces;
using FilmsUCWpf.ViewInterfaces;
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
		private IBaseMenu menu;
		private TableCollection collection;

		public BookUpdatePresenter(Book model, IBookUpdateView view, IMenu<Book> menu, TableCollection table)
		{
			this.model = model;
			this.view = view;
			this.menu = menu;
			collection = table;
			foreach (BookGenre genre in collection.GetTable<BookGenre>())
			{
				view.Genres.Add(genre);
			}
			foreach (string mark in Helper.GetAllMarks())
			{
				view.Marks.Add(mark);
			}
			RefreshElement();
		}

		private static Book defBook = new Book();
		public void RefreshElement()
		{
			view.ID = model.ID.ToString();
			view.Name = model.Name;
			view.Genre = model.BookGenre;
			view.RealiseYear = Book.FormatToString(model.PublicationYear, defBook.PublicationYear);
			view.Readed = model.Readed;
			view.Author = model.Author;
			view.FullReadDate = model.FullReadDate;
			view.Mark = Helper.MarkToText(Book.FormatToString(model.Mark, defBook.Mark));
			view.CountOfReadings = Book.FormatToString(model.CountOfReadings, defBook.CountOfReadings);
			view.Bookmark = model.Bookmark;
		}

		public void UpdateElement()
		{
			model.Name = view.Name;
			model.BookGenre = view.Genre;
			model.PublicationYear = Helper.TextToInt32(view.RealiseYear);
			model.Readed = view.Readed;
			model.Author = view.Author;
			model.FullReadDate = view.FullReadDate;
			model.Mark = Helper.TextToMark(view.Mark);
			model.CountOfReadings = Helper.TextToInt32(view.CountOfReadings);
			model.Bookmark = view.Bookmark;
		}

		public void OpenSources()
		{
			Helper.OpenSources(menu, model.Sources);
		}
	}
}
