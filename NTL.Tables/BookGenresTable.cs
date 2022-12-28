using NewTablesLibrary;
using TL_Objects;

namespace TL_Tables
{
    public class BookGenresTable : Table<BookGenre>
	{
		public BookGenresTable()
        {

        }

        public static BookGenresTable GetDefaultGenresTable()
        {
            BookGenresTable export = new BookGenresTable();

            BookGenre genre = new BookGenre { Name = "Book"};
            export.Add(genre);

            return export;
        }
    }
}
