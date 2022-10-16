using FilmsUCWpf.ViewModel.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TL_Objects;

namespace FilmsUCWpf.ViewModel
{
    public class FilmSerieViewModel : FilmViewModel
    {
        private readonly Serie serie;
        public FilmSerieViewModel(Serie serie, IMenuViewModel<Film> menu) : base(serie.Film, menu)
        {
            this.serie = serie;
            serie.PropertyChanged += SeriePropertyChanged;
        }

        private void SeriePropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(serie.StartWatchDate))
            {
                OnPropertyChanged(nameof(StartWatchDateTxt));
                return;
            }

            OnPropertyChanged(e);
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
