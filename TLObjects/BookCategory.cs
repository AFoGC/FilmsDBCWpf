using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using TablesLibrary.Interpreter;
using TablesLibrary.Interpreter.TableCell;

namespace TL_Objects
{
    [TableCell("BookCategory")]
    public class BookCategory : Cell
    {
        private string name = "";
        private sbyte mark = -1;
        private int priority = 0;

        private TLCollection<Book> books = new TLCollection<Book>();

        public BookCategory() : base() { }
        public BookCategory(int id) : base(id) { }

        public bool RemoveBookFromCategory(Book book)
        {
            if (book.FranshiseId == this.ID)
            {
                book.FranshiseId = 0;
                book.FranshiseListIndex = -1;
                return books.Remove(book);
            }
            else return false;
        }

        protected override void updateThisBody(Cell cell)
        {
            BookCategory category = (BookCategory)cell;

            name = category.name;
            mark = category.mark;
            priority = category.priority;
            books = category.books;
        }

        protected override void saveBody(StreamWriter streamWriter, Cell defaultCell)
        {
            streamWriter.Write(FormatParam("name", name, "", 2));
            streamWriter.Write(FormatParam("mark", mark, -1, 2));
            streamWriter.Write(FormatParam("priority", priority, 0, 2));
        }

        protected override void loadBody(Comand comand)
        {
            switch (comand.Paramert)
            {
                case "name":
                    name = comand.Value;
                    break;
                case "mark":
                    mark = Convert.ToSByte(comand.Value);
                    break;
                case "priority":
                    priority = Convert.ToInt32(comand.Value);
                    break;

                default:
                    break;
            }
        }

        public string Name
        {
            get { return name; }
            set { name = value; OnPropertyChanged(nameof(Name)); }
        }

        public sbyte Mark
        {
            get { return mark; }
            set { mark = value; OnPropertyChanged(nameof(Mark)); }
        }

        public int Priority
        {
            get { return priority; }
            set { priority = value; OnPropertyChanged(nameof(Priority)); }
        }

        public TLCollection<Book> Books
        {
            get { return books; }
            set { books = value; OnPropertyChanged(nameof(Books)); }
        }
    }
}
