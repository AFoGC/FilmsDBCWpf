using WpfApp.Config;
using WpfApp.Visual.MainWindow;
using System;
using System.Collections.Generic;
using System.Text;
using TablesLibrary.Interpreter;
using TL_Objects;
using TL_Tables;

namespace WpfApp
{
	public static class MainInfo
	{
		static MainInfo()
        {
			MainWindow = (MainWindow)App.Current.MainWindow;

			Tables.SetDefaultMainTableCollection();
			Settings = ProgramSettings.Initialize();
			TableCollection.TableFilePath = Settings.UsedProfile.MainFilePath;
			TableCollection.FileEncoding = Encoding.UTF8;
        }

		public static TableCollection TableCollection { get; private set; }
		public static MainWindow MainWindow { get; private set; }
		public static ProgramSettings Settings { get; private set; }

		public static class Tables
		{
			public static void SetDefaultMainTableCollection()
			{
				TableCollection = new TableCollection();

				CategoriesTable = new CategoriesTable();
				GenresTable = GenresTable.GetDefaultGenresTable();
				FilmsTable = new FilmsTable();
				SeriesTable = new SeriesTable();
				PriorityFilmsTable = new PriorityFilmsTable();
				BookGenresTable = new BookGenresTable();
				BooksTable = new BooksTable();
				BookCategoriesTable = new BookCategoriesTable();

				TableCollection.RemoveAllTables(true);

				TableCollection.AddTable(CategoriesTable);
				TableCollection.AddTable(GenresTable);
				TableCollection.AddTable(FilmsTable);
				TableCollection.AddTable(SeriesTable);
				TableCollection.AddTable(PriorityFilmsTable);
				TableCollection.AddTable(BookGenresTable);
				TableCollection.AddTable(BooksTable);
				TableCollection.AddTable(BookCategoriesTable);
			}

			public static TableCollection GetDefaultTableCollectionData()
			{
				TableCollection export = new TableCollection();

				export.AddTable(new DefaultTable<Category>());
				export.AddTable(GenresTable.GetDefaultGenresTable());
				export.AddTable(new DefaultTable<Film>());
				export.AddTable(new DefaultTable<Serie>());
				export.AddTable(new DefaultTable<PriorityFilm>());
				export.AddTable(new DefaultTable<BookGenre>());
				export.AddTable(new DefaultTable<Book>());
				export.AddTable(new DefaultTable<BookCategory>());

				export.FileEncoding = Encoding.UTF8;

				return export;
			}


			private class DefaultTable<Te> : Table<Te> where Te : Cell, new()
			{
				public DefaultTable() : base() { }
				public DefaultTable(int id) : base(id) { }
				public DefaultTable(int id, string name) : base(id, name) { }
				public override void ConnectionsSubload(TableCollection tablesCollection)
				{
					throw new NotImplementedException();
				}
			}



			public static CategoriesTable CategoriesTable { get; private set; }
			public static GenresTable GenresTable { get; private set; }
			public static FilmsTable FilmsTable { get; private set; }
			public static SeriesTable SeriesTable { get; private set; }
			public static PriorityFilmsTable PriorityFilmsTable { get; private set; }
			public static BookGenresTable BookGenresTable { get; private set; }
			public static BooksTable BooksTable { get; private set; }
			public static BookCategoriesTable BookCategoriesTable { get; private set; }
		}
	}
}
