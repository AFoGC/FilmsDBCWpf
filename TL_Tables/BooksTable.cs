﻿using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Text;
using TablesLibrary.Interpreter;
using TablesLibrary.Interpreter.Table;
using TablesLibrary.Interpreter.TableCell;
using TL_Objects;

namespace TL_Tables
{
	public class BooksTable : Table<Book>
	{
		private int markSystem;
		public bool NewMarkSystem { get; private set; }
		public int MarkSystem
		{
			get => markSystem;
			set
			{
				markSystem = value;
				foreach (Film film in this)
				{
					film.FormatedMark.MarkSystem = markSystem;
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
				Film film = (Film)e.NewItems[0];
				film.FormatedMark.MarkSystem = MarkSystem;
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
