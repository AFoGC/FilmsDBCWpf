using System.Text;
using TablesLibrary.Interpreter;
using TL_Objects;
using TL_Tables;

namespace WpfApp.Models
{
	public class TLTables
	{
		public CategoriesTable FilmCategoriesTable => (CategoriesTable)tcoll.GetTable<Category>();
		public GenresTable FilmGenresTable => (GenresTable)tcoll.GetTable<Genre>();
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
