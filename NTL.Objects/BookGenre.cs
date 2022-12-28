using NewTablesLibrary;
using TL_Objects.Interfaces;

namespace TL_Objects
{
    [Cell("BookGenre")]
    public class BookGenre : Cell, IGenre
    {
        [SaveField("name")]
        private string _name = string.Empty;

        private readonly OneToManyCollection<BookGenre, Book> _books;

        public BookGenre()
        {
            _books = new OneToManyCollection<BookGenre, Book>(this);
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; OnPropertyChanged(nameof(Name)); }
        }

        public override string ToString()
        {
            return _name;
        }
    }
}
