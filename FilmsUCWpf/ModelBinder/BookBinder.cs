using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TL_Objects;
using TL_Tables;

namespace FilmsUCWpf.ModelBinder
{
    public class BookBinder : BaseBinder<Book>
    {
		private BookGenresTable genresTable;
        public BookBinder(Book book) : base(book)
        {
			book.PropertyChanged += Book_PropertyChanged;
			book.BookGenre.PropertyChanged += BookGenre_PropertyChanged;
			book.FormatedMark.PropertyChanged += FormatedMark_PropertyChanged;

			genresTable = (BookGenresTable)book.BookGenre.ParentTable;
		}

		private void Book_PropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			if (e.PropertyName == nameof(Model.BookGenre))
			{
                OnPropertyChanged(nameof(BookGenreText));
                OnPropertyChanged(nameof(SelectedGenre));
                return;
            }
		}

		private void FormatedMark_PropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			OnPropertyChanged(nameof(Mark));
			if (e.PropertyName == nameof(Model.FormatedMark.MarkSystem))
			{
                OnPropertyChanged(nameof(Marks));
            }
        }

		private void BookGenre_PropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			OnPropertyChanged(nameof(BookGenreText));
			OnPropertyChanged(nameof(SelectedGenre));
        }

		public String ID
		{ 
			get => Model.ID.ToString(); 
			set { } 
		}
		public virtual String Name 
		{ 
			get => Model.Name; 
			set => Model.Name = value; 
		}

		public String BookGenreText
		{ 
			get => Model.BookGenre.ToString(); 
			set { } 
		}
		public BookGenre SelectedGenre 
		{ 
			get => Model.BookGenre; 
			set => Model.BookGenre = value; 
		}
		public INotifyCollectionChanged GenresCollection => genresTable;

        public String PublicationYear
		{ 
			get => formatZero(Model.PublicationYear); 
			set => Model.PublicationYear = formatEmpty(value); 
		}
		public Boolean Readed 
		{ 
			get => Model.Readed; 
			set => Model.Readed = value; 
		}
		public String Author 
		{ 
			get => Model.Author; 
			set => Model.Author = value; 
		}
		public String FullReadDate 
		{ 
			get => FormateDate(Model.FullReadDate); 
			set { } 
		}
		public String Mark 
		{ 
			get => Model.FormatedMark.ToString();
			set => Model.FormatedMark.SetMarkFromString(value);
		}
		public List<String> Marks => Model.FormatedMark.GetComboItems();
		public String CountOfReadings 
		{ 
			get => formatZero(Model.CountOfReadings); 
			set => Model.CountOfReadings = formatEmpty(value); 
		}
		public String Bookmark 
		{ 
			get => Model.Bookmark; 
			set => Model.Bookmark = value; 
		}
		public String Sources 
		{ 
			get => Helper.SourcesStateString(Model.Sources); 
			set { } 
		}
	}
}
