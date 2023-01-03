using NewTablesLibrary;
using TL_Objects.CellDataClasses;

namespace TL_Objects
{
    [Cell("BookCategory")]
    public class BookCategory : Cell
    {
        [SaveField("name")]
        private string _name;

        [SaveField("hideName")]
        private string _hideName;

        [SaveField("mark")]
        private Mark _mark;

        [SaveField("priority")]
        private int _priority;

        private readonly OneToManyCollection<BookCategory, Book> _books;

        public BookCategory()
        {
            _name = String.Empty;
            _hideName = String.Empty;
            _mark = new Mark();
            _priority = 0;
            _books = new OneToManyCollection<BookCategory, Book>(this);

            _mark.PropertyChanged += Mark_PropertyChanged;
        }

        protected override void OnRemoving()
        {
            _books.Clear();

            base.OnRemoving();
        }

        private void Mark_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            OnPropertyChanged(nameof(Mark));
            OnPropertyChanged(nameof(FormatedMark));
        }

        public bool ChangeBookPositionBy(Book book, int i)
        {
            return false;
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
                /*
                foreach (Book book in CategoryElements)
                {
                    book.OnPropertyChanged(nameof(book.Name));
                }
                */
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

        public OneToManyCollection<BookCategory, Book> CategoryElements
        {
            get => _books;
        }
    }
}
