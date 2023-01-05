using NewTablesLibrary;
using TL_Objects;

namespace TL_Tables
{
    public class SeriesTable : Table<Serie>
    {
        public SeriesTable()
        {

        }

        public Serie FindAndConnectSerie(Film film)
        {
            Serie serie = this.Where(x => x.Film?.ID == film.ID).FirstOrDefault();

            if (serie != null)
            {
                serie.Film = film;
                return serie;
            }

            serie = new Serie();
            serie.Film = film;
            this.Add(serie);

            return serie;
        }
    }
}
