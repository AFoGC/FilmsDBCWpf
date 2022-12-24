using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Text;
using TablesLibrary.Interpreter;
using TablesLibrary.Interpreter.Table;
using TablesLibrary.Interpreter.TableCell;
using TL_Objects;
using TL_Tables.Interfaces;

namespace TL_Tables
{
	public class BooksTable : Table<Book>, IHasMarkSystem
	{
		private int markSystem;
		public bool NewMarkSystem { get; private set; }
		public int MarkSystem
		{
			get => markSystem;
			set
			{
				markSystem = value;
				foreach (Book book in this)
				{
					book.FormatedMark.MarkSystem = markSystem;
				}
			}
		}

		public BooksTable()
        {
			NewMarkSystem = false;
			MarkSystem = 6;
            this.CollectionChanged += BooksTable_CollectionChanged; ;
		}

        private void BooksTable_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
			if (e.Action == NotifyCollectionChangedAction.Add)
			{
				Book book = (Book)e.NewItems[0];
				book.FormatedMark.MarkSystem = MarkSystem;
			}
			if (e.Action == NotifyCollectionChangedAction.Remove)
			{
				Book book = (Book)e.OldItems[0];
				if (book.FranshiseId != 0)
				{
                    BookCategoriesTable categoriesTable = (BookCategoriesTable)TableCollection.GetTable<BookCategory>();
                    BookCategory category = categoriesTable.GetCategoryByBook(book);
                    category.CategoryElements.Remove(book);
                }
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
            MarkSystem = MarkSystem;
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

			if (!this.NewMarkSystem)
			{
				changeToNewMarkSystem();
			}
		}

		private void changeToNewMarkSystem()
		{
			foreach (Book book in this)
			{
				book.Mark *= 50;
			}

			this.NewMarkSystem = true;
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
