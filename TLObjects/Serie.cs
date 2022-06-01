﻿using System;
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
        private int filmId = 0;
        private Film film = null;
        private DateTime startWatchDate = new DateTime();
        private int countOfWatchedSeries = 0;
        private int totalSeries = 0;

        public Serie() : base() { }

        protected override void updateThisBody(Cell cell)
        {
            Serie serie = (Serie)cell;

            startWatchDate = serie.startWatchDate;
            countOfWatchedSeries = serie.countOfWatchedSeries;
            totalSeries = serie.totalSeries;
        }

        protected override void saveBody(StreamWriter streamWriter, Cell defaultCell)
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
                if (Film != null)
                    Film.Serie = null;

                film = value;
                if (film != null)
                {
                    filmId = film.ID;
                    film.Serie = this;
                }
                else filmId = 0;
                OnPropertyChanged(nameof(Film));
            }
        }

        public DateTime StartWatchDate
        {
            get { return startWatchDate; }
            set { startWatchDate = value; OnPropertyChanged(nameof(StartWatchDate)); }
        }

        public int CountOfWatchedSeries
        {
            get { return countOfWatchedSeries; }
            set { countOfWatchedSeries = value; OnPropertyChanged(nameof(CountOfWatchedSeries)); }
        }

        public int TotalSeries
        {
            get { return totalSeries; }
            set { totalSeries = value; OnPropertyChanged(nameof(TotalSeries)); }
        }
    }
}
