using System;
using System.Collections.Generic;
using System.Text;
using TablesLibrary.Interpreter;
using TablesLibrary.Interpreter.Table;
using TL_Objects;

namespace TL_Tables
{
    public class PriorityBooksTable : Table<PriorityBook>
    {
        public PriorityBooksTable() : base() { }
        public PriorityBooksTable(int id) : base(id) { }
        public PriorityBooksTable(int id, string name) : base(id, name) { }

        public override void ConnectionsSubload(TableCollection tablesCollection)
        {
            Table<Book> booksTable = tablesCollection.GetTable<Book>();

            foreach (Book book in booksTable)
            {
                foreach (PriorityBook priorityBook in this)
                {
                    if (priorityBook.BookId == book.ID)
                    {
                        priorityBook.Book = book;
                    }
                }
            }
        }
    }
}
