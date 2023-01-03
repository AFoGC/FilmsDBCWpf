using NewTablesLibrary;

namespace TL_Objects
{
    [Cell("PriorityBook")]
    public class PriorityBook : Cell
    {
        private OneToOne<PriorityBook, Book> _book;

        public PriorityBook()
        {
            _book = new OneToOne<PriorityBook, Book>(this);
        }

        protected override void OnRemoving()
        {
            _book.SetValue(null);

            base.OnRemoving();
        }

        public Book Book
        {
            get => _book.Value;
            set
            {
                _book.SetValue(value);
                OnPropertyChanged(nameof(Book));
            }
        }
    }
}
