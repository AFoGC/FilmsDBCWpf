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
    public class FilmBinder : BaseBinder<Film>
    {
        private GenresTable genresTable;
        public FilmBinder(Film film) : base(film)
        {
			film.Genre.PropertyChanged += Genre_PropertyChanged;
			film.PropertyChanged += Film_PropertyChanged;

            genresTable = (GenresTable)film.Genre.ParentTable;
        }

		private void Film_PropertyChanged(object sender, PropertyChangedEventArgs e)
		{
            if (e.PropertyName == nameof(Model.Genre))
            {
                OnPropertyChanged(nameof(GenreText));
                OnPropertyChanged(nameof(SelectedGenre));
                return;
            }
            if (e.PropertyName == nameof(Model.DateOfWatch))
			{
				OnPropertyChanged(nameof(DateOfWatchTxt));
				return;
			}
            if (e.PropertyName == nameof(Model.DateOfWatch))
            {
                OnPropertyChanged(nameof(DateOfWatchTxt));
                return;
            }
        }

		private void Genre_PropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			OnPropertyChanged(nameof(GenreText));
            OnPropertyChanged(nameof(SelectedGenre));
        }

		private static Film defFilm = new Film();
		private static Serie defSerie = new Serie();
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
		public String GenreText
		{ 
			get => Model.Genre.ToString(); 
			set { } 
		}
		public Genre SelectedGenre
		{
			get => Model.Genre;
			set => Model.Genre = value;
		}
        public INotifyCollectionChanged GenresCollection => genresTable;
        public String RealiseYear 
		{ 
			get => formatZero(Model.RealiseYear);
            set => Model.RealiseYear = formatEmpty(value);
        }
		public Boolean Watched 
		{ 
			get => Model.Watched;
			set => Model.Watched = value;
		}
		public String DateOfWatchTxt
		{ 
			get => FormateDate(Model.DateOfWatch); 
			set { } 
		}
		public DateTime DateOfWatch
		{
			get => Model.DateOfWatch;
			set => Model.DateOfWatch = value;
		}

        public String Mark 
		{ 
			get => Model.FormatedMark.ToString();
			set => Model.FormatedMark.SetMarkFromString(value);
    }
		public String CountOfViews 
		{ 
			get => formatZero(Model.CountOfViews);
            set => Model.CountOfViews = formatEmpty(value);
        }
		public String Comment 
		{ 
			get => Model.Comment;
			set => Model.Comment = value;
		}
		public String Sources 
		{ 
			get => Helper.SourcesStateString(Model.Sources); 
			set { } 
		}
	}
}
