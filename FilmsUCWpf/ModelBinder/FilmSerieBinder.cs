using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TL_Objects;

namespace FilmsUCWpf.ModelBinder
{
    public class FilmSerieBinder : FilmBinder
    {
        private readonly Serie serie;
        public FilmSerieBinder(Film film) : base(film)
        {
            serie = film.Serie;
            serie.PropertyChanged += Serie_PropertyChanged;
        }

        private void Serie_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(serie.StartWatchDate))
            {
                OnPropertyChanged(nameof(StartWatchDateTxt));
                return;
            }
        }

        public String StartWatchDateTxt
        {
            get => FormateDate(Model.Serie.StartWatchDate);
            set { }
        }
        public DateTime StartWatchDate
        {
            get => serie.StartWatchDate;
            set => serie.StartWatchDate = value;
        }
        public String CountOfWatchedSeries
        {
            get => formatZero(serie.CountOfWatchedSeries);
            set => serie.CountOfWatchedSeries = formatEmpty(value);
        }
        public String TotalSeries
        {
            get => formatZero(serie.TotalSeries);
            set => serie.TotalSeries = formatEmpty(value);
        }
    }
}
