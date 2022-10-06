using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TablesLibrary.Interpreter;
using TL_Objects;
using TL_Tables;

namespace WpfApp.Models
{
    public class BooksModel
    {
        public TableCollection TableCollection { get; private set; }
        public BooksTable BooksTable { get; private set; }
        public BookCategoriesTable BookCategoriesTable { get; private set; }
        public BookGenresTable BookGenresTable { get; private set; }
        public PriorityBooksTable PriorityBooksTable { get; private set; }

        public BooksModel()
        {
            TableCollection collection = SettingsModel.Initialize().TableCollection;
            TableCollection = collection;

            BooksTable = (BooksTable)collection.GetTable<Book>();
            BookCategoriesTable = (BookCategoriesTable)collection.GetTable<BookCategory>();
            BookGenresTable = (BookGenresTable)collection.GetTable<BookGenre>();
            PriorityBooksTable = (PriorityBooksTable)collection.GetTable<PriorityBook>();
        }
    }
}
