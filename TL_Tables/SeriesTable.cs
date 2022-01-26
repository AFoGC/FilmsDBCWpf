using System;
using System.Collections.Generic;
using System.Text;
using TablesLibrary.Interpreter;
using TablesLibrary.Interpreter.Table;
using TL_Objects;

namespace TL_Tables
{
    public class SeriesTable : Table<Serie>
    {
        public SeriesTable() : base() { }
        public SeriesTable(int id) : base(id) { }
        public SeriesTable(int id, string name) : base(id, name) { }

        public override void ConnectionsSubload(TableCollection tablesCollection)
        {
            Table<Film> filmsTable = tablesCollection.GetTable<Film>();

            foreach (Film film in filmsTable)
            {
                if (film.Genre.IsSerialGenre)
                {
                    FindAndConnectSerie(film);
                }
            }

            foreach (Serie serie in this)
            {
                if (serie.Film == null)
                {
                    serie.Film = filmsTable.GetElementByIndex(serie.FilmId);
                }
            }
        }

        public Serie FindAndConnectSerie(Film film)
        {
            foreach (Serie serie in this)
            {
                if (serie.FilmId == film.ID)
                {
                    return serie;
                }
            }

            Serie ser = new Serie();
            ser.Film = film;
            this.AddElement(ser);

            return ser;
        }
    }
}
