using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TL_Objects;

namespace FilmsUCWpf.ModelBinder
{
    public class BookBinder : BaseBinder<Book>
    {
        public BookBinder(Book book) : base(book)
        {
			book.BookGenre.PropertyChanged += BookGenre_PropertyChanged;
		}

		private void BookGenre_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			OnPropertyChanged("BookGenre");
		}

		private static Book defBook = new Book();
		public String ID { get => Model.ID.ToString(); set { } }
		public virtual String Name { get => Model.Name; set { } }
		public String BookGenre { get => Model.BookGenre.ToString(); set { } }
		public String PublicationYear { get => Film.FormatToString(Model.PublicationYear, defBook.PublicationYear); set { } }
		public Boolean Readed { get => Model.Readed; set { } }
		public String Author { get => Model.Author; set { } }
		public String FullReadDate { get => FormateDate(Model.FullReadDate); set { } }
		public String Mark { get => Model.FormatedMark.ToString(); set { } }
		public String CountOfReadings { get => Book.FormatToString(Model.CountOfReadings, defBook.CountOfReadings); set { } }
		public String Bookmark { get => Model.Bookmark; set { } }
		public String Sources { get => Helper.SourcesStateString(Model.Sources); set { } }
	}
}
