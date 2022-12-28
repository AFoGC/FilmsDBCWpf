using NewTablesLibrary;

namespace TL_Objects
{
    [Cell("Serie")]
    public class Serie : Cell
    {
        [SaveField("startWatchDate")]
        private DateTime _startWatchDate;

        [SaveField("countOfWatchedSeries")]
        private int _countOfWatchedSeries;

        [SaveField("totalSeries")]
        private int _totalSeries;

        [SaveField("filmId")]
        private OneToOne<Serie, Film> _film;

        public Serie()
        {
            _film = new OneToOne<Serie, Film>(this);
            _startWatchDate = new DateTime();
            _countOfWatchedSeries = 0;
            _totalSeries = 0;
        }

        public Film Film
        {
            get { return _film.Value; }
            set
            {
                _film.SetValue(value);
                OnPropertyChanged(nameof(Film));
            }
        }

        public DateTime StartWatchDate
        {
            get { return _startWatchDate; }
            set { _startWatchDate = value; OnPropertyChanged(nameof(StartWatchDate)); }
        }

        public int CountOfWatchedSeries
        {
            get { return _countOfWatchedSeries; }
            set { _countOfWatchedSeries = value; OnPropertyChanged(nameof(CountOfWatchedSeries)); }
        }

        public int TotalSeries
        {
            get { return _totalSeries; }
            set { _totalSeries = value; OnPropertyChanged(nameof(TotalSeries)); }
        }
    }
}
