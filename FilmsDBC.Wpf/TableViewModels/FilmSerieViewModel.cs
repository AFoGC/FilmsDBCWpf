using System;
using System.ComponentModel;
using TL_Objects;
using WpfApp.TableViewModels.Interfaces;

namespace WpfApp.TableViewModels
{
    public class FilmSerieViewModel : FilmViewModel
    {
        private readonly Serie serie;

        public FilmSerieViewModel(Serie serie, IMenuViewModel<Film> menu) : base(serie.Film, menu)
        {
            this.serie = serie;
            serie.PropertyChanged += SeriePropertyChanged;
        }

        public Serie Serie => serie;

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
