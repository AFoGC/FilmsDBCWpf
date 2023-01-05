using NewTablesLibrary;
using TL_Objects;

namespace TL_Tables
{
    public class PriorityBooksTable : Table<PriorityBook>
    {
        public PriorityBooksTable()
        {

        }

        public bool ContainsBook(Book book)
        {
            return this.Any(x => x.Book == book);
        }
    }
}
