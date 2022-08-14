using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using TablesLibrary.Interpreter;
using TablesLibrary.Interpreter.TableCell;

namespace TL_Objects
{
    [TableCell("Serie")]
    public class Serie : Cell
    {
        private int _filmId;
        private Film _film;
        private DateTime _startWatchDate;
        private int _countOfWatchedSeries;
        private int _totalSeries;

        public Serie()
        {
            _filmId = 0;
            _film = null;
            _startWatchDate = new DateTime();
            _countOfWatchedSeries = 0;
            _totalSeries = 0;
        }

        protected override void updateThisBody(Cell cell)
        {
            Serie serie = (Serie)cell;

            _startWatchDate = serie._startWatchDate;
            _countOfWatchedSeries = serie._countOfWatchedSeries;
            _totalSeries = serie._totalSeries;
        }

        protected override void saveBody(StreamWriter streamWriter, Cell defaultCell)
        {
            streamWriter.Write(FormatParam("filmId", _filmId, 0, 2));
            streamWriter.Write(FormatParam("startWatchDate", _startWatchDate, new DateTime(), 2));
            streamWriter.Write(FormatParam("countOfWatchedSeries", _countOfWatchedSeries, 0, 2));
            streamWriter.Write(FormatParam("totalSeries", _totalSeries, 0, 2));
        }

        protected override void loadBody(Comand comand)
        {
            switch (comand.Paramert)
            {
                case "filmId":
                    _filmId = Convert.ToInt32(comand.Value);
                    break;
                case "startWatchDate":
                    _startWatchDate = Convert.ToDateTime(comand.Value);
                    break;
                case "countOfWatchedSeries":
                    _countOfWatchedSeries = Convert.ToInt32(comand.Value);
                    break;
                case "totalSeries":
                    _totalSeries = Convert.ToInt32(comand.Value);
                    break;

                default:
                    break;
            }
        }

        public int FilmId
        {
            get { return _filmId; }
        }

        public Film Film
        {
            get { return _film; }
            set
            {
                if (Film != null)
                    Film.Serie = null;

                _film = value;
                if (_film != null)
                {
                    _filmId = _film.ID;
                    _film.Serie = this;
                }
                else
                {
                    _filmId = 0;
                    ParentTable.Remove(this);
                }
                    
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
