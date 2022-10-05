using System.Text;
using TablesLibrary.Interpreter;
using TL_Objects;
using TL_Tables;

namespace WpfApp.Models
{
	public class TLTables
	{
		public CategoriesTable CategoriesTable => (CategoriesTable)tcoll.GetTable<Category>();
		public GenresTable GenresTable => (GenresTable)tcoll.GetTable<Genre>();
		public FilmsTable FilmsTable => (FilmsTable)tcoll.GetTable<Film>();
		public SeriesTable SeriesTable => (SeriesTable)tcoll.GetTable<Serie>();
		public PriorityFilmsTable PriorityFilmsTable => (PriorityFilmsTable)tcoll.GetTable<PriorityFilm>();
		public BookGenresTable BookGenresTable => (BookGenresTable)tcoll.GetTable<BookGenre>();
		public BooksTable BooksTable => (BooksTable)tcoll.GetTable<Book>();
		public BookCategoriesTable BookCategoriesTable => (BookCategoriesTable)tcoll.GetTable<BookCategory>();
		public PriorityBooksTable PriorityBooksTable => (PriorityBooksTable)tcoll.GetTable<PriorityBook>();

		private TableCollection tcoll;
		public TLTables(TableCollection collection)
		{
			tcoll = collection;
		}

		public void Initialize()
		{
            tcoll.FileEncoding = Encoding.UTF8;

            tcoll.AddTable(new CategoriesTable());
            tcoll.AddTable(GenresTable.GetDefaultGenresTable());
            tcoll.AddTable(new FilmsTable());
            tcoll.AddTable(new SeriesTable());
            tcoll.AddTable(new PriorityFilmsTable());
            tcoll.AddTable(BookGenresTable.GetDefaultGenresTable());
            tcoll.AddTable(new BooksTable());
            tcoll.AddTable(new BookCategoriesTable());
            tcoll.AddTable(new PriorityBooksTable());
        }

		public static TableCollection GetDefaultTableCollectionData()
		{
			TableCollection export = new TableCollection();
			export.FileEncoding = Encoding.UTF8;

			export.AddTable(new CategoriesTable());
			export.AddTable(GenresTable.GetDefaultGenresTable());
			export.AddTable(new FilmsTable());
			export.AddTable(new SeriesTable());
			export.AddTable(new PriorityFilmsTable());
			export.AddTable(BookGenresTable.GetDefaultGenresTable());
			export.AddTable(new BooksTable());
			export.AddTable(new BookCategoriesTable());
			export.AddTable(new PriorityBooksTable());

			return export;
		}
	}
}
