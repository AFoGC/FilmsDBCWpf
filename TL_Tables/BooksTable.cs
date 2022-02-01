using System;
using System.Collections.Generic;
using System.Text;
using TablesLibrary.Interpreter;
using TablesLibrary.Interpreter.Table;
using TL_Objects;

namespace TL_Tables
{
	public class BooksTable : Table<Book>
	{
		public BooksTable() : base() { }
		public BooksTable(int id) : base(id) { }
		public BooksTable(int id, string name) : base(id, name) { }

		public override void ConnectionsSubload(TableCollection tablesCollection)
		{
			Table<BookGenre> genresTable = tablesCollection.GetTable<BookGenre>();

			foreach (Book book in this)
			{
				if (book.BookGenreId != 0)
				{
					foreach (BookGenre genre in genresTable)
					{
						if (book.BookGenreId == genre.ID)
						{
							book.BookGenre = genre;
							break;
						}
					}
				}
				else
				{
					book.BookGenre = genresTable.DefaultCell;
				}
			}
		}

		public bool GenreHasBook(BookGenre genre)
		{
			bool hasGenre = false;

			foreach (Book book in this)
			{
				if (book.BookGenre == genre)
				{
					return true;
				}
			}

			return hasGenre;
		}
	}
}
