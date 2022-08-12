using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.IO;
using System.Text;
using TablesLibrary.Interpreter;
using TablesLibrary.Interpreter.TableCell;
using TL_Objects.CellDataClasses;

namespace TL_Objects
{
    [TableCell("BookCategory")]
    public class BookCategory : Cell
    {
        private string name = "";
        private string hideName = String.Empty;
        private Mark mark = new Mark();
        private int priority = 0;

        private ObservableCollection<Book> books = new ObservableCollection<Book>();

        public BookCategory() : base()
        {
            books.CollectionChanged += Books_CollectionChanged;
            mark.PropertyChanged += Mark_PropertyChanged;
        }

        private void Mark_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            OnPropertyChanged(nameof(Mark));
            OnPropertyChanged(nameof(FormatedMark));
        }

        private void Books_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            Book book;
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Remove:
                    book = (Book)e.OldItems[0];
                    book.FranshiseId = 0;
                    book.FranshiseListIndex = -1;
                    break;
                case NotifyCollectionChangedAction.Add:
                    book = (Book)e.NewItems[0];
                    book.FranshiseId = this.ID;
                    break;

                default:
                    break;
            }

            sbyte i = 0;
            foreach (Book item in Books)
            {
                item.FranshiseListIndex = i++;
            }
        }

        public bool RemoveBookFromCategory(Book book)
        {
            if (books.Contains(book))
            {
                if (book.FranshiseId == this.ID)
                {
                    book.FranshiseId = 0;
                    book.FranshiseListIndex = -1;
                }
                return books.Remove(book);
            }
            else
            {
                return false;
            }
        }

        public bool ChangeBookPositionBy(Book book, int i)
        {
            int oldIndex = Books.IndexOf(book);
            int newIndex = oldIndex + i;

            if (newIndex > -1 && newIndex < Books.Count)
            {
                Books.Move(oldIndex, newIndex);
                return true;
            }
            return false;
        }

        protected override void updateThisBody(Cell cell)
        {
            BookCategory category = (BookCategory)cell;

            name = category.name;
            hideName = category.hideName;
            mark = category.mark;
            priority = category.priority;
            books = category.books;
        }

        protected override void saveBody(StreamWriter streamWriter, Cell defaultCell)
        {
            streamWriter.Write(FormatParam("name", name, "", 2));
            streamWriter.Write(FormatParam("hideName", hideName, String.Empty, 2));
            streamWriter.Write(FormatParam("mark", mark.RawMark, 0, 2));
            streamWriter.Write(FormatParam("priority", priority, 0, 2));
        }

        protected override void loadBody(Comand comand)
        {
            switch (comand.Paramert)
            {
                case "name":
                    name = comand.Value;
                    break;
                case "hideName":
                    hideName = comand.Value;
                    break;
                case "mark":
                    mark.RawMark = Convert.ToInt32(comand.Value);
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

        public string HideName
        {
            get { return hideName; }
            set 
            { 
                hideName = value;
                foreach (Book book in Books)
                {
                    book.OnPropertyChanged(nameof(book.Name));
                }
                OnPropertyChanged(nameof(HideName)); 
            }
        }

        public int Mark
        {
            get { return mark.RawMark; }
            set { mark.RawMark = value; OnPropertyChanged(nameof(Mark)); }
        }

        public Mark FormatedMark
        {
            get => mark;
        }

        public int Priority
        {
            get { return priority; }
            set { priority = value; OnPropertyChanged(nameof(Priority)); }
        }

        public ObservableCollection<Book> Books
        {
            get { return books; }
        }
    }
}
