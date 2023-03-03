using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.IO;
using System.Text;
using TablesLibrary.Interpreter;
using TablesLibrary.Interpreter.TableCell;
using TL_Objects.CellDataClasses;
using TL_Objects.Interfaces;

namespace TL_Objects
{
    [TableCell("BookCategory")]
    public class BookCategory : Cell, ICategory<Book>
    {
        private string _name;
        private string _hideName;
        private Mark _mark;
        private int _priority;

        private ObservableCollection<Book> _books;

        public BookCategory()
        {
            _name = String.Empty;
            _hideName = String.Empty;
            _mark = new Mark();
            _priority = 0;
            _books = new ObservableCollection<Book>();

            _books.CollectionChanged += Books_CollectionChanged;
            _mark.PropertyChanged += Mark_PropertyChanged;
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
            foreach (Book item in CategoryElements)
            {
                item.FranshiseListIndex = i++;
            }
        }

        public bool RemoveBookFromCategory(Book book)
        {
            if (_books.Contains(book))
            {
                if (book.FranshiseId == this.ID)
                {
                    book.FranshiseId = 0;
                    book.FranshiseListIndex = -1;
                }
                return _books.Remove(book);
            }
            else
            {
                return false;
            }
        }

        public bool ChangeBookPositionBy(Book book, int i)
        {
            int oldIndex = CategoryElements.IndexOf(book);
            int newIndex = oldIndex + i;

            if (newIndex > -1 && newIndex < CategoryElements.Count)
            {
                CategoryElements.Move(oldIndex, newIndex);
                return true;
            }
            return false;
        }

        protected override void updateThisBody(Cell cell)
        {
            BookCategory category = (BookCategory)cell;

            _name = category._name;
            _hideName = category._hideName;
            _mark = category._mark;
            _priority = category._priority;
            _books = category._books;
        }

        protected override void saveBody(StreamWriter streamWriter, Cell defaultCell)
        {
            streamWriter.Write(FormatParam("name", _name, "", 2));
            streamWriter.Write(FormatParam("hideName", _hideName, String.Empty, 2));
            streamWriter.Write(FormatParam("mark", _mark.RawMark, 0, 2));
            streamWriter.Write(FormatParam("priority", _priority, 0, 2));
        }

        protected override void loadBody(Comand comand)
        {
            switch (comand.Paramert)
            {
                case "name":
                    _name = comand.Value;
                    break;
                case "hideName":
                    _hideName = comand.Value;
                    break;
                case "mark":
                    _mark.RawMark = Convert.ToInt32(comand.Value);
                    break;
                case "priority":
                    _priority = Convert.ToInt32(comand.Value);
                    break;

                default:
                    break;
            }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; OnPropertyChanged(nameof(Name)); }
        }

        public string HideName
        {
            get { return _hideName; }
            set 
            { 
                _hideName = value;
                foreach (Book book in CategoryElements)
                {
                    book.OnPropertyChanged(nameof(book.Name));
                }
                OnPropertyChanged(nameof(HideName)); 
            }
        }

        public int Mark
        {
            get { return _mark.RawMark; }
            set { _mark.RawMark = value; OnPropertyChanged(nameof(Mark)); }
        }

        public Mark FormatedMark
        {
            get => _mark;
        }

        public int Priority
        {
            get { return _priority; }
            set { _priority = value; OnPropertyChanged(nameof(Priority)); }
        }

        public ObservableCollection<Book> CategoryElements
        {
            get { return _books; }
        }
    }
}
