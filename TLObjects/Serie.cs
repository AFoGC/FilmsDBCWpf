using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using TablesLibrary.Interpreter;
using TablesLibrary.Interpreter.Attributes;

namespace TL_Objects
{
    [TableCell("Serie")]
    public class Serie : Cell
    {
        private int filmId = 0;
        private Film film = null;
        private DateTime startWatchDate = new DateTime();
        private int countOfWatchedSeries = 0;
        private int totalSeries = 0;

        public Serie() : base() { }
        public Serie(int id) : base(id) { }

        protected override void updateThisBody(Cell cell)
        {
            Serie serie = (Serie)cell;

            Film = serie.Film;
            startWatchDate = serie.startWatchDate;
            countOfWatchedSeries = serie.countOfWatchedSeries;
            totalSeries = serie.totalSeries;
        }

        protected override void saveBody(StreamWriter streamWriter)
        {
            streamWriter.Write(FormatParam("filmId", filmId, 0, 2));
            streamWriter.Write(FormatParam("startWatchDate", startWatchDate, new DateTime(), 2));
            streamWriter.Write(FormatParam("countOfWatchedSeries", countOfWatchedSeries, 0, 2));
            streamWriter.Write(FormatParam("totalSeries", totalSeries, 0, 2));
        }

        protected override void loadBody(Comand comand)
        {
            switch (comand.Paramert)
            {
                case "filmId":
                    filmId = Convert.ToInt32(comand.Value);
                    break;
                case "startWatchDate":
                    startWatchDate = Convert.ToDateTime(comand.Value);
                    break;
                case "countOfWatchedSeries":
                    countOfWatchedSeries = Convert.ToInt32(comand.Value);
                    break;
                case "totalSeries":
                    totalSeries = Convert.ToInt32(comand.Value);
                    break;

                default:
                    break;
            }
        }

        public int FilmId
        {
            get { return filmId; }
        }

        public Film Film
        {
            get { return film; }
            set
            {
                film = value;
                filmId = film.ID;
            }
        }

        public DateTime StartWatchDate
        {
            get { return startWatchDate; }
            set { startWatchDate = value; }
        }

        public int CountOfWatchedSeries
        {
            get { return countOfWatchedSeries; }
            set { countOfWatchedSeries = value; }
        }

        public int TotalSeries
        {
            get { return totalSeries; }
            set { totalSeries = value; }
        }
    }
}
