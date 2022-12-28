using NewTablesLibrary;
using System.Collections.Specialized;
using TL_Objects;
using TL_Tables.Interfaces;

namespace TL_Tables
{
	public class BooksTable : Table<Book>, IHasMarkSystem
	{
        [SaveField("markSystem")]
        private int _markSystem;

        [SaveField("newMarkSystem")]
        private bool _newMarkSystem;
        

		public BooksTable()
        {
			_newMarkSystem = false;
			MarkSystem = 6;
		}

        public bool NewMarkSystem => _newMarkSystem;
        public int MarkSystem
        {
            get => _markSystem;
            set
            {
                _markSystem = value;
                foreach (Book book in this)
                    book.FormatedMark.MarkSystem = _markSystem;
            }
        }

        protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                Book book = (Book)e.NewItems[0];
                book.FormatedMark.MarkSystem = MarkSystem;
            }

            base.OnCollectionChanged(e);
        }

        protected override void OnLoaded()
        {
            MarkSystem = _markSystem;

            if (NewMarkSystem == false)
                ChangeToNewMarkSystem();

            base.OnLoaded();
        }

		private void ChangeToNewMarkSystem()
		{
			foreach (Book book in this)
				book.Mark *= 50;

			_newMarkSystem = true;
		}
	}
}
