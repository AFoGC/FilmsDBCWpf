using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TL_Objects;

namespace FilmsUCWpf.ModelBinder
{
    public class FilmBinder : BaseBinder<Film>
    {
        public FilmBinder(Film film) : base(film)
        {
			film.Genre.PropertyChanged += Genre_PropertyChanged;
		}

		private void Genre_PropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			OnPropertyChanged("Genre");
		}

		private static Film defFilm = new Film();
		private static Serie defSerie = new Serie();
		public String ID { get => Model.ID.ToString(); set { } }
		public virtual String Name { get => Model.Name; set { } }
		public String Genre { get => Model.Genre.ToString(); set { } }
		public String RealiseYear { get => Film.FormatToString(Model.RealiseYear, defFilm.RealiseYear); set { } }
		public Boolean Watched { get => Model.Watched; set { } }
		public String DateOfWatch { get => FormateDate(Model.DateOfWatch); set { } }
		public String Mark { get => Model.FormatedMark.ToString(); set { } }
		public String CountOfViews { get => Film.FormatToString(Model.CountOfViews, defFilm.CountOfViews); set { } }
		public String Comment { get => Model.Comment; set { } }
		public String Sources { get => Helper.SourcesStateString(Model.Sources); set { } }

		public String StartWatchDate { get => FormateDate(Model.Serie.StartWatchDate); set { } }
		public String CountOfWatchedSeries { get => Serie.FormatToString(Model.Serie.CountOfWatchedSeries, defSerie.CountOfWatchedSeries); set { } }
		public String TotalSeries { get => Serie.FormatToString(Model.Serie.TotalSeries, defSerie.TotalSeries); set { } }
	}
}
