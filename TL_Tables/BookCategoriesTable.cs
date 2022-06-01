using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using TablesLibrary.Interpreter;
using TablesLibrary.Interpreter.Table;
using TL_Objects;

namespace TL_Tables
{
	public class BookCategoriesTable : Table<BookCategory>
	{
		public BookCategoriesTable() : base() { }
		public BookCategoriesTable(int id) : base(id) { }
		public BookCategoriesTable(int id, string name) : base(id, name) { }


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
