using BO_Films;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TablesLibrary.Interpreter;
using TablesLibrary.Interpreter.Table;
using TablesLibrary.Interpreter.TableCell;
using TL_Objects;
using TL_Tables;
using WpfApp.Config;

namespace WpfApp.MVP.Models
{
    public class MainWindowModel
    {
        public Boolean InfoUnsaved { get; set; }
        public TableCollection TableCollection { get; private set; }
        public ProgramSettings Settings { get; private set; }
		public Tables Tables { get; private set; }

        public event EventHandler UserChanged;
        private UserBO userBO;

        public Boolean IsLoggedIn
        {
            get
            {
                return LoggedInUser != null;
            }
        }
        public UserBO LoggedInUser
        {
            get { return userBO; }
            set
            {
                userBO = value;
                EventHandler handler = UserChanged;
                if (null != handler) handler(null, EventArgs.Empty);
            }
        }

        public MainWindowModel()
        {
            InfoUnsaved = false;
			Tables = new Tables(TableCollection);
			Tables.SetDefaultMainTableCollection();
            Settings = ProgramSettings.Initialize();

            TableCollection.FileEncoding = Encoding.UTF8;
        }
	}

	public class Tables
	{
		public CategoriesTable CategoriesTable { get; private set; }
		public GenresTable GenresTable { get; private set; }
		public FilmsTable FilmsTable { get; private set; }
		public SeriesTable SeriesTable { get; private set; }
		public PriorityFilmsTable PriorityFilmsTable { get; private set; }
		public BookGenresTable BookGenresTable { get; private set; }
		public BooksTable BooksTable { get; private set; }
		public BookCategoriesTable BookCategoriesTable { get; private set; }
		public PriorityBooksTable PriorityBooksTable { get; private set; }

		private TableCollection tableCollection;
		public Tables(TableCollection collection)
        {
			tableCollection = collection;
        }

		public void SetDefaultMainTableCollection()
		{
			tableCollection = new TableCollection();

			CategoriesTable = new CategoriesTable();
			GenresTable = GenresTable.GetDefaultGenresTable();
			FilmsTable = new FilmsTable();
			SeriesTable = new SeriesTable();
			PriorityFilmsTable = new PriorityFilmsTable();
			BookGenresTable = new BookGenresTable();
			BooksTable = new BooksTable();
			BookCategoriesTable = new BookCategoriesTable();
			PriorityBooksTable = new PriorityBooksTable();


			tableCollection.RemoveAllTables(true);

			tableCollection.AddTable(CategoriesTable);
			tableCollection.AddTable(GenresTable);
			tableCollection.AddTable(FilmsTable);
			tableCollection.AddTable(SeriesTable);
			tableCollection.AddTable(PriorityFilmsTable);
			tableCollection.AddTable(BookGenresTable);
			tableCollection.AddTable(BooksTable);
			tableCollection.AddTable(BookCategoriesTable);
			tableCollection.AddTable(PriorityBooksTable);
		}

		public TableCollection GetDefaultTableCollectionData()
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
			export.AddTable(new DefaultTable<PriorityBook>());

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
	}
}
