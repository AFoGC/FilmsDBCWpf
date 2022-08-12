using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Text;
using TablesLibrary.Interpreter;
using TablesLibrary.Interpreter.Table;
using TablesLibrary.Interpreter.TableCell;
using TL_Objects;
using TL_Tables.Interfaces;

namespace TL_Tables
{
	public class BookCategoriesTable : Table<BookCategory>, IHasMarkSystem
	{
		private int markSystem;
		public bool NewMarkSystem { get; private set; }
		public int MarkSystem
		{
			get => markSystem;
			set
			{
				markSystem = value;
				foreach (BookCategory category in this)
				{
					category.FormatedMark.MarkSystem = markSystem;
				}
			}
		}
		public BookCategoriesTable()
        {
			NewMarkSystem = false;
			MarkSystem = 6;
			this.CollectionChanged += CategoriesTable_CollectionChanged;
		}

		private void CategoriesTable_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			if (e.Action == NotifyCollectionChangedAction.Add)
			{
				BookCategory book = (BookCategory)e.NewItems[0];
				book.FormatedMark.MarkSystem = MarkSystem;
			}
		}

		protected override void saveBody(StreamWriter streamWriter)
		{
			streamWriter.Write(Cell.FormatParam("newMarkSystem", NewMarkSystem, false, 1));
			streamWriter.Write(Cell.FormatParam("markSystem", MarkSystem, 6, 1));
		}

		protected override void loadBody(Comand comand)
		{
			switch (comand.Paramert)
			{
				case "newMarkSystem":
					NewMarkSystem = Convert.ToBoolean(comand.Value);
					break;
				case "markSystem":
					MarkSystem = Convert.ToInt32(comand.Value);
					break;

				default:
					break;
			}
		}

		public override void ConnectionsSubload(TableCollection tablesCollection)
		{
			Table<Book> booksTable = tablesCollection.GetTable<Book>();

			List<Book> categoryFilms = new List<Book>();
			foreach (BookCategory category in this)
			{
				categoryFilms.Clear();
				foreach (Book book in booksTable)
				{
					if (book.FranshiseId == category.ID)
					{
						categoryFilms.Add(book);
					}
				}
				sortBooks(category.Books, categoryFilms);
			}

			if (!this.NewMarkSystem)
			{
				changeToNewMarkSystem();
			}
		}

		private void changeToNewMarkSystem()
		{
			foreach (BookCategory category in this)
			{
				category.Mark *= 50;
			}

			this.NewMarkSystem = true;
		}

		private void sortBooks(ObservableCollection<Book> categoryBooks, List<Book> source)
		{
			IEnumerable<Book> enumerable = source.OrderBy(o => o.FranshiseListIndex);

			foreach (Book book in enumerable)
			{
				categoryBooks.Add(book);
			}
		}

		public BookCategory GetCategoryByBook(Book book)
        {
            foreach (BookCategory item in this)
            {
				if (item.Books.Contains(book)) 
					return item;
            }
			return null;
        }
	}
}
