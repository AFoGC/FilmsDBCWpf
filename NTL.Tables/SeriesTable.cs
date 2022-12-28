using System.Collections.Generic;
using System.Linq;
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

            Serie[] EmptySeries = this.Where(x => x.Film == null).ToArray();
            foreach (Serie serie in EmptySeries)
            {
                this.Remove(serie);
            }
        }

        public Serie FindAndConnectSerie(Film film)
        {
            Serie serie = this.Where(x => x.FilmId == film.ID).FirstOrDefault();

            if (serie != null)
            {
                serie.Film = film;
                return serie;
            }

            serie = new Serie();
            serie.Film = film;
            this.AddElement(serie);

            return serie;
        }
    }
}
